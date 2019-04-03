using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMap : MonoBehaviour
{

    private Dictionary<MapPoint, Tile> MapDictionary = new Dictionary<MapPoint, Tile>();
    private Dictionary<MapPoint, int> TreeDictionary = new Dictionary<MapPoint, int>();
    private Tilemap GameTileMap;

    private ForestGenerator forestGenerator;

    private Tile WaterTile;
    private Tile SandTile;
    private Tile RoadTile;

    private int currentTrees = 0;

    // Use this for initialization
    void Start()
    {
        GameTileMap = GameObject.FindGameObjectWithTag("TileMap").GetComponent<Tilemap>();
    }

    public void AttachMapGameObject()
    {
        GameTileMap = GameObject.FindGameObjectWithTag("TileMap").GetComponent<Tilemap>();
    }

    public void AttachForestGenerator(ForestGenerator _fg)
    {
        forestGenerator = _fg;
    }

    public void AttachTiles()
    {
        WaterTile = GenerationManager.instance.tilePool.GetWaterTile();
        SandTile = GenerationManager.instance.tilePool.GetSandTile();
        //RoadTile = _road;
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
    public void AddTree(int _tree, MapPoint _mapPoint)
    {

        if (!(_mapPoint.x < 0 || _mapPoint.y < 0 || _mapPoint.x >= GenerationManager.instance.Width || _mapPoint.y >= GenerationManager.instance.Height))
        {
            currentTrees++;
            if (MapDictionary.ContainsKey(_mapPoint))
            {
                TreeDictionary[_mapPoint] = _tree;
            }
            else
            {
                TreeDictionary.Add(_mapPoint, _tree);
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
        TreeDictionary.Clear();
 
        GameTileMap.ClearAllTiles();

        int numTiles = _width * _width;
        MapDictionary = new Dictionary<MapPoint, Tile>(numTiles);
    }

    public void ApplySandNextToWater()
    {
        List<MapPoint> tileToAddSand = new List<MapPoint>();
        
        foreach (KeyValuePair<MapPoint, Tile> entry in MapDictionary)
        {                      
            if (entry.Value == WaterTile)
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
                        if (MapDictionary[mp] != WaterTile && MapDictionary[mp] != SandTile) //&& MapDictionary[mp] != RoadTile
                        {
                            tileToAddSand.Add(mp);                           
                        }
                    }
                }
            }
        }

        foreach(MapPoint mp in tileToAddSand)
        {
            MapDictionary[mp] = SandTile;
        }

    }

    public void GenerateMap()
    {

        foreach (KeyValuePair<MapPoint, Tile> entry in MapDictionary)
        {
            GameTileMap.SetTile(new Vector3Int(entry.Key.x, entry.Key.y, 0), entry.Value);
        }
        foreach (KeyValuePair<MapPoint, int> entry in TreeDictionary)
        {
            if (MapDictionary.ContainsKey(entry.Key))
            {
                if (MapDictionary[entry.Key] != WaterTile && MapDictionary[entry.Key] != SandTile) //&& MapDictionary[mp] != RoadTile
                {
                    forestGenerator.AddTreeToMap(entry.Key);
                }
            }
        }
    }

    public KeyValuePair<MapPoint, Tile> GetTileFromWorldPos(Vector3 mousePos)
    {
        Vector3Int coordinate = GameTileMap.WorldToCell(mousePos);
        Debug.Log(string.Format("Map Co-ords [X: {0} Y: {1}]", coordinate.x, coordinate.z));

        MapPoint point = new MapPoint(coordinate.x, coordinate.y);

        return new KeyValuePair<MapPoint, Tile>(point, MapDictionary[point]);
    }

    public void FillEmptySpaceWithTile(Tile _tile)
    {
        for (int iy = 0; iy < GenerationManager.instance.Height; iy++)
        {
            for (int ix = 0; ix < GenerationManager.instance.Width; ix++)
            {
                //MapPoint point = new MapPoint(ix, iy);
                MapPoint mp = new MapPoint(ix, iy);
                if (MapDictionary.ContainsKey(mp))
                {
                    if (MapDictionary[mp] == null) //&& MapDictionary[mp] != RoadTile
                    {
                        MapDictionary.Add(mp, _tile);
                    }
                }
                else
                {
                    MapDictionary.Add(mp, _tile);
                }             
            }
        }
    }
}
