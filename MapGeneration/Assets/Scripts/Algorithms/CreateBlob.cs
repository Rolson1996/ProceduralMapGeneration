using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateNoiseBlob
{

    public static void CreateBlobAtPosition(GameMap _map, MapPoint _point, int _radius, Tile _tile)
    {

        float scale = 20;
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
                if (perlin < .6f)
                {
                    _map.AddTile(_tile, new MapPoint(locationX, locationY));
                }                
            }
        }
    }




}

