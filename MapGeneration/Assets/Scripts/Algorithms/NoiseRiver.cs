using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseRiver 
{
    Tile WaterTile;

    struct StartRiverMidMap
    {
        public MapPoint startPoint;
        public int xDirection;
        public int yDirection;
        public int thickness;     
    }



    public void BuildRiverFromMapEdge(MapPoint _point, Tile _waterTile)
    {
        WaterTile = _waterTile;


        StartRiverMidMap? SineEndPoint = GenerateSineSection(_point, false);

        if (SineEndPoint != null)
        {
            GenerateStraightSection(SineEndPoint.Value.startPoint, null, SineEndPoint.Value.xDirection,SineEndPoint.Value.yDirection);
        }

        
        //GenerateStraightSection(_point);

    }

    private StartRiverMidMap? GenerateSineSection(MapPoint _startPoint, bool _EndAtEdge)
    {
        float curveStrech = 1.4F;
        float curveFrequency = 10.6F; //10 = about 1 up and down over 72 tiles (Tiny)

        //randomPoint in Degrees
        //float randomStartPoint = 0; //start of wave
        float randomStartPoint = 21;

        int thick = 2;

        float randomCurveEndPercent = UnityEngine.Random.Range(0.4F,0.8F);

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


        float locationTrack = Mathf.Abs((currentPoint.x * xDirection) + (currentPoint.y * yDirection));

        while (progress < GenerationManager.instance.Width)
        {
            float pointInDegrees = (locationTrack + randomStartPoint); //y = sin(x)
            float pointInRadians = pointInDegrees * Mathf.Deg2Rad;

            float hold = locationTrack;

            //float holdSinX = Mathf.Sin(hold * Mathf.PI/2F);//pointInRadians / curveFrequency); //Mathf.sin uses Radians           
            float holdSinX = Mathf.Sin(pointInDegrees / curveFrequency);//pointInRadians / curveFrequency); //Mathf.sin uses Radians           
            int sineHeight = Mathf.RoundToInt(holdSinX * curveStrech); //SineHeight is Y in y = sin(x)

            //Debug.Log("X: " + currentPoint.x + "     Y: " + hold);

            currentPoint.x = currentPoint.x + xDirection + (sineHeight * yDirection);
            currentPoint.y = currentPoint.y + yDirection + (sineHeight * xDirection);
            GenerationManager.instance.GetGameMap().AddTile(WaterTile, currentPoint);

            ApplyThicknessToRiver(currentPoint, thick, xDirection, yDirection);

            locationTrack = Mathf.Abs((currentPoint.x * xDirection) + (currentPoint.y * yDirection));


            progress++;
            if (!_EndAtEdge)
            {
                if (progress >= (GenerationManager.instance.Width * randomCurveEndPercent))
                {
                    progress = int.MaxValue;
                }
            }                           
        }

        if (_EndAtEdge)
        {
            return null;
        }
        else
        {
            StartRiverMidMap srmm = new StartRiverMidMap();
            srmm.startPoint = currentPoint;
            srmm.thickness = thick;
            srmm.xDirection = xDirection;
            srmm.yDirection = yDirection;

            return srmm;
        }
    }

    private void GenerateStraightSection(MapPoint _startPoint, MapPoint? _endPoint = null, int _xDirection = 0, int _yDirection = 0)
    {
        int thick = 2;

        int seed = UnityEngine.Random.Range(0, 5000);
        GenerationManager.instance.GetGameMap().AddTile(WaterTile, _startPoint);

        MapPoint currentPoint = _startPoint;
        int xDirection = _xDirection;
        int yDirection = _yDirection;
        if (xDirection == 0 && yDirection == 0)
        {
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
        }

        ApplyThicknessToRiver(currentPoint, thick, xDirection, yDirection);

        bool generating = true;

        while (generating)
        {
            float xCoord = currentPoint.x / ((float)GenerationManager.instance.Width * 4)* seed;
            float yCoord = (float)currentPoint.y / ((float)GenerationManager.instance.Height * 4)* seed;
            float test = Mathf.PerlinNoise(xCoord, yCoord);
            //Debug.Log(test);
            float test2 = (test * 3F) - 1F;
            int change = Mathf.FloorToInt(test2);

            //Debug.Log(change);

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


    private void ApplyNoiseThicknessToRiver()
    {

    }
}
