﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePool : MonoBehaviour
{

    public Tile[] TileSet_GrassPlains;
    public Tile[] TileSet_GrassWoods;
    public Tile[] TileSet_SnowPlains;

    public Tile GrassTile;
    public Tile SnowTile;
    public Tile SandTile;
    public Tile DesertTile;

    public Tile IceTile; //Convert into array with different shades of ice
    public Tile WaterTile; //Convert into array with different shades of water

    public Tile[] SnowMixTiles;
    public Tile[] GrassDustMixTiles;

    public Tile RoadTile;
    public Tile ShallowsTile;

    void Awake()
    {
        int GP_TileCount = 1 + GrassDustMixTiles.Length;
        TileSet_GrassPlains = new Tile[GP_TileCount];
        TileSet_GrassPlains[0] = GrassTile;
        //TileSet_GrassPlains[1] = GrassTile;
        GrassDustMixTiles.CopyTo(TileSet_GrassPlains, 1);


        int SP_TileCount = 1 + SnowMixTiles.Length;
        TileSet_SnowPlains = new Tile[SP_TileCount];
        TileSet_SnowPlains[0] = SnowTile;
        //TileSet_SnowPlains[1] = SnowTile;
        //TileSet_SnowPlains[2] = GrassTile;
        SnowMixTiles.CopyTo(TileSet_SnowPlains, 1);


    }

    public Tile[] GetTileSetFromBiomeType(MapBiome mt)
    {
        switch (mt)
        {
            case MapBiome.Grass:
                return (TileSet_GrassPlains);
            case MapBiome.Snow:
                return (TileSet_SnowPlains);
            default:
                return (TileSet_GrassPlains);
        }
    }

    public Tile GetWaterTile()
    {
        return WaterTile;
    }

    public Tile GetSandTile()
    {
        return SandTile;
    }

    public Tile GetRoadTile()
    {
        return RoadTile;
    }

    public Tile GetShallowsTile()
    {
        return ShallowsTile;
    }
}
