using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseGroundTiles
{
	public static void GenerateGroundTiles(GameMap _map, Tile[] _tileSet)
    {
        int numOfExtraTiles = _tileSet.Length - 1;
        float scale = 15;
        float seed = UnityEngine.Random.Range(1000, 10000);


        for (int x = 0; x < GenerationManager.instance.Width; x++)
        {
            for (int y = 0; y < GenerationManager.instance.Height; y++)
            {

                float seededX = x + seed;
                float seededY = y + seed;
                
                var perlin = Mathf.PerlinNoise((seededX / (float)GenerationManager.instance.Width) * scale, (seededY / (float)GenerationManager.instance.Height) * scale);
                if (perlin > .3f)
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
}
