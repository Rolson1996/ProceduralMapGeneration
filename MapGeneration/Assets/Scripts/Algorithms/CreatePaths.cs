using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreatePaths
{
    private static GameMap generationMap;
    private static Tile roadTile;
    private static Tile shallowsTile;
    private static Tile waterTile;

    public static void AttachMap(GameMap _map)
    {
        generationMap = _map;
    }
    public static void AttachTiles(Tile _road, Tile _shallows, Tile _water)
    {
        roadTile = _road;
        shallowsTile = _shallows;
        waterTile = _water;
    }

    public static void CreatePathBetween2Points(MapPoint _start, MapPoint _end)
    {

        float xDifference = _end.x - _start.x;
        float yDifference = _end.y - _start.y;

        float xDifferenceAbs = Math.Abs(xDifference);
        float yDifferenceAbs = Math.Abs(yDifference);


        int xDirection = 1;
        int yDirection = 1;

        if(xDifference < 0)
        {
            xDirection = -1;
        }

        if (yDifference < 0)
        {
            yDirection = -1;
        }


        if (xDifferenceAbs > yDifferenceAbs)
        {
            generationMap.AddTile(roadTile, _start);

            int xProgress = 0;
            int yProgress = 0;

            while (xProgress < xDifferenceAbs)
            {
                float progressPerct = (xProgress / xDifferenceAbs);
                yProgress = (int)(progressPerct * yDifferenceAbs);
                xProgress++;

                MapPoint mp = new MapPoint(_start.x + (xProgress * xDirection), _start.y + (yProgress * yDirection));

                CreatePathBlobAtPosition(generationMap, mp);
                //generationMap.AddTile(roadTile, mp);
            }
        }
        else
        {
            generationMap.AddTile(roadTile, _start);

            int xProgress = 0;
            int yProgress = 0;

            while(yProgress < yDifferenceAbs)
            {
                float progressPerct = (yProgress / yDifferenceAbs);
                xProgress = (int)(progressPerct * xDifferenceAbs);
                yProgress++;

                MapPoint mp = new MapPoint(_start.x + (xProgress * xDirection), _start.y + (yProgress * yDirection));

                CreatePathBlobAtPosition(generationMap, mp);
                //generationMap.AddTile(roadTile, mp);
            }

        }

    }

    public static void CreatePathBlobAtPosition(GameMap _map, MapPoint _point)
    {

        float scale = 20;
        float seed = UnityEngine.Random.Range(1000, 10000);

        for (int x = -1; x < 1; x++)
        {
            for (int y = -1; y < 1; y++)
            {

                int locationX = _point.x + x;
                int locationY = _point.y + y;

                float seededX = locationX + seed;
                float seededY = locationY + seed;

                var perlin = Mathf.PerlinNoise((seededX / (float)GenerationManager.instance.Width) * scale, (seededY / (float)GenerationManager.instance.Height) * scale);
                if (perlin < .6f)
                {
                    MapPoint mp = new MapPoint(locationX, locationY);
                    if (_map.GetTileAtPos(mp) == waterTile || _map.GetTileAtPos(mp) == shallowsTile)
                    {
                        _map.AddTile(shallowsTile, mp);
                    }
                    else
                    {
                        _map.AddTile(roadTile, mp);
                    }
                    
                }
            }
        }
    }


}

