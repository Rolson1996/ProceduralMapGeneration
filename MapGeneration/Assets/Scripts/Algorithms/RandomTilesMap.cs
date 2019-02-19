using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Tilemaps;

namespace Algorithms
{
    class RandomTilesMap
    {

        public RandomTilesMap()
        {

        }

        public void GenerateRandomTileMap(GameMap _map, Tile[] _tileSet)
        {
           
            System.Random rnd = new System.Random();
            int ArraySize = _tileSet.Length;

            for (int iy = 0; iy < GenerationManager.instance.Height; iy++)
            {
                for (int ix = 0; ix < GenerationManager.instance.Width; ix++)
                {
                    int RandomTile = rnd.Next(ArraySize);
                    _map.AddTile(_tileSet[RandomTile], new MapPoint(ix, iy));
                    //Debug.Log("X: " + ix + " | Y: " + iy + " | " + RandomTile + " | " + tiles[RandomTile].ToString());
                }
            }
            
        }
           
    }
}
