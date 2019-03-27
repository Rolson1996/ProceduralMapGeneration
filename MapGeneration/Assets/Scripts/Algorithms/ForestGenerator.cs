using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ForestGenerator : MonoBehaviour
{

    public GameObject TreePrefab;
    public Canvas canvas;

    public void AddTreeToMap(MapPoint point)
    {
        var newTree = Instantiate(TreePrefab, this.transform, false);
        newTree.transform.localPosition = new Vector3(1.25F * (point.x + 1), 1.25F * (point.y + 1), -0.1F);
        newTree.GetComponent<GameResource>().MapLocation = point;
        newTree.GetComponent<GameResource>().canvas = canvas;
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

    public void DestroyOldTrees()
    {
        int i = 0;
        Debug.Log(transform.childCount);
        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }
        Debug.Log(transform.childCount);
    }

}
