using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private ShapeGenerator shapeGenerator;
    [SerializeField] private Drawn drawn;
    private Color color;
    private bool colorControl = false;
    private bool drawnControl = true;

    void Update()
    {
        DrawnGenerate();
    }

    public void ReceiveColor(Material material)
    {
        if(shapeGenerator.EraserControl())
        {
            color = Color.white;
            colorControl = true;
        }
        else
        {
            color = material.color;
            colorControl = true;
        }
    }

    public void DrawnGenerate()
    {
        drawnControl = shapeGenerator.DrawnControl();
        if (Input.touchCount > 0 && drawnControl)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    if (color != null && colorControl)
                    {
                        drawn.GetComponent<LineRenderer>().SetColors(color, color);
                    }

                    Instantiate(drawn);
                    drawn.tag = "Drawn";
                    //if (shapeGenerator.EraserControl())
                    //{
                    //    Debug.Log("eraser is active ");
                    //    drawn.startWidth = 1;
                    //}
                    //else
                    //{
                    //    drawn.startWidth = 0.1f;
                    //}
                }
            }
        }

    }

    public void Eraser()
    {
        
    }
}
