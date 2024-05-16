using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserHelper : MonoBehaviour
{
    [SerializeField] private ShapeGenerator shapeGenerator;
    private bool eraserControl = false;

    void Update()
    {
        eraserControl = shapeGenerator.EraserControl();
        if (Input.touchCount > 0 && eraserControl)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    GameObject eraser = new GameObject();
                    eraser.name = "eraser";
                    eraser.tag = "Eraser";
                    eraser.AddComponent<LineRenderer>();
                    eraser.AddComponent<Eraser>();
                    eraser.GetComponent<LineRenderer>().material = new Material(Shader.Find("UI/Default"));
                }
            }
        }

    }
}
