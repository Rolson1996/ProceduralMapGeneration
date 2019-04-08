using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePool : MonoBehaviour
{
    //Random Gens
    public BiomeTileSet TileSet_Grass;
    public BiomeTileSet TileSet_Snow;
    public BiomeTileSet TileSet_Desert;


    //Testing Maps
    public BiomeTileSet TileSet_Mont1;
    public BiomeTileSet TileSet_ElCid5;
    public BiomeTileSet TileSet_VindSaga;
    public BiomeTileSet TileSet_Saladin1;




    public Tile GrassTile;
    public Tile SnowTile;
    public Tile SandTile;
    public Tile DesertTile;

    public Tile IceTile; 
    public Tile WaterTile;
    public Tile RoadTile;
    public Tile ShallowsTile;

    public Tile GrassDustTile;

    public Tile SnowGrassTile;
    public Tile SnowDustTile;

    public Tile DustGrassTile;


    void Awake()
    {
        TileSet_Grass = new BiomeTileSet();
        Tile[] Grass_GTiles = new Tile[2];
        Grass_GTiles[0] = GrassTile;
        Grass_GTiles[1] = GrassDustTile;

        TileSet_Grass.GroundTiles = Grass_GTiles;
        TileSet_Grass.Sand = SandTile;
        TileSet_Grass.Water = WaterTile;
        TileSet_Grass.Road = RoadTile;
        TileSet_Grass.Shallows = ShallowsTile;

        /////////////////////////////////

        TileSet_Snow = new BiomeTileSet();
        Tile[] Snow_GTiles = new Tile[3];
        Snow_GTiles[0] = SnowTile;
        Snow_GTiles[1] = SnowGrassTile;
        Snow_GTiles[2] = SnowDustTile;

        TileSet_Snow.GroundTiles = Snow_GTiles;
        TileSet_Snow.Sand = IceTile;
        TileSet_Snow.Water = WaterTile;
        TileSet_Snow.Road = RoadTile;
        TileSet_Snow.Shallows = IceTile;

        ////////////////////////////////////

        TileSet_Desert = new BiomeTileSet();
        Tile[] Desert_GTiles = new Tile[2];
        Desert_GTiles[0] = DesertTile;
        Desert_GTiles[1] = DustGrassTile;

        TileSet_Desert.GroundTiles = Desert_GTiles;
        TileSet_Desert.Sand = SandTile;
        TileSet_Desert.Water = WaterTile;
        TileSet_Desert.Road = RoadTile;
        TileSet_Desert.Shallows = ShallowsTile;

        ////////////////////////////////////

        TileSet_Mont1 = new BiomeTileSet();
        Tile[] Mont_GTiles = new Tile[1];
        Mont_GTiles[0] = GrassTile;

        TileSet_Mont1.GroundTiles = Mont_GTiles;
        TileSet_Mont1.Sand = SandTile;
        TileSet_Mont1.Water = WaterTile;
        TileSet_Mont1.Road = RoadTile;
        TileSet_Mont1.Shallows = ShallowsTile;

        ////////////////////////////////////

        TileSet_ElCid5 = new BiomeTileSet();
        Tile[] ElCid_GTiles = new Tile[2];
        ElCid_GTiles[0] = GrassTile;
        ElCid_GTiles[1] = DesertTile;

        TileSet_ElCid5.GroundTiles = ElCid_GTiles;
        TileSet_ElCid5.Sand = SandTile;
        TileSet_ElCid5.Water = WaterTile;
        TileSet_ElCid5.Road = RoadTile;
        TileSet_ElCid5.Shallows = ShallowsTile;

        ////////////////////////////////////

        TileSet_VindSaga = new BiomeTileSet();
        Tile[] Vind_GTiles = new Tile[3];
        Vind_GTiles[0] = SnowTile;
        Vind_GTiles[1] = GrassTile;
        Vind_GTiles[2] = SnowGrassTile;

        TileSet_VindSaga.GroundTiles = Vind_GTiles;
        TileSet_VindSaga.Sand = IceTile;
        TileSet_VindSaga.Water = WaterTile;
        TileSet_VindSaga.Road = RoadTile;
        TileSet_VindSaga.Shallows = IceTile;
        TileSet_VindSaga.Ice = IceTile;

        ////////////////////////////////////

        TileSet_Saladin1 = new BiomeTileSet();
        Tile[] Sal_GTiles = new Tile[2];
        Sal_GTiles[0] = DesertTile;
        Sal_GTiles[1] = GrassTile;

        TileSet_Saladin1.GroundTiles = Sal_GTiles;
        TileSet_Saladin1.Sand = SandTile;
        TileSet_Saladin1.Water = WaterTile;
        TileSet_Saladin1.Road = RoadTile;
        TileSet_Saladin1.Shallows = ShallowsTile;
    }

    public BiomeTileSet GetBiomeTileSetFromBiomeType(MapBiome mb)
    {
        switch (mb)
        {
            case MapBiome.Grass:
                return (TileSet_Grass);
            case MapBiome.Snow:
                return (TileSet_Snow);
            case MapBiome.Desert:
                return (TileSet_Desert);
            case MapBiome.Mont1:
                return (TileSet_Mont1);
            case MapBiome.ElCid5:
                return (TileSet_ElCid5);
            case MapBiome.Vindsaga:
                return (TileSet_VindSaga);
            case MapBiome.Saladin1:
                return (TileSet_Saladin1);
            default:
                return (TileSet_Grass);
        }
    }
}
