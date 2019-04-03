using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;


public struct SmallIsland
{
    public MapPoint centrePoint;
    public int Radius;

    public SmallIsland(MapPoint _mp, int _r)
    {
        centrePoint = _mp;
        Radius = _r;
    }
}

public class NoiseIslands
{
    public static void GenerateIslandsDirectNoise(GameMap _map, Tile[] _tileSet, Tile _water)
    {
        int numOfExtraTiles = _tileSet.Length - 1;
        float scale = 4;
        float seed = UnityEngine.Random.Range(1000, 10000);


        for (int x = 0; x < GenerationManager.instance.Width; x++)
        {
            for (int y = 0; y < GenerationManager.instance.Height; y++)
            {

                float seededX = x + seed;
                float seededY = y + seed;

                var perlin = Mathf.PerlinNoise((seededX / (float)GenerationManager.instance.Width) * scale, (seededY / (float)GenerationManager.instance.Height) * scale);
                if (perlin > .35f)
                {
                    _map.AddTile(_water, new MapPoint(x, y));
                }
                else if (perlin > .1f)
                {
                    _map.AddTile(_tileSet[0], new MapPoint(x, y));
                }
                else
                {
                    //add blob at location
                    _map.AddTile(_tileSet[1], new MapPoint(x, y));
                }

            }
        }
    }

    public static void CreateIndividualIsland(GameMap _map, MapPoint _point, int _radius, Tile _tile, Tile[] _tileSet)
    {

        float scale = 7;
        float seed = UnityEngine.Random.Range(1000, 10000);



        for (int x = -_radius; x < _radius; x++)
        {
            for (int y = -_radius; y < _radius; y++)
            {

                int locationX = _point.x + x;
                int locationY = _point.y + y;

                float seededX = locationX + seed;
                float seededY = locationY + seed;

                var perlin = Mathf.PerlinNoise((seededX / (float)GenerationManager.instance.Width) * scale, (seededY / (float)GenerationManager.instance.Height) * scale);
                if (perlin > .5f)
                {
                    _map.AddTile(_tile, new MapPoint(locationX, locationY));
                }
            }
        }

    }

    public static void CreateCircleIsland(GameMap _map, MapPoint _centrePoint, int _radius, Tile _tile)
    {
        int radiusSquared = _radius * _radius;
        for (int x = -_radius; x < _radius; x++)
        {
            for (int y = -_radius; y < _radius; y++)
            {
                float distanceFromCentreSquared = (x * x) + (y * y);
                
                if(distanceFromCentreSquared < radiusSquared)
                {
                    _map.AddTile(_tile, new MapPoint(_centrePoint.x + x, _centrePoint.y + y));
                }
                
            }
        }
    }

    public static void CreateIslandFromMultipleCircles(GameMap _map, MapPoint _centrePoint, int _radius, Tile _tile)
    {
        int numberOfCircles = UnityEngine.Random.Range(4, 10);
        //Debug.Log("Number of Circles: " + numberOfCircles);

        int c = 0;
        while(c < numberOfCircles)
        {
            int x = UnityEngine.Random.Range(-_radius, _radius);
            int y = UnityEngine.Random.Range(-_radius, _radius);

            CreateCircleIsland(_map, new MapPoint(_centrePoint.x + x, _centrePoint.y + y), _radius, _tile);
            c++;
        }
    }

    public static bool IsSmallIslandTooClose(SmallIsland _islandToAdd, SmallIsland[] _existingIslands)
    {
        int toAddX = _islandToAdd.centrePoint.x;
        int toAddY = _islandToAdd.centrePoint.y;
        foreach (SmallIsland eSI in _existingIslands)
        {
            int differenceX = Math.Abs(toAddX - eSI.centrePoint.x);
            int differenceY = Math.Abs(toAddY - eSI.centrePoint.y);
            int distanceBetweenCentreSqaured = (differenceX * differenceX) + (differenceY * differenceY);

            int minDistanceBetween = (eSI.Radius * 2) + (_islandToAdd.Radius * 2);
            int minDistanceBetweenSquared = minDistanceBetween * minDistanceBetween;

            if(minDistanceBetweenSquared > distanceBetweenCentreSqaured)
            {                
                return true;
            }
        }
        //Debug.Log("Fine");
        return false;
    }
}

