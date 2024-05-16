using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using static GameData;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public List<Transform> drawnObjectTransforms; 

    [SerializeField] private GameObject bucketPrefab;
    [SerializeField] private GameObject trianglePrefab;
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private GameObject rectanglePrefab;
    [SerializeField] private Mesh mesh;
    [SerializeField] private ZPositionProvider zPos;
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private UploadDrawn uploadDrawn;

    public GameData gameData;

    public void DeleteAllPrefs()
    {
        PlayerPrefs.DeleteAll();
        File.Delete(GetPath());
        gameData = new GameData();
    }

    private void Start()
    {
        gameData = Load();
        LoadBuckets();
        LoadCircles();
        LoadTriangles();
        LoadSquares();
        LoadRectangles();
        LoadDrawns();
        LoadErasers();
    }
    private void OnApplicationQuit()
    {
        SaveCircles();
        SaveTriangles();
        SaveSquares();
        SaveRectangles();
        Save(gameData);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveCircles();
            SaveTriangles();
            SaveSquares();
            SaveRectangles();
            Save(gameData);
        }
    }

    public static void Save(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(GetPath(), json);
    }

    public static GameData Load()
    {
        if (File.Exists(GetPath()))
        {
            string json = File.ReadAllText(GetPath());
            GameData loadedData = JsonUtility.FromJson<GameData>(json);
            return loadedData;
        }
        else
        {
            GameData emptyData = new GameData();
            Save(emptyData);
            Debug.Log("new game");
            return emptyData;
        }
    }

    private static string GetPath()
    {
        return Application.persistentDataPath + "/" + "savedData.json";
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }

    public void ClearCanva()
    {
        GameObject[] drawnObjects = GameObject.FindGameObjectsWithTag("Drawn");
        GameObject[] eraserObjects = GameObject.FindGameObjectsWithTag("Eraser");
        GameObject[] bucketObjects = GameObject.FindGameObjectsWithTag("Bucket");
        GameObject[] circleObjects = GameObject.FindGameObjectsWithTag("Circle");
        GameObject[] squareObjects = GameObject.FindGameObjectsWithTag("Square");
        GameObject[] triangleObjects = GameObject.FindGameObjectsWithTag("Triangle");
        GameObject[] rectangleObjects = GameObject.FindGameObjectsWithTag("Rectangle");

        foreach (GameObject obj in drawnObjects)
        {
                Destroy(obj);
        }

        foreach (GameObject obj in eraserObjects)
        {
            Destroy(obj);
        }

        foreach (GameObject obj in bucketObjects)
        {
            Destroy(obj);
        }

        foreach (GameObject obj in circleObjects)
        {
            Destroy(obj);
        }

        foreach (GameObject obj in squareObjects)
        {
            Destroy(obj);
        }

        foreach (GameObject obj in triangleObjects)
        {
            Destroy(obj);
        }

        foreach (GameObject obj in rectangleObjects)
        {
            Destroy(obj);
        }
    }

    public void SaveBuckets(GameObject bucket, float zPos)
    {
        Color spriteColor = bucket.GetComponent<SpriteRenderer>().color;
        string rgbString = ColorToRGBString(spriteColor);
        gameData.bucketZPos = zPos;
        gameData.bucketColor = rgbString;
        PlayerPrefs.SetString("BucketColor", rgbString);
        PlayerPrefs.SetFloat("BucketZPos", zPos);
    }

    public void LoadBuckets()
    {
        if (gameData.bucketColor != null && gameData.bucketZPos != 0)
        {
            string savedRGBString = gameData.bucketColor;
            Color savedColor = RGBStringToColor(savedRGBString);
            bucketPrefab.GetComponent<SpriteRenderer>().color = savedColor;
            bucketPrefab.tag = "Bucket";
            Vector3 bucketPos = new Vector3(0, 0, gameData.bucketZPos);
            Instantiate(bucketPrefab, bucketPos, Quaternion.identity);
        }
    }

    public void SaveCircles()
    {
        GameObject[] circleObjects = GameObject.FindGameObjectsWithTag("Circle");
        foreach (GameObject circleObj in circleObjects) 
        {
            Color spriteColor = circleObj.GetComponent<SpriteRenderer>().color;
            string rgbString = ColorToRGBString(spriteColor);

            GameData.Circle circle = new GameData.Circle();
            circle.color = rgbString;

            circle.x = circleObj.transform.position.x;
            circle.y = circleObj.transform.position.y;
            circle.z = circleObj.transform.position.z;

            gameData.circles.Add(circle);
        }
    }

    public void LoadCircles()
    {
        if(gameData.circles != null) 
        {
            foreach(GameData.Circle circleObj in gameData.circles)
            {
                string savedRGBString = circleObj.color;
                Color savedColor = RGBStringToColor(savedRGBString);
                circlePrefab.GetComponent<SpriteRenderer>().color = savedColor;

                Vector3 savedPosition = new Vector3(
                    circleObj.x,
                    circleObj.y,
                    circleObj.z
                 );
                circlePrefab.tag = "Circle";
                Instantiate(circlePrefab, savedPosition, Quaternion.identity);
            }
        }
    }

    public void SaveTriangles()
    {
        GameObject[] triangleObjects = GameObject.FindGameObjectsWithTag("Triangle");
        foreach (GameObject triangleObj in triangleObjects)
        {
            Color spriteColor = triangleObj.GetComponent<SpriteRenderer>().color;
            string rgbString = ColorToRGBString(spriteColor);

            GameData.Triangle triangle = new GameData.Triangle();
            triangle.color = rgbString;

            triangle.x = triangleObj.transform.position.x;
            triangle.y = triangleObj.transform.position.y;
            triangle.z = triangleObj.transform.position.z;

            gameData.triangles.Add(triangle);
        }
    }
   
    public void LoadTriangles()
    {
        if(gameData.triangles != null)
        {
            foreach(GameData.Triangle triangleObj in gameData.triangles)
            {
                string savedRGBString = triangleObj.color;
                Color savedColor = RGBStringToColor(savedRGBString);
                trianglePrefab.GetComponent<SpriteRenderer>().color = savedColor;

                Vector3 savedPosition = new Vector3(
                    triangleObj.x,
                    triangleObj.y,
                    triangleObj.z
                 );
                trianglePrefab.tag = "Triangle";
                Instantiate(trianglePrefab, savedPosition, Quaternion.identity);
            }
        }
    }

    public void SaveSquares()
    {
        GameObject[] squaresObjects = GameObject.FindGameObjectsWithTag("Square");
        foreach (GameObject squareObj in squaresObjects)
        {
            Color spriteColor = squareObj.GetComponent<SpriteRenderer>().color;
            string rgbString = ColorToRGBString(spriteColor);

            GameData.Square square = new GameData.Square();
            square.color = rgbString;

            square.x = squareObj.transform.position.x;
            square.y = squareObj.transform.position.y;
            square.z = squareObj.transform.position.z;

            gameData.squares.Add(square);
        }
    }

    public void LoadSquares()
    {
        if(gameData.squares != null) 
        {
            foreach(GameData.Square squareObj in gameData.squares)
            {
                string savedRGBString = squareObj.color;
                Color savedColor = RGBStringToColor(savedRGBString);
                squarePrefab.GetComponent<SpriteRenderer>().color = savedColor;
                Vector3 savedPosition = new Vector3(
                    squareObj.x,
                    squareObj.y,
                    squareObj.z
                 );
                squarePrefab.tag = "Square";
                Instantiate(squarePrefab, savedPosition, Quaternion.identity);
            }
        }
    }

    public void SaveRectangles()
    {
        GameObject[] rectangleObjects = GameObject.FindGameObjectsWithTag("Rectangle");
        if(rectangleObjects.Length > 0)
        {
            foreach (GameObject rectangleObj in rectangleObjects)
            {
                Color spriteColor = rectangleObj.GetComponent<SpriteRenderer>().color;
                string rgbString = ColorToRGBString(spriteColor);

                GameData.Rectangle rectangle = new GameData.Rectangle();

                rectangle.color = rgbString;
                rectangle.x = rectangleObj.transform.position.x;
                rectangle.y = rectangleObj.transform.position.y;
                rectangle.z = rectangleObj.transform.position.z;
                gameData.rectangles.Add(rectangle);
            }
        }
    }

    public void LoadRectangles()
    {
        if(gameData.rectangles != null)
        {
            foreach(GameData.Rectangle rectangleObj in gameData.rectangles)
            {
                string savedRGBString = rectangleObj.color;
                Color savedColor = RGBStringToColor(savedRGBString);
                rectanglePrefab.GetComponent<SpriteRenderer>().color = savedColor;
                Vector3 savedPosition = new Vector3(
                    rectangleObj.x,
                    rectangleObj.y,
                    rectangleObj.z
                 );
                rectanglePrefab.tag = "Rectangle";
                Instantiate(rectanglePrefab, savedPosition, Quaternion.identity);
            }
        }
    }

    public void LoadDrawns()
    {
        if(gameData.drawns != null ) 
        {
            foreach(GameData.Drawn drawnObj  in gameData.drawns)
            {
                GameObject uploadedDrawn = new GameObject();
                uploadedDrawn.AddComponent<LineRenderer>();
                uploadedDrawn.GetComponent<LineRenderer>().material = new Material(Shader.Find("UI/Default"));
                uploadedDrawn.AddComponent<UploadDrawn>();

                string savedRGBString = drawnObj.color;
                Color savedColor = RGBStringToColor(savedRGBString);

                uploadedDrawn.tag = "Drawn";
                uploadedDrawn.GetComponent<UploadDrawn>().drawn = drawnObj;
                uploadedDrawn.GetComponent<UploadDrawn>().color = savedColor;
            }
        }
    }

    public void LoadErasers()
    {
        if (gameData.erasers != null)
        {
            foreach (GameData.Eraser eraserObj in gameData.erasers)
            {
                GameObject uploadedEraser = new GameObject();
                uploadedEraser.AddComponent<LineRenderer>();
                uploadedEraser.GetComponent<LineRenderer>().material = new Material(Shader.Find("UI/Default"));
                uploadedEraser.AddComponent<UploadDrawn>();
                uploadedEraser.GetComponent<UploadDrawn>().startWidth = 1f;
                uploadedEraser.tag = "Eraser";
                uploadedEraser.GetComponent<UploadDrawn>().eraser = eraserObj;
                uploadedEraser.GetComponent<UploadDrawn>().color = Color.white;
            }
        }
    }

    public string ColorToRGBString(Color color)
    {
        int r = Mathf.RoundToInt(color.r * 255);
        int g = Mathf.RoundToInt(color.g * 255);
        int b = Mathf.RoundToInt(color.b * 255);

        return r.ToString() + "," + g.ToString() + "," + b.ToString();
    }

    public Color RGBStringToColor(string rgbString)
    {
        string[] rgbValues = rgbString.Split(',');
        
        if (rgbValues.Length != 3)
        {
            return Color.white; 
        }

        float r = Mathf.Clamp01(float.Parse(rgbValues[0]) / 255f);
        float g = Mathf.Clamp01(float.Parse(rgbValues[1]) / 255f);
        float b = Mathf.Clamp01(float.Parse(rgbValues[2]) / 255f);

        return new Color(r, g, b);
    }

}
