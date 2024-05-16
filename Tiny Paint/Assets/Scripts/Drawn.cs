using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Drawn : MonoBehaviour
{
    private AudioController audioController;
    private ZPositionProvider zPos;
    private ShapeGenerator shpGn;
    private GameData gameData;
    private GameData.Drawn saveDrawn;
    private LineRenderer line;
    private List<Vector3> linePoints = new List<Vector3>();

    private float minDistance = 0.1f;
    private bool firstTime = true;
    private bool drawnControl = true;

    private List<float> drawnsX;
    private List<float> drawnsY;

    public float startWidth = 0.1f;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
        line.startWidth = startWidth;

        GameObject audioControllerObj = GameObject.Find("AudioController");
        audioController = audioControllerObj.GetComponent<AudioController>();

        gameData = GameObject.Find("GameManagement").GetComponent<GameManagement>().gameData;
        saveDrawn = new GameData.Drawn();

         drawnsX = new List<float>();
         drawnsY = new List<float>();


    GameObject zPositionProviderObj = GameObject.Find("ZPositionProvider");
        if (zPositionProviderObj == null)
        {
            Debug.Log("zpos did not taken");
        }
        else
        {

            zPos = zPositionProviderObj.GetComponent<ZPositionProvider>();
        }
        GameObject shpGnObj = GameObject.Find("ShapeGenerator");
        shpGn = shpGnObj.GetComponent<ShapeGenerator>();
        drawnControl = shpGn.DrawnControl();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && drawnControl)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Ended && firstTime)
            {
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(touch.position);
                currentPosition.z = zPos.ReceiveZPosition();
                drawnsX.Add(currentPosition.x);
                drawnsY.Add(currentPosition.y);
                saveDrawn.z = currentPosition.z;
                zPos.zPosSaved = false;

                if (Vector3.Distance(currentPosition, linePoints.LastOrDefault()) > minDistance)
                {
                    audioController.PlayDrawingSound();
                    linePoints.Add(currentPosition);
                    UpdateLineRenderer();
                }
            }
            else
            {
                if (firstTime)
                {
                    audioController.StopDrawingSound();
                    saveDrawn.x = drawnsX;
                    saveDrawn.y = drawnsY;
                    saveDrawn.color = ColorToRGBString(line.startColor);
                    gameData.drawns.Add(saveDrawn);
                    zPos.zPosSaved = true;
                }
                firstTime = false;
            }
        }
    }

    private void UpdateLineRenderer()
    {
        line.positionCount = linePoints.Count;
        line.SetPositions(linePoints.ToArray());
    }

    public void ReceiveColor(Material material)
    {
        Color color = material.color;
        string savedColor = ColorToRGBString(color);
    }

    public void StartDrawing()
    {
        firstTime = true;
    }

    public string ColorToRGBString(Color color)
    {
        int r = Mathf.RoundToInt(color.r * 255);
        int g = Mathf.RoundToInt(color.g * 255);
        int b = Mathf.RoundToInt(color.b * 255);

        return r.ToString() + "," + g.ToString() + "," + b.ToString();
    }
}
