using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapClicks : MonoBehaviour
{

    public Grid grid;

    public Camera camera;

    bool canvasClicked = false;
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !canvasClicked)
        {
           ClickedOnMap();
        }
        canvasClicked = false;
    }
    public void ClickedOnMap()
    {       
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Vector3 origin = ray.origin;
            Vector3 direction = ray.direction;
            float yFactor = origin.y / direction.y;

            Vector3 test = origin - (direction * yFactor);

            Tile tile = GenerationManager.instance.GetGameMap().GetTileFromWorldPos(test);        
    }

    public void CanvasClicked()
    {
        canvasClicked = true;
    }
}
