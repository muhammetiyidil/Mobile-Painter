using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof (MeshRenderer), typeof(MeshFilter))]
public class ShapeGenerator : MonoBehaviour
{
    [SerializeField] private ZPositionProvider zPos;
    [SerializeField] private Material material;
    [SerializeField] private GameManagement gameManagement;
    [SerializeField] private AudioController audioController;

    private bool triangleGenerate = false;
    private bool squareGenerate = false;
    private bool rectangleGenerate = false;
    private bool circleGenerate = false;
    private bool drawnControl = true;
    private bool canSpawn = true;
    private bool eraserControl = false;

    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject triangle;
    [SerializeField] private GameObject square;
    [SerializeField] private GameObject rectangle;

    public void Drawn()
    {
        squareGenerate = false;
        rectangleGenerate = false;
        circleGenerate = false;
        triangleGenerate = false;
        eraserControl = false;
        drawnControl = true;
    }

    public bool DrawnControl()
    {
        return drawnControl;
    }

    public bool EraserControl()
    {
        return eraserControl;
    }

    public void TriangleGenerate()
    {
        squareGenerate = false;
        rectangleGenerate = false;
        circleGenerate = false;
        drawnControl = false;
        eraserControl = false;
        triangleGenerate = true;
    }

    public void SquareGenerate()
    {
        triangleGenerate = false;
        rectangleGenerate = false;
        circleGenerate = false;
        drawnControl = false;
        eraserControl = false;
        squareGenerate = true;
    }

    public void RectangleGenerate()
    {
        triangleGenerate= false;
        squareGenerate= false;
        circleGenerate= false;
        drawnControl = false;
        eraserControl = false;
        rectangleGenerate = true;
    }

    public void CircleGenerate() 
    {
        triangleGenerate = false;
        squareGenerate = false;
        rectangleGenerate = false;
        drawnControl = false;
        eraserControl = false;
        circleGenerate = true;
    }

    public void StartEraser()
    {
        triangleGenerate = false;
        squareGenerate = false;
        rectangleGenerate = false;
        circleGenerate = false;
        drawnControl = true;
        eraserControl = true;
    }

    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && triangleGenerate && canSpawn)
        {
            audioController.PlayShapeSound();
            Vector3 newPosition = Utils.GetMouseWorldPosition();
            newPosition.z = zPos.ReceiveZPosition();
            Instantiate(triangle, newPosition, Quaternion.identity);
            triangle.tag = "Triangle";
            canSpawn = false;
        }

        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && squareGenerate && canSpawn)
        {
            audioController.PlayShapeSound();
            Vector3 newPosition = Utils.GetMouseWorldPosition();
            newPosition.z = zPos.ReceiveZPosition();
            square.tag = "Square";
            Instantiate(square, newPosition, Quaternion.identity);
            canSpawn = false;
        }

        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && rectangleGenerate && canSpawn)
        {
            audioController.PlayShapeSound();
            Vector3 newPosition = Utils.GetMouseWorldPosition();
            newPosition.z = zPos.ReceiveZPosition();
            rectangle.tag = "Rectangle";
            Instantiate(rectangle, newPosition, Quaternion.identity);
            canSpawn = false;
        }

        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && circleGenerate && canSpawn) 
        {
            audioController.PlayShapeSound();
            Vector3 newPosition = Utils.GetMouseWorldPosition();
            newPosition.z = zPos.ReceiveZPosition();
            GameObject circleObj = Instantiate(circle, newPosition, Quaternion.identity);
            circleObj.tag = "Circle";
            canSpawn = false;
        }

        if (Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            canSpawn = true;
        }
    }

    public void SelectColor(Material material)
    {
        GetComponent<MeshRenderer>().material = material;
    }

    public void SelectColorCircle(GameObject circle)
    {
        this.circle = circle;
    }

    public void SelectColorTriangle(GameObject triangle)
    {
        this.triangle = triangle;
    }

    public void SelectColorSquare(GameObject square)
    {
        this.square = square;
    }

    public void SelectColorRectangle(GameObject rectangle)
    {
        this.rectangle = rectangle;
    }
}
