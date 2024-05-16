using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameData;

public class UploadDrawn : MonoBehaviour
{
    private LineRenderer line;
    private List<Vector3> linePoints = new List<Vector3>();
    private float minDistance = 0.1f;
    public GameData.Drawn drawn;
    public GameData.Eraser eraser;
    public Color color = Color.cyan;
    public Vector3 currentPosition = Vector3.zero;
    public float startWidth = 0.1f;


    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
        line.startWidth = startWidth;
        line.endWidth = startWidth;

        if(gameObject.tag == "Drawn")
        {
            for (int i = 0; i < drawn.x.Count; i++)
            {
                currentPosition = new Vector3(drawn.x[i], drawn.y[i], drawn.z);
                if (Vector3.Distance(currentPosition, linePoints.LastOrDefault()) > minDistance)
                {
                    linePoints.Add(currentPosition);
                    UpdateLineRenderer();
                }

            }
            if (RGBStringToColor(drawn.color) != null)
            {
                Color uploadColor = RGBStringToColor(drawn.color);
                line.SetColors(uploadColor, uploadColor);
            }
            else
            {
                line.SetColors(Color.white, Color.white);
            }
        }
        else if(gameObject.tag == "Eraser")
        {
            for (int i = 0; i < eraser.x.Count; i++)
            {
                Debug.Log("eraser uploaded "+i);
                currentPosition = new Vector3(eraser.x[i], eraser.y[i], eraser.z);
                if (Vector3.Distance(currentPosition, linePoints.LastOrDefault()) > minDistance)
                {
                    linePoints.Add(currentPosition);
                    UpdateLineRenderer();
                }

            }
        }  
    }
    private void UpdateLineRenderer()
    {
        line.positionCount = linePoints.Count;
        line.SetPositions(linePoints.ToArray());
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
