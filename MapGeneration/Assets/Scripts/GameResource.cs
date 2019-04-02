using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResource : MonoBehaviour {
   
    public MapPoint MapLocation; 

    private void OnMouseDown()
    {
        GenerationManager.instance.displaySelected.Select(GetComponent<SpriteRenderer>().sprite.name, MapLocation.x, MapLocation.y, GetComponent<SpriteRenderer>().sprite);
        Debug.Log("Tree Hit");
    }
}
