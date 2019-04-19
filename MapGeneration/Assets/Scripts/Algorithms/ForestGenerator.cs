using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    public GameObject TreePrefab;
    private int currentTree = 0;

    public void AddTreeToMap(MapPoint point)
    {
        //var newTree = Instantiate(TreePrefab, this.transform, false);
        //newTree.transform.localPosition = new Vector3(point.x + .25F, point.y + .25F, -1F);
        //newTree.GetComponent<GameResource>().MapLocation = point;
        if (currentTree < transform.childCount)
        {
            var t = transform.GetChild(currentTree);
            t.localPosition = new Vector3(point.x + .25F, point.y + .25F, -1F);
            t.GetComponent<GameResource>().MapLocation = point;
            currentTree++;
        }
    }


    public void CreateTreeBlobAtPosition(GameMap _map, MapPoint _point, int _radius)
    {
        float scale = 30;
        float seed = UnityEngine.Random.Range(1000, 10000);

        for (int x = -_radius; x < _radius; x++)
        {
            for (int y = -_radius; y < _radius; y++)
            {

                int locationX = _point.x + x;
                int locationY = _point.y + y;

                float seededX = locationX + seed;
                float seededY = locationY + seed;

                var perlin = Mathf.PerlinNoise((seededX / (float)GenerationManager.instance.Width) * scale, (seededY / (float)GenerationManager.instance.Height) * scale);
                if (perlin < .6f)
                {
                    AddTreeToMap(new MapPoint(locationX, locationY));
                }
            }
        }
    }

    public static void GenerateTreesForForest(GameMap _map)
    {
        //int numOfExtraTiles = _tileSet.Length - 1;
        float scale = 10;
        float seed = UnityEngine.Random.Range(1000, 10000);


        for (int x = 0; x < GenerationManager.instance.Width; x++)
        {
            for (int y = 0; y < GenerationManager.instance.Height; y++)
            {

                float seededX = x + seed;
                float seededY = y + seed;

                var perlin = Mathf.PerlinNoise((seededX / (float)GenerationManager.instance.Width) * scale, (seededY / (float)GenerationManager.instance.Height) * scale);
                if (perlin < .3f)
                {
                    _map.AddTree(0, new MapPoint(x, y));
                }
            }
        }
    }

    public void ResetTrees()
    {
 
        foreach (Transform child in transform)
        {
            child.localPosition = new Vector3 (-99F,-99F,-1F);
        }

        currentTree = 0;
    }

}
