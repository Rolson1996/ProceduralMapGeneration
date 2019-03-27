using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MapClicks : MonoBehaviour
{
    public Camera camera;
    public Canvas canvas;

    private DisplaySelected displaySelected;


    private void Start()
    {      
        displaySelected = canvas.GetComponent<DisplaySelected>();
    }

    private void OnMouseDown()
    {
        ClickedOnMap();
    }

    public void ClickedOnMap()
    {       
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 origin = ray.origin;
        Vector3 direction = ray.direction;
        float yFactor = origin.z / direction.z;

        Vector3 test = origin - (direction * yFactor);

        KeyValuePair<MapPoint, Tile> tile = GenerationManager.instance.GetGameMap().GetTileFromWorldPos(test);
        displaySelected.Select(tile.Value.name, tile.Key.x, tile.Key.y, tile.Value.sprite);
    }
}
