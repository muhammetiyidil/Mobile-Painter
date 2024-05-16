using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    private AudioController audioController;
    private ZPositionProvider zPos;
    private ShapeGenerator shpGn;
    private GameData gameData;
    private GameData.Eraser saveEraser;
    private LineRenderer line;
    private List<Vector3> linePoints = new List<Vector3>();

    private float minDistance = 0.1f;
    private bool firstTime = true;
    private bool eraserControl = true;

    private List<float> drawnsX;
    private List<float> drawnsY;


    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;

        GameObject audioControllerObj = GameObject.Find("AudioController");
        audioController = audioControllerObj.GetComponent<AudioController>();

        gameData = GameObject.Find("GameManagement").GetComponent<GameManagement>().gameData;
        saveEraser = new GameData.Eraser();

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
        eraserControl = shpGn.EraserControl();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && eraserControl)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Ended && firstTime)
            {
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(touch.position);
                currentPosition.z = zPos.ReceiveZPosition();
                drawnsX.Add(currentPosition.x);
                drawnsY.Add(currentPosition.y);
                saveEraser.z = currentPosition.z;
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
                    saveEraser.x = drawnsX;
                    saveEraser.y = drawnsY;
                    gameData.erasers.Add(saveEraser);
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

    public void StartDrawing()
    {
        firstTime = true;
    }
}
