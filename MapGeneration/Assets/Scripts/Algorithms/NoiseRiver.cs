using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseRiver 
{
    static Tile WaterTile;

    private static GameMap map;

    struct StartRiverMidMap
    {
        public MapPoint startPoint;
        public int xDirection;
        public int yDirection;
        public int thickness;     
    }
    public static void AttachMap(GameMap _map, Tile _waterTile)
    {
        map = _map;
        WaterTile = _waterTile;
    }
    public static void BuildRiverFromMapEdge(GameMap _map, MapPoint _point, Tile _waterTile)
    {
        map = _map;

        WaterTile = _waterTile;

        int riverType = UnityEngine.Random.Range(0, 5);
        
        switch (riverType)
        {
            case 0: // Sine -> Edge
                GenerateSineSection(_point, true);
                //Debug.Log("Sine -> Edge");
                break;
            case 1: // Sine -> Not Edge
                GenerateSineSection(_point, false);
                //Debug.Log("Sine -> Not Edge");
                break;
            case 2: // Sine -> Straight
                StartRiverMidMap? SineEndPoint = GenerateSineSection(_point, true);
                if (SineEndPoint != null)
                {
                    GenerateStraightSection(SineEndPoint.Value.startPoint, true, SineEndPoint.Value.xDirection, SineEndPoint.Value.yDirection);
                }
               // Debug.Log("Sine -> Straight");
                break;
            case 3: // Straight -> Edge
                GenerateStraightSection(_point, true);
                //Debug.Log("Straight -> Edge");
                break;
            case 4: // Straight -> Not Edge
                //Debug.Log("Straight -> Not Edge");
                GenerateStraightSection(_point, false);
                break; 
            default: // Sine -> Edge
                GenerateSineSection(_point, true);
                //Debug.Log("default:  Sine -> Edge");
                break;
        }
    }

    private static StartRiverMidMap? GenerateSineSection(MapPoint _startPoint, bool _endAtEdge = true)
    {
        float curveAmplitude = UnityEngine.Random.Range(1F, 4F); //
        float curveFrequency = 7F;

        int randomCurveEnd = Mathf.FloorToInt(GenerationManager.instance.Width * UnityEngine.Random.Range(0.4F, 0.8F));

        float randomSineStartPoint = UnityEngine.Random.Range(-180, 180);
        int progress = 1;

        int thick = 2;

        map.AddTile(WaterTile, _startPoint);
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

        //ApplyThicknessToRiver(currentPoint, thick, xDirection, yDirection);
        CreateNoiseBlob.CreateBlobAtPosition(map, currentPoint, thick, WaterTile);

        float locationTrack = Mathf.Abs((currentPoint.x * xDirection) + (currentPoint.y * yDirection));

        while (progress < GenerationManager.instance.Width)
        {
            float pointInDegrees = (locationTrack + randomSineStartPoint); //y = sin(x)

            float xWithFreq = pointInDegrees / curveFrequency;
            float holdSinX = Mathf.Sin(xWithFreq);       
            int sineHeight = Mathf.RoundToInt(holdSinX * curveAmplitude / 2); //SineHeight is Y in y = sin(x)
            //int sineHeight = Mathf.RoundToInt(holdSinX * curveStrech * (curveStrech / curveFrequency)); //SineHeight is Y in y = sin(x)


            //int sineHeight = Mathf.RoundToInt(holdSinX * curveStrech - curveFrequency); //- curve frequency makes the river diagonal

            currentPoint.x = currentPoint.x + xDirection + (sineHeight * yDirection);
            currentPoint.y = currentPoint.y + yDirection + (sineHeight * xDirection);
            map.AddTile(WaterTile, currentPoint);

            //ApplyThicknessToRiver(currentPoint, thick, xDirection, yDirection);
            CreateNoiseBlob.CreateBlobAtPosition(map, currentPoint, thick, WaterTile);

            locationTrack = Mathf.Abs((currentPoint.x * xDirection) + (currentPoint.y * yDirection));

            progress++;
            if (!_endAtEdge)
            {
                if (progress >= randomCurveEnd)
                {
                    progress = int.MaxValue;
                }
            }                           
        }

        if (_endAtEdge)
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

    public static void GenerateStraightSection(MapPoint _startPoint, bool _endAtEdge = true, int _xDirection = 0, int _yDirection = 0)
    {
        int thick = 3;
        int seed = UnityEngine.Random.Range(1000, 10000);
        int randomCurveEnd = Mathf.FloorToInt(GenerationManager.instance.Width * UnityEngine.Random.Range(0.4F, 0.8F));
        int progress = 1;

        map.AddTile(WaterTile, _startPoint);

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
            float xCoord = currentPoint.x / ((float)GenerationManager.instance.Width * 4) * seed;
            float yCoord = currentPoint.y / ((float)GenerationManager.instance.Height * 4) * seed;        

            float perlin = Mathf.PerlinNoise(xCoord, yCoord);

            float floatChange = (perlin * 3F) - 1F; // range is now between -1 and 1
            int change = Mathf.FloorToInt(floatChange); //-1, 0 or 1
          
            currentPoint.x = currentPoint.x + xDirection + (change * yDirection);
            currentPoint.y = currentPoint.y + yDirection + (change * xDirection);

            map.AddTile(WaterTile, currentPoint);

            //ApplyThicknessToRiver(currentPoint, thick, xDirection, yDirection);

            CreateNoiseBlob.CreateBlobAtPosition(map, currentPoint, thick, WaterTile);

            progress++;
            if (!_endAtEdge)
            {
                if (progress >= randomCurveEnd)
                {
                    generating = false;
                }
            }
            else if (currentPoint.x < 0 || currentPoint.y < 0 || currentPoint.x > GenerationManager.instance.Width - 1 || currentPoint.y > GenerationManager.instance.Height - 1)
            {
                generating = false;
            }
        }
    }

    private static void ApplyThicknessToRiver(MapPoint _riverCentre, int _radius, int _xDirection, int _yDirection)
    {
        for (int i = 1; i <= _radius; i++)
        {
            MapPoint sidePos = new MapPoint();
            sidePos.x = _riverCentre.x + (i * _yDirection);
            sidePos.y = _riverCentre.y + (i * _xDirection);
            map.AddTile(WaterTile, sidePos);

            MapPoint sideNeg = new MapPoint();
            sideNeg.x = _riverCentre.x + (-i * _yDirection);
            sideNeg.y = _riverCentre.y + (-i * _xDirection);
            map.AddTile(WaterTile, sideNeg);
        }
    } 
}
