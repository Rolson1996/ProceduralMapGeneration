using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoastLineGenerator
{

    static Tile WaterTile;
    private static GameMap map;

    public static int GenerateCoastline(GameMap _generationMap, Tile _waterTile)
    {
        map = _generationMap;
        WaterTile = _waterTile;

        int coastlineMapDepth = Mathf.FloorToInt(GenerationManager.instance.Width * UnityEngine.Random.Range(0.2F, 0.5F));
        int coastlineSide = UnityEngine.Random.Range(0, 4);
        Debug.Log(coastlineSide);

        switch (coastlineSide)
        {
            //X 0 X
            //1 X 3
            //X 2 X

            case 0:
                NoiseRiver.GenerateStraightSection(new MapPoint(0, coastlineMapDepth));
                ApplyWaterVertical(0, 1);
                break;
            case 1:
                NoiseRiver.GenerateStraightSection(new MapPoint(coastlineMapDepth, 0));
                ApplyWaterHorizontal(0, 1);
                break;
            case 2:
                NoiseRiver.GenerateStraightSection(new MapPoint(GenerationManager.instance.Width, GenerationManager.instance.Height - coastlineMapDepth));
                ApplyWaterVertical(GenerationManager.instance.Height, -1);
                break;
            case 3:
                NoiseRiver.GenerateStraightSection(new MapPoint(GenerationManager.instance.Width - coastlineMapDepth, GenerationManager.instance.Height));
                ApplyWaterHorizontal(GenerationManager.instance.Width, -1);
                break;
            default:
                NoiseRiver.GenerateStraightSection(new MapPoint(0, coastlineMapDepth));
                break;
        }

        return coastlineSide;
    }

    public static void ApplyWaterHorizontal(int _startValue, int _leftOrRight)
    {
        int y = 0;
        while (y < GenerationManager.instance.Height)
        {
            int x = _startValue;
            bool hitWater = false;

            while (!hitWater)
            {
                MapPoint mp = new MapPoint(x, y);
                if (map.GetTileAtPos(mp) != WaterTile)
                {
                    map.AddTile(WaterTile, mp);
                }
                else
                {
                    hitWater = true;
                }
                x = x + _leftOrRight;
            }
            y++;
        }
    }

    public static void ApplyWaterVertical(int _startValue, int _upOrDown)
    {
        int x = 0;
        while (x < GenerationManager.instance.Width)
        {
            int y = _startValue;
            bool hitWater = false;

            while (!hitWater)
            {
                MapPoint mp = new MapPoint(x, y);
                if(map.GetTileAtPos(mp) != WaterTile)
                {
                    map.AddTile(WaterTile, mp);
                }
                else
                {
                    hitWater = true;
                }
                y = y + _upOrDown;
            }
            x++;
        }
    }
        

}

