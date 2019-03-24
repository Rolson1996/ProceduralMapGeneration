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

        if (!(_mapPoint.x < 0 || _mapPoint.y < 0 || _mapPoint.x >= GenerationManager.instance.Width || _mapPoint.y >= GenerationManager.instance.Height))
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

    public void CreateEmptyMap(int _width)
    {
        MapDictionary.Clear();
        GameTileMap.ClearAllTiles();

        int numTiles = _width * _width;
        MapDictionary = new Dictionary<MapPoint, Tile>(numTiles);

        for (int iy = 0; iy < GenerationManager.instance.Height; iy++)
        {
            for (int ix = 0; ix < GenerationManager.instance.Width; ix++)
            {
                //MapPoint point = new MapPoint(ix, iy);
                MapDictionary.Add(new MapPoint(ix, iy), null);
            }
        }
    }

    public void ApplySandNextToWater()
    {
        List<MapPoint> tileToAddSand = new List<MapPoint>();
        Tile water = GenerationManager.instance.tilePool.GetWaterTile();
        Tile sand = GenerationManager.instance.tilePool.GetSandTile();

        foreach (KeyValuePair<MapPoint, Tile> entry in MapDictionary)
        {                      
            if (entry.Value == water)
            {
                //123
                //4X6
                //789

                int x = entry.Key.x;
                int y = entry.Key.y;

                MapPoint[] surrondingTiles = new MapPoint[8];
                surrondingTiles[0] = new MapPoint(x - 1, y + 1);
                surrondingTiles[1] = new MapPoint(x, y + 1);
                surrondingTiles[2] = new MapPoint(x + 1, y + 1);
                surrondingTiles[3] = new MapPoint(x - 1, y);
                surrondingTiles[4] = new MapPoint(x + 1, y);
                surrondingTiles[5] = new MapPoint(x - 1, y - 1);
                surrondingTiles[6] = new MapPoint(x, y - 1);
                surrondingTiles[7] = new MapPoint(x + 1, y - 1);

                foreach (MapPoint mp in surrondingTiles)
                {
                    if (MapDictionary.ContainsKey(mp))
                    {
                        if (MapDictionary[mp] != water && MapDictionary[mp] != sand)
                        {
                            tileToAddSand.Add(mp);                           
                        }
                    }
                }
            }
        }

        foreach(MapPoint mp in tileToAddSand)
        {
            MapDictionary[mp] = sand;
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
        //Vector3Int coordinate = GameTileMap.LocalToCell(mousePos);
        //Debug.Log(string.Format("Map Co-ords [X: {0} Y: {1}]", coordinate.x, coordinate.z));
        //Vector3Int correctCoord = new Vector3Int(coordinate.x, coordinate.z, 0);


        Debug.Log(string.Format("Map Co-ords [X: {0} Y: {1}]", Mathf.Floor(mousePos.x), Mathf.Floor(mousePos.z)));
        Vector3Int correctCoord = new Vector3Int((int)Mathf.Floor(mousePos.x), (int)Mathf.Floor(mousePos.z), 0);

        GameTileMap.SetTile(correctCoord, null);

        return null;
    }
}
