using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DifferentSizeIslands
{

    public static void GenerateTinyIslands(GameMap _gameMap, Tile _tile)
    {
        //Top Left
        int x1 = UnityEngine.Random.Range(15, 24);
        int y1 = UnityEngine.Random.Range(15, 24);
        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x1, y1), 9, _tile);


        //Bottom Right
        int x2 = UnityEngine.Random.Range(48, 57);
        int y2 = UnityEngine.Random.Range(48, 57);
        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x2, y2), 9, _tile);

        //Top Right
        int IslandCountTR = UnityEngine.Random.Range(0, 3);

        if (IslandCountTR == 1)
        {
            int x = UnityEngine.Random.Range(42, 66);
            int y = UnityEngine.Random.Range(6, 30);
            NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x, y), UnityEngine.Random.Range(3, 5), _tile);
        }
        else if (IslandCountTR > 1)
        {
            SmallIsland[] SmallIslands = new SmallIsland[IslandCountTR];
            int i = 0;
            while (i < IslandCountTR)
            {
                int Attempts = 0;
                bool IslandGenerated = false;
                while (!IslandGenerated)
                {
                    int x = UnityEngine.Random.Range(42, 66);
                    int y = UnityEngine.Random.Range(6, 30);

                    MapPoint mp = new MapPoint(x, y);

                    int smallRadius = UnityEngine.Random.Range(3, 4);

                    SmallIsland smallIsland = new SmallIsland(mp, smallRadius);

                    if (!NoiseIslands.IsSmallIslandTooClose(smallIsland, SmallIslands))
                    {
                        SmallIslands[i] = smallIsland;

                        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, mp, smallRadius, _tile);
                        IslandGenerated = true;
                    }
                    else
                    {
                        Attempts++;
                        Debug.Log("TR Too Close || Attempt: " + Attempts);

                        if (Attempts >= 5)
                        {
                            break;
                        }
                    }
                }

                i++;
            }
        }

        //Bottom Left
        int IslandCountBL = UnityEngine.Random.Range(0, 3);

        if (IslandCountBL == 1)
        {
            int x = UnityEngine.Random.Range(6, 30);
            int y = UnityEngine.Random.Range(42, 66);
            NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x, y), UnityEngine.Random.Range(3, 5), _tile);
        }
        else if (IslandCountBL > 1)
        {
            SmallIsland[] SmallIslands = new SmallIsland[IslandCountBL];
            int i = 0;
            while (i < IslandCountBL)
            {
                int Attempts = 0;
                bool IslandGenerated = false;
                while (!IslandGenerated)
                {
                    int x = UnityEngine.Random.Range(6, 30);
                    int y = UnityEngine.Random.Range(42, 66);

                    MapPoint mp = new MapPoint(x, y);

                    int smallRadius = UnityEngine.Random.Range(3, 4);

                    SmallIsland smallIsland = new SmallIsland(mp, smallRadius);

                    if (!NoiseIslands.IsSmallIslandTooClose(smallIsland, SmallIslands))
                    {
                        SmallIslands[i] = smallIsland;

                        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, mp, smallRadius, _tile);
                        IslandGenerated = true;
                    }
                    else
                    {
                        Attempts++;
                        Debug.Log("BL Too Close || Attempt: " + Attempts);

                        if (Attempts >= 5)
                        {
                            break;
                        }
                    }
                }
                i++;
            }
        }
    }

    public static void GenerateSmallIslands(GameMap _gameMap, Tile _tile)
    {
        //Top Left
        int x1 = UnityEngine.Random.Range(20, 30);
        int y1 = UnityEngine.Random.Range(20, 30);
        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x1, y1), 12, _tile);


        //Bottom Right
        int x2 = UnityEngine.Random.Range(66, 76);
        int y2 = UnityEngine.Random.Range(66, 76);
        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x2, y2), 12, _tile);

        //Top Right
        int IslandCountTR = UnityEngine.Random.Range(1, 4);

        if (IslandCountTR == 1)
        {
            int x = UnityEngine.Random.Range(56, 86);
            int y = UnityEngine.Random.Range(10, 42);
            NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x, y), UnityEngine.Random.Range(4, 6), _tile);
        }
        else if (IslandCountTR > 1)
        {
            SmallIsland[] SmallIslands = new SmallIsland[IslandCountTR];
            int i = 0;
            while (i < IslandCountTR)
            {
                int Attempts = 0;
                bool IslandGenerated = false;
                while (!IslandGenerated)
                {
                    int x = UnityEngine.Random.Range(56, 90);
                    int y = UnityEngine.Random.Range(6, 42);

                    MapPoint mp = new MapPoint(x, y);

                    int smallRadius = UnityEngine.Random.Range(4, 5);

                    SmallIsland smallIsland = new SmallIsland(mp, smallRadius);

                    if (!NoiseIslands.IsSmallIslandTooClose(smallIsland, SmallIslands))
                    {
                        SmallIslands[i] = smallIsland;

                        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, mp, smallRadius, _tile);
                        IslandGenerated = true;
                    }
                    else
                    {
                        Attempts++;
                        Debug.Log("TR Too Close || Attempt: " + Attempts);

                        if (Attempts >= 5)
                        {
                            break;
                        }
                    }
                }

                i++;
            }
        }

        //Bottom Left
        int IslandCountBL = UnityEngine.Random.Range(1, 4);

        if (IslandCountBL == 1)
        {
            int x = UnityEngine.Random.Range(6, 42);
            int y = UnityEngine.Random.Range(56, 86);
            NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x, y), UnityEngine.Random.Range(4, 6), _tile);
        }
        else if (IslandCountBL > 1)
        {
            SmallIsland[] SmallIslands = new SmallIsland[IslandCountBL];
            int i = 0;
            while (i < IslandCountBL)
            {
                int Attempts = 0;
                bool IslandGenerated = false;
                while (!IslandGenerated)
                {
                    int x = UnityEngine.Random.Range(6, 42);
                    int y = UnityEngine.Random.Range(56, 90);

                    MapPoint mp = new MapPoint(x, y);

                    int smallRadius = UnityEngine.Random.Range(4, 5);

                    SmallIsland smallIsland = new SmallIsland(mp, smallRadius);

                    if (!NoiseIslands.IsSmallIslandTooClose(smallIsland, SmallIslands))
                    {
                        SmallIslands[i] = smallIsland;

                        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, mp, smallRadius, _tile);
                        IslandGenerated = true;
                    }
                    else
                    {
                        Attempts++;
                        Debug.Log("BL Too Close || Attempt: " + Attempts);

                        if (Attempts >= 5)
                        {
                            break;
                        }
                    }
                }
                i++;
            }
        }
    }

    public static void GenerateMediumIslands(GameMap _gameMap, Tile _tile)
    {
        //Top Left
        int x1 = UnityEngine.Random.Range(30, 35);
        int y1 = UnityEngine.Random.Range(30, 35);
        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x1, y1), 14, _tile);


        //Bottom Right
        int x2 = UnityEngine.Random.Range(85, 90);
        int y2 = UnityEngine.Random.Range(85, 90);
        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x2, y2), 14, _tile);

        //Top Right
        int IslandCountTR = UnityEngine.Random.Range(1, 4);

        if (IslandCountTR == 1)
        {
            int x = UnityEngine.Random.Range(76, 104);
            int y = UnityEngine.Random.Range(16, 44);
            NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x, y), UnityEngine.Random.Range(6, 8), _tile);
        }
        else if (IslandCountTR > 1)
        {
            SmallIsland[] SmallIslands = new SmallIsland[IslandCountTR];
            int i = 0;
            while (i < IslandCountTR)
            {
                int Attempts = 0;
                bool IslandGenerated = false;
                while (!IslandGenerated)
                {
                    int x = UnityEngine.Random.Range(70, 110);
                    int y = UnityEngine.Random.Range(10, 50);

                    MapPoint mp = new MapPoint(x, y);

                    int smallRadius = UnityEngine.Random.Range(4, 5);

                    SmallIsland smallIsland = new SmallIsland(mp, smallRadius);

                    if (!NoiseIslands.IsSmallIslandTooClose(smallIsland, SmallIslands))
                    {
                        SmallIslands[i] = smallIsland;

                        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, mp, smallRadius, _tile);
                        IslandGenerated = true;
                    }
                    else
                    {
                        Attempts++;
                        Debug.Log("TR Too Close || Attempt: " + Attempts);

                        if (Attempts >= 5)
                        {
                            break;
                        }
                    }
                }

                i++;
            }
        }

        //Bottom Left
        int IslandCountBL = UnityEngine.Random.Range(1, 4);

        if (IslandCountBL == 1)
        {
            int x = UnityEngine.Random.Range(16, 44);
            int y = UnityEngine.Random.Range(76, 104);
            NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x, y), UnityEngine.Random.Range(6, 8), _tile);
        }
        else if (IslandCountBL > 1)
        {
            SmallIsland[] SmallIslands = new SmallIsland[IslandCountBL];
            int i = 0;
            while (i < IslandCountBL)
            {
                int Attempts = 0;
                bool IslandGenerated = false;
                while (!IslandGenerated)
                {
                    int x = UnityEngine.Random.Range(10, 50);
                    int y = UnityEngine.Random.Range(70, 110);

                    MapPoint mp = new MapPoint(x, y);

                    int smallRadius = UnityEngine.Random.Range(4, 5);

                    SmallIsland smallIsland = new SmallIsland(mp, smallRadius);

                    if (!NoiseIslands.IsSmallIslandTooClose(smallIsland, SmallIslands))
                    {
                        SmallIslands[i] = smallIsland;

                        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, mp, smallRadius, _tile);
                        IslandGenerated = true;
                    }
                    else
                    {
                        Attempts++;
                        Debug.Log("BL Too Close || Attempt: " + Attempts);

                        if (Attempts >= 5)
                        {
                            break;
                        }
                    }
                }
                i++;
            }
        }
    }

    public static void GenerateLargeIslands(GameMap _gameMap, Tile _tile)
    {
        //Top Left
        int x1 = UnityEngine.Random.Range(30, 47);
        int y1 = UnityEngine.Random.Range(30, 47);
        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x1, y1), 14, _tile);


        //Bottom Right
        int x2 = UnityEngine.Random.Range(97, 114);
        int y2 = UnityEngine.Random.Range(97, 114);
        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x2, y2), 14, _tile);

        //Top Right
        int IslandCountTR = UnityEngine.Random.Range(1, 4);

        if (IslandCountTR == 1)
        {
            int x = UnityEngine.Random.Range(90, 126);
            int y = UnityEngine.Random.Range(18, 54);
            NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x, y), UnityEngine.Random.Range(7, 9), _tile);
        }
        else if (IslandCountTR > 1)
        {
            SmallIsland[] SmallIslands = new SmallIsland[IslandCountTR];
            int i = 0;
            while (i < IslandCountTR)
            {
                int Attempts = 0;
                bool IslandGenerated = false;
                while (!IslandGenerated)
                {
                    int x = UnityEngine.Random.Range(84, 132);
                    int y = UnityEngine.Random.Range(12, 60);

                    MapPoint mp = new MapPoint(x, y);

                    int smallRadius = UnityEngine.Random.Range(5, 6);

                    SmallIsland smallIsland = new SmallIsland(mp, smallRadius);

                    if (!NoiseIslands.IsSmallIslandTooClose(smallIsland, SmallIslands))
                    {
                        SmallIslands[i] = smallIsland;

                        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, mp, smallRadius, _tile);
                        IslandGenerated = true;
                    }
                    else
                    {
                        Attempts++;
                        Debug.Log("TR Too Close || Attempt: " + Attempts);

                        if (Attempts >= 5)
                        {
                            break;
                        }
                    }
                }

                i++;
            }
        }

        //Bottom Left
        int IslandCountBL = UnityEngine.Random.Range(1, 4);

        if (IslandCountBL == 1)
        {
            int x = UnityEngine.Random.Range(18, 54);
            int y = UnityEngine.Random.Range(90, 126);
            NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x, y), UnityEngine.Random.Range(7, 9), _tile);
        }
        else if (IslandCountBL > 1)
        {
            SmallIsland[] SmallIslands = new SmallIsland[IslandCountBL];
            int i = 0;
            while (i < IslandCountBL)
            {
                int Attempts = 0;
                bool IslandGenerated = false;
                while (!IslandGenerated)
                {
                    int x = UnityEngine.Random.Range(12, 60);
                    int y = UnityEngine.Random.Range(84, 132);

                    MapPoint mp = new MapPoint(x, y);

                    int smallRadius = UnityEngine.Random.Range(5, 6);

                    SmallIsland smallIsland = new SmallIsland(mp, smallRadius);

                    if (!NoiseIslands.IsSmallIslandTooClose(smallIsland, SmallIslands))
                    {
                        SmallIslands[i] = smallIsland;

                        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, mp, smallRadius, _tile);
                        IslandGenerated = true;
                    }
                    else
                    {
                        Attempts++;
                        Debug.Log("BL Too Close || Attempt: " + Attempts);

                        if (Attempts >= 5)
                        {
                            break;
                        }
                    }
                }
                i++;
            }
        }
    }

    public static void GenerateHugeIslands(GameMap _gameMap, Tile _tile)
    {
        //Top Left
        int x1 = UnityEngine.Random.Range(40, 65);
        int y1 = UnityEngine.Random.Range(40, 65);
        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x1, y1), 19, _tile);


        //Bottom Right
        int x2 = UnityEngine.Random.Range(135, 160);
        int y2 = UnityEngine.Random.Range(135, 160);
        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x2, y2), 19, _tile);

        //Top Right
        int IslandCountTR = UnityEngine.Random.Range(2, 6);

        if (IslandCountTR == 2)
        {
            SmallIsland[] SmallIslands = new SmallIsland[IslandCountTR];
            int i = 0;
            while (i < IslandCountTR)
            {
                int Attempts = 0;
                bool IslandGenerated = false;
                while (!IslandGenerated)
                {
                    int x = UnityEngine.Random.Range(120, 180);
                    int y = UnityEngine.Random.Range(20, 80);

                    MapPoint mp = new MapPoint(x, y);

                    //int smallRadius = UnityEngine.Random.Range(5, 6);

                    SmallIsland smallIsland = new SmallIsland(mp, 10);

                    if (!NoiseIslands.IsSmallIslandTooClose(smallIsland, SmallIslands))
                    {
                        SmallIslands[i] = smallIsland;

                        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x, y), 10, _tile);
                        IslandGenerated = true;
                    }
                    else
                    {
                        Attempts++;
                        Debug.Log("TR Too Close || Attempt: " + Attempts);

                        if (Attempts >= 5)
                        {
                            break;
                        }
                    }
                }
                i++;
            }
        }
        else if (IslandCountTR > 2)
        {
            SmallIsland[] SmallIslands = new SmallIsland[IslandCountTR];
            int i = 0;
            while (i < IslandCountTR)
            {
                int Attempts = 0;
                bool IslandGenerated = false;
                while (!IslandGenerated)
                {
                    int x = UnityEngine.Random.Range(112, 188);
                    int y = UnityEngine.Random.Range(12, 88);

                    MapPoint mp = new MapPoint(x, y);

                    int smallRadius = UnityEngine.Random.Range(5, 6);

                    SmallIsland smallIsland = new SmallIsland(mp, smallRadius);

                    if (!NoiseIslands.IsSmallIslandTooClose(smallIsland, SmallIslands))
                    {
                        SmallIslands[i] = smallIsland;

                        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, mp, smallRadius, _tile);
                        IslandGenerated = true;
                    }
                    else
                    {
                        Attempts++;
                        Debug.Log("TR Too Close || Attempt: " + Attempts);

                        if (Attempts >= 5)
                        {
                            break;
                        }
                    }
                }

                i++;
            }
        }

        //Bottom Left
        int IslandCountBL = UnityEngine.Random.Range(2, 6);

        if (IslandCountBL == 2)
        {
            SmallIsland[] SmallIslands = new SmallIsland[IslandCountTR];
            int i = 0;
            while (i < IslandCountBL)
            {
                int Attempts = 0;
                bool IslandGenerated = false;
                while (!IslandGenerated)
                {
                    int x = UnityEngine.Random.Range(20, 80);
                    int y = UnityEngine.Random.Range(120, 180);

                    MapPoint mp = new MapPoint(x, y);

                    SmallIsland smallIsland = new SmallIsland(mp, 10);

                    if (!NoiseIslands.IsSmallIslandTooClose(smallIsland, SmallIslands))
                    {
                        SmallIslands[i] = smallIsland;

                        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, new MapPoint(x, y), 10, _tile);
                        IslandGenerated = true;
                    }
                    else
                    {
                        Attempts++;
                        Debug.Log("TR Too Close || Attempt: " + Attempts);

                        if (Attempts >= 5)
                        {
                            break;
                        }
                    }
                }

                i++;
            }
        }
        else if (IslandCountBL > 2)
        {
            SmallIsland[] SmallIslands = new SmallIsland[IslandCountBL];
            int i = 0;
            while (i < IslandCountBL)
            {
                int Attempts = 0;
                bool IslandGenerated = false;
                while (!IslandGenerated)
                {
                    int x = UnityEngine.Random.Range(12, 88);
                    int y = UnityEngine.Random.Range(112, 188);

                    MapPoint mp = new MapPoint(x, y);

                    int smallRadius = UnityEngine.Random.Range(5, 6);

                    SmallIsland smallIsland = new SmallIsland(mp, smallRadius);

                    if (!NoiseIslands.IsSmallIslandTooClose(smallIsland, SmallIslands))
                    {
                        SmallIslands[i] = smallIsland;

                        NoiseIslands.CreateIslandFromMultipleCircles(_gameMap, mp, smallRadius, _tile);
                        IslandGenerated = true;
                    }
                    else
                    {
                        Attempts++;
                        Debug.Log("BL Too Close || Attempt: " + Attempts);

                        if (Attempts >= 5)
                        {
                            break;
                        }
                    }
                }
                i++;
            }
        }
    }
    
}

