using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NoiseGroundTiles
{
	
	public void GenerateGroundTiles(GameMap _map, Tile[] _tileSet)
    {
        int numOfExtraTiles = _tileSet.Length - 1;
        float seed = UnityEngine.Random.Range(0, 10000);


        for (int x = 0; x < GenerationManager.instance.Width; x++)
        {
            for (int y = 0; y < GenerationManager.instance.Height; y++)
            {
                var perlin = Mathf.PerlinNoise((x / ((float)GenerationManager.instance.Width) * seed), y / ((float)GenerationManager.instance.Height) * seed);
                if (perlin > .1f)
                {
                    _map.AddTile(_tileSet[0], new MapPoint(x, y));
                }
                else
                {
                    _map.AddTile(_tileSet[1], new MapPoint(x, y));
                }
               
            }
        }
    }
}
