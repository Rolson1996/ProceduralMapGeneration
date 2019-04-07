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

        BiomeTileSet biome = GenerationManager.instance.GetCurrentBiomeTileSet();

        Tile waterTile = biome.Water;
        Tile shallowsTile = biome.Shallows;
        Tile roadTile = biome.Road;

        Tile groundTile = biome.GroundTiles[0];
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
                else if (mapTile == "2G")
                {
                    generationMap.AddTile(groundTile2, mp);
                }
                else if (mapTile == "3G")
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

