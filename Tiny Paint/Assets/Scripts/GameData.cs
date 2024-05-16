using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public float bucketZPos;
    public string bucketColor;

    public List<Circle> circles = new List<Circle>();
    public List<Triangle> triangles = new List<Triangle>();
    public List<Square> squares = new List<Square>();
    public List<Rectangle> rectangles = new List<Rectangle>();
    public List<Drawn> drawns = new List<Drawn>();
    public List<Eraser> erasers = new List<Eraser>();



    public GameData()
    {
        bucketZPos = 0;
        bucketColor = string.Empty;
        circles = new List<Circle>();
        triangles = new List<Triangle>();
        squares = new List<Square>();
        rectangles = new List<Rectangle>();
        drawns = new List<Drawn>();
        erasers = new List<Eraser>();
    }

    [System.Serializable]
    public struct Circle
    {
        public string color;
        public float x, y, z;   
    }

    [System.Serializable]
    public struct Triangle
    {
        public string color;
        public float x, y, z;
    }

    [System.Serializable]
    public struct Square
    {
        public string color;
        public float x, y, z;
    }

    [System.Serializable]
    public struct Rectangle
    {
        public string color;
        public float x, y, z;
    }

    [System.Serializable]
    public struct Drawn
    {
        public string color;
        public List<float> x;
        public List<float> y;
        public float z;
    }

    [System.Serializable]
    public struct Eraser
    {
        public List<float> x;
        public List<float> y;
        public float z;
    }
}
