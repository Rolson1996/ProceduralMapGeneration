﻿using System.Collections;
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

        if (!(_mapPoint.x < 0 || _mapPoint.y < 0 || _mapPoint.x == GenerationManager.instance.Width - 1 || _mapPoint.y == GenerationManager.instance.Height - 1))
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
    }

    public Tile GetTileAtPos(MapPoint _point)
    {
        if(MapDictionary.ContainsKey(_point))
        {
            return MapDictionary[_point];
        }
        else
        {
            return null;
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

    public Tile GetTileFromWorldPos(Vector3 mousePos)
    {
        Vector3Int coordinate = GameTileMap.LocalToCell(mousePos);
        Debug.Log(string.Format("Map Co-ords [X: {0} Y: {0}]", coordinate.x, coordinate.y));
        GameTileMap.SetTile(coordinate, null);

        return null;
    }
}
