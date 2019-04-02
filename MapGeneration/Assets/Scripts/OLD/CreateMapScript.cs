using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class CreateMapScript : MonoBehaviour
{

    public Tilemap GameTileMap;
    public GameObject heightText;
    public GameObject widthText;

    //public Tile BlackSpaceTile;

    //public Tile GrassTile;
    //public Tile SnowTile;
    //public Tile SnowGrassTile;

    public int MapWidth = 10;
    public int MapHeight = 10;

    public TilePool tilePool;
    public MapBiome SelectedMapType = MapBiome.Grass;


    // Use this for initialization
    void Start()
    {
        tilePool = this.gameObject.GetComponent<TilePool>();
        var hold = heightText.GetComponent<Text>();
        hold.text = MapHeight.ToString();
        widthText.GetComponent<Text>().text = MapWidth.ToString();

        GenerateMap();

    }

    public void GenerateMap()
    {
        GameTileMap.ClearAllTiles();

        if (heightText.GetComponent<Text>().text.Length > 0)
        {
            int.TryParse(heightText.GetComponent<Text>().text, out MapHeight);
        }
        if (widthText.GetComponent<Text>().text.Length > 0)
        {
            int.TryParse(widthText.GetComponent<Text>().text, out MapWidth);
        }


        GeneratePlains(tilePool.GetTileSetFromMapType(SelectedMapType));


        //System.Random rnd = new System.Random();
        //for (int iy = 0; iy < MapHeight; iy++)
        //{
        //    for (int ix = 0; ix < MapWidth; ix++)
        //    {
        //        int RandomTile = rnd.Next(3);

        //        switch (RandomTile)
        //        {
        //            case 1:
        //                SetMapTile(ix, iy, SnowGrassTile);
        //                break;
        //            case 2:
        //                SetMapTile(ix, iy, SnowTile);
        //                break;
        //            default:
        //                SetMapTile(ix, iy, GrassTile);
        //                break;
        //        }
        //    }
        //}

        //for (int ix = 0; ix < MapWidth + 1; ix++)
        //{
        //    GameTileMap.SetTile(new Vector3Int(ix, -1, 0), BlackSpaceTile);
        //    GameTileMap.SetTile(new Vector3Int(ix, MapHeight, 0), BlackSpaceTile);

        //}

        //for (int iy = 0; iy < MapHeight; iy++)
        //{
        //    GameTileMap.SetTile(new Vector3Int(-1, iy, 0), BlackSpaceTile);
        //    GameTileMap.SetTile(new Vector3Int(MapWidth, iy, 0), BlackSpaceTile);
        //}

        //GameTileMap.SetTile(new Vector3Int(-1, -1, 0), BlackSpaceTile);
        //GameTileMap.SetTile(new Vector3Int(-1, MapHeight, 0), BlackSpaceTile);
    }

    private void SetMapTile(int x, int y, Tile t)
    {
        GameTileMap.SetTile(new Vector3Int(x, y, 0), t);
    }

    private void GeneratePlains(Tile[] tiles)
    {
        System.Random rnd = new System.Random();
        int ArraySize = tiles.Length;

        for (int iy = 0; iy < MapHeight; iy++)
        {
            for (int ix = 0; ix < MapWidth; ix++)
            {
                int RandomTile = rnd.Next(ArraySize);
                SetMapTile(ix, iy, tiles[RandomTile]);
                Debug.Log("X: " + ix + " | Y: " + iy + " | " + RandomTile + " | " + tiles[RandomTile].ToString());
            }
        }
    }
}
