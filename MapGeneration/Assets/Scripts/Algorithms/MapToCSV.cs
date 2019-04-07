using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Tilemaps;

public class MapToCSV
{


    public static string[] WriteMapToStringArrays(GameMap _mapToSave)
    {
        BiomeTileSet biome = GenerationManager.instance.GetCurrentBiomeTileSet();

        Tile waterTile = biome.Water;
        Tile shallowsTile = biome.Shallows;
        Tile roadTile = biome.Road;
        Tile groundTile2 = null;
        Tile groundTile3 = null;
        if (biome.GroundTiles.Length > 1)
        {
            groundTile2 = biome.GroundTiles[1];
        }
        if (biome.GroundTiles.Length > 2)
        {
            groundTile3 = biome.GroundTiles[2];
        }

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
                else if (_mapToSave.GetTileAtPos(mp) == groundTile2)
                {
                    row = row + "2G";
                }
                else if (_mapToSave.GetTileAtPos(mp) == groundTile3)
                {
                    row = row + "3G";
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

