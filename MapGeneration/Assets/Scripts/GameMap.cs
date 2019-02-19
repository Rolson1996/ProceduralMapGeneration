using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMap : MonoBehaviour
{

    private Dictionary<MapPoint, Tile> MapDictionary = new Dictionary<MapPoint, Tile>();
    private Tilemap GameTileMap;

    // Use this for initialization
    void Start()
    {
        GameTileMap = GameObject.FindGameObjectWithTag("TileMap").GetComponent<Tilemap>();
    }

    public void AttachMapGameObject()
    {
        GameTileMap = GameObject.FindGameObjectWithTag("TileMap").GetComponent<Tilemap>();
    }

    public void AddTile(Tile _tile, MapPoint _mapPoint)
    {
        if (MapDictionary.ContainsKey(_mapPoint))
        {
            MapDictionary[_mapPoint] = _tile;
                
        }
        else
        {
            MapDictionary.Add(_mapPoint, _tile);
                
        }
    }

    public void CreateEmptyMap()
    {
        MapDictionary.Clear();
        GameTileMap.ClearAllTiles();

        for (int iy = 0; iy < GenerationManager.instance.Height; iy++)
        {
            for (int ix = 0; ix < GenerationManager.instance.Width; ix++)
            {
                //MapPoint point = new MapPoint(ix, iy);
                MapDictionary.Add(new MapPoint(ix, iy), null);
            }
        }
    }

    public void GenerateMap()
    {

        foreach (KeyValuePair<MapPoint, Tile> entry in MapDictionary)
        {
            GameTileMap.SetTile(new Vector3Int(entry.Key.x, entry.Key.y, 0), entry.Value);
        }
    }
}
