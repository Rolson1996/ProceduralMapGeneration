using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResource : MonoBehaviour {

    public Canvas canvas;

    private DisplaySelected displaySelected;
    private SpriteRenderer spriteRenderer;

    public MapPoint MapLocation; 


    private void Start()
    {
        displaySelected = canvas.GetComponent<DisplaySelected>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        displaySelected.Select(spriteRenderer.sprite.name, MapLocation.x, MapLocation.y, spriteRenderer.sprite);
        Debug.Log("Tree Hit");
    }
}
