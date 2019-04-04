using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Tilemaps;

public class CSVToMap
{
    private static GameMap generationMap;

    public static void BuildMapFromStringArrayList(List<string[]> mapInStrings, MapBiome biomeType)
    {
        generationMap = GenerationManager.instance.GetGameMap();
        GenerationManager.instance.Height = mapInStrings.Count;
        GenerationManager.instance.Width = mapInStrings.Count;

        switch (mapInStrings.Count)
        {
            case 72:
                GenerationManager.instance.SetTopDownPos(36F, 36.5F, 65F);
                break;
            case 96:
                GenerationManager.instance.SetTopDownPos(47.7F, 48.3F, 85F);
                break;
            case 120:
                GenerationManager.instance.SetTopDownPos(58.2F, 60F, 105F);
                break;
            case 144:
                GenerationManager.instance.SetTopDownPos(69.3F, 72.4F, 130F);
                break;
            case 200:
                GenerationManager.instance.SetTopDownPos(98.1F, 100F, 175F);
                break;
            case 255:
                GenerationManager.instance.SetTopDownPos(123.3F, 126.5F, 225F);
                break;
            default:

                break;
        }



        generationMap.CreateEmptyMap(mapInStrings.Count);
        GenerationManager.instance.forestGenerator.DestroyOldTrees();

        Tile groundTile = GenerationManager.instance.tilePool.GetTileSetFromBiomeType(biomeType)[0];
        Tile groundTile2 = GenerationManager.instance.tilePool.GetTileSetFromBiomeType(biomeType)[1];
        Tile waterTile = GenerationManager.instance.tilePool.GetWaterTile();
        Tile roadTile = GenerationManager.instance.tilePool.GetRoadTile();
        Tile shallowsTile = GenerationManager.instance.tilePool.GetShallowsTile();

        int y = mapInStrings.Count - 1;
        foreach(string[] row in mapInStrings)
        {
            int x = 0;//
            foreach(string mapTile in row)
            {
                MapPoint mp = new MapPoint(x, y);
                if(mapTile == "G")
                {
                    generationMap.AddTile(groundTile, mp);
                }
                else if (mapTile == "G2")
                {
                    generationMap.AddTile(groundTile2, mp);
                }
                else if (mapTile == "T")
                {
                    generationMap.AddTile(groundTile, mp);
                    generationMap.AddTree(0, mp);
                }
                else if (mapTile == "W")
                {
                    generationMap.AddTile(waterTile, mp);
                }
                else if (mapTile == "S")
                {
                    generationMap.AddTile(shallowsTile, mp);
                }
                else if (mapTile == "R")
                {
                    generationMap.AddTile(roadTile, mp);
                }
                x++;

            }
            y--;
        }

        generationMap.ApplySandNextToWater();
        generationMap.GenerateMap();
        GenerationManager.instance.MoveGamePlayCam();
    }
}

