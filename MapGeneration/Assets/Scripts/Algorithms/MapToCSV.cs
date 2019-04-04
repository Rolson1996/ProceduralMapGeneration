using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Tilemaps;

public class MapToCSV
{


    public static string[] WriteMapToStringArrays(GameMap _mapToSave)
    {
        Tile waterTile = GenerationManager.instance.tilePool.GetWaterTile();
        Tile shallowsTile = GenerationManager.instance.tilePool.GetShallowsTile();
        Tile roadTile = GenerationManager.instance.tilePool.GetRoadTile();
        Tile secondGround = GenerationManager.instance.tilePool.GetTileSetFromBiomeType(GenerationManager.instance.SelectedMapBiome)[1];

        string[] mapRows = new string[GenerationManager.instance.Width];
        int i = 0;
        int y = GenerationManager.instance.Width - 1;
        while(y >= 0)
        {
            int x = 0;
            string row = "";
            while (x < GenerationManager.instance.Width)
            {
                MapPoint mp = new MapPoint(x, y);

                
                if(_mapToSave.GetTileAtPos(mp) == waterTile)
                {
                    row = row + "W";
                }
                else if(_mapToSave.GetTileAtPos(mp) == shallowsTile)
                {
                    row = row + "S";
                }
                else if(_mapToSave.GetTileAtPos(mp) == roadTile)
                {
                    row = row + "R";
                }
                else if (_mapToSave.DoesTileContainTree(mp))
                {
                    row = row + "T";
                }
                else if (_mapToSave.GetTileAtPos(mp) == secondGround)
                {
                    row = row + "G2";
                }
                else
                {
                    row = row + "G";
                }
               
                x++;

                if (x < GenerationManager.instance.Width)
                {
                    row = row + ",";
                }
            }

            mapRows[i] = row;
            i++;
            y--;
        }

        return mapRows;
    }
}

