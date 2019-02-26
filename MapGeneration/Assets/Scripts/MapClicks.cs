using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapClicks : MonoBehaviour
{


    public Camera camera;


    private void LateUpdate()
    {
        ClickedOnMap();
    }
    public void ClickedOnMap()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);            
            //Tile tile = GenerationManager.instance.GetGameMap().GetTileFromWorldPos(new Vector3(pos.x, pos.y, pos.z));
            //Debug.Log(string.Format("Co-ords of mouse is [X: {0} Y: {0}]", pos.x, pos.y));

            Vector3 worldPoint;

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                worldPoint = hit.point;
                Tile tile = GenerationManager.instance.GetGameMap().GetTileFromWorldPos(worldPoint);
            }


            // get the collision point of the ray with the z = 0 plane
            //Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
            //Vector3Int position = grid.WorldToCell(worldPoint);

            //Tile tile = GenerationManager.instance.GetGameMap().GetTileFromWorldPos(worldPoint);

            //if (tile)
            //{
            //    Debug.Log(string.Format("Tile is: {0}", tile.TileType));
            //}
        }
    }

}
