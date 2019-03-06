using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseRiver : MonoBehaviour
{
    Tile WaterTile;
    public void BuildRiverFromMapEdge(MapPoint _point, Tile _waterTile)
    {

        WaterTile = _waterTile;


        GenerateSineSection(_point, true);

        //GenerateStraightSection(_point);
        
    }

    private MapPoint? GenerateSineSection(MapPoint _startPoint, bool _EndAtEdge)
    {
        float curveStrech = 2F;
        float curveFrequency = 1F; //10 = about 1 up and down over 72 tiles (Tiny)


        //randomPoint in Degrees
        float randomPoint = 0; //start of wave
        //float randomPoint = 35;

        int thick = 0;

        GenerationManager.instance.GetGameMap().AddTile(WaterTile, _startPoint);

        MapPoint currentPoint = _startPoint;
        int xDirection = 0;
        int yDirection = 0;
        if (_startPoint.x == 0)
        {
            xDirection = 1;
        }
        else if (_startPoint.x == GenerationManager.instance.Width)
        {
            xDirection = -1;
        }
        if (_startPoint.y == 0)
        {
            yDirection = 1;
        }
        else if (_startPoint.y == GenerationManager.instance.Height)
        {
            yDirection = -1;
        }
        ApplyThicknessToRiver(currentPoint, thick, xDirection, yDirection);
        int progress = 1;

        while (progress < GenerationManager.instance.Width)
        {
            float pointInDegrees = ((currentPoint.x * 180) + randomPoint); //y = sin(x*180) each tile will be y = 1, 0 or -1
            float pointInRadians = pointInDegrees * Mathf.Deg2Rad;


            float holdX = currentPoint.x;

            float hold = Mathf.Sin(holdX * Mathf.PI/2F);//pointInRadians / curveFrequency); //Mathf.sin uses Radians           
            //float hold = Mathf.Sin(pointInRadians);//pointInRadians / curveFrequency); //Mathf.sin uses Radians           
            int sineHeight = Mathf.RoundToInt(hold * curveStrech); //SineHeight is Y in y = sin(x)

            

            Debug.Log("X: " + currentPoint.x + "     Y: " + hold);

            currentPoint.x = currentPoint.x + xDirection + (sineHeight * yDirection);
            currentPoint.y = currentPoint.y + yDirection + (sineHeight * xDirection);
            GenerationManager.instance.GetGameMap().AddTile(WaterTile, currentPoint);

            ApplyThicknessToRiver(currentPoint, thick, xDirection, yDirection);

            progress++;
        }


        return null;
    }

    private void GenerateStraightSection(MapPoint _startPoint, MapPoint? _endPoint = null)
    {
        int thick = 2;

        int seed = UnityEngine.Random.Range(0, 5000);
        GenerationManager.instance.GetGameMap().AddTile(WaterTile, _startPoint);

        MapPoint currentPoint = _startPoint;
        int xDirection = 0;
        int yDirection = 0;

        if (_startPoint.x == 0)
        {
            xDirection = 1;
        }
        else if (_startPoint.x == GenerationManager.instance.Width)
        {
            xDirection = -1;
        }
        if (_startPoint.y == 0)
        {
            yDirection = 1;
        }
        else if (_startPoint.y == GenerationManager.instance.Height)
        {
            yDirection = -1;
        }
   
        ApplyThicknessToRiver(currentPoint, thick, xDirection, yDirection);

        bool generating = true;

        while (generating)
        {
            float xCoord = currentPoint.x / (float)GenerationManager.instance.Width * seed;
            float yCoord = (float)currentPoint.y / (float)GenerationManager.instance.Height * seed;
            float test = Mathf.PerlinNoise(xCoord, yCoord);
            Debug.Log(test);
            float test2 = (test * 3F) - 1F;
            int change = Mathf.FloorToInt(test2);

            Debug.Log(change);

            currentPoint.x = currentPoint.x + xDirection + (change * yDirection);
            currentPoint.y = currentPoint.y + yDirection + (change * xDirection);

            GenerationManager.instance.GetGameMap().AddTile(WaterTile, currentPoint);

            ApplyThicknessToRiver(currentPoint, thick, xDirection, yDirection);

            if (currentPoint.x < 0 || currentPoint.y < 0 || currentPoint.x == GenerationManager.instance.Width - 1 || currentPoint.y == GenerationManager.instance.Height - 1)
            {
                generating = false;
            }
        }
    }

    private void ApplyThicknessToRiver(MapPoint _riverCentre, int _radius, int _xDirection, int _yDirection)
    {
        for (int i = 1; i <= _radius; i++)
        {
            MapPoint sidePos = new MapPoint();
            sidePos.x = _riverCentre.x + (i * _yDirection);
            sidePos.y = _riverCentre.y + (i * _xDirection);
            GenerationManager.instance.GetGameMap().AddTile(WaterTile, sidePos);

            MapPoint sideNeg = new MapPoint();
            sideNeg.x = _riverCentre.x + (-i * _yDirection);
            sideNeg.y = _riverCentre.y + (-i * _xDirection);
            GenerationManager.instance.GetGameMap().AddTile(WaterTile, sideNeg);
        }
    }
           
}
