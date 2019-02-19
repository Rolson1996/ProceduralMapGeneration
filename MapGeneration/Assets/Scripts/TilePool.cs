using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePool : MonoBehaviour {

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

    void Awake()
    {
        int GP_TileCount = 2 + GrassDustMixTiles.Length;
        TileSet_GrassPlains = new Tile[GP_TileCount];
        TileSet_GrassPlains[0] = GrassTile;
        TileSet_GrassPlains[1] = GrassTile;
        GrassDustMixTiles.CopyTo(TileSet_GrassPlains, 2);


        int SP_TileCount = 3 + SnowMixTiles.Length;
        TileSet_SnowPlains = new Tile[SP_TileCount];
        TileSet_SnowPlains[0] = SnowTile;
        TileSet_SnowPlains[1] = SnowTile;
        TileSet_SnowPlains[2] = GrassTile;
        SnowMixTiles.CopyTo(TileSet_SnowPlains, 3);


    }

    public Tile[] GetTileSetFromMapType(MapType mt)
    {
        switch (mt)
        {
            case MapType.GrassPlains:
                return (TileSet_GrassPlains);
            case MapType.SnowPlains:
                return (TileSet_SnowPlains);
            default:
                return (TileSet_GrassPlains);
        }
    }
}
