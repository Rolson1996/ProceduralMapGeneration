using Algorithms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour {

    public static GenerationManager instance = null;

    public int Height = 10;
    public int Width = 10;

    private GameMap GenerationMap;

    public TilePool tilePool;
    public MapType SelectedMapType = MapType.GrassPlains;



    // Use this for initialization
    void Start ()
    {
		if(instance == null)
        {
            instance = this;
            tilePool = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<TilePool>();

            GenerationMap = this.gameObject.AddComponent<GameMap>();
            GenerationMap.AttachMapGameObject();
        }
        else
        {
            instance.tilePool = this.gameObject.GetComponent<TilePool>();
            Destroy(this);
        }
	}
	
    public void StartGenerationProcess()
    {
        GenerationMap.CreateEmptyMap();


        RandomTilesMap rtm = new RandomTilesMap();
        rtm.GenerateRandomTileMap(GenerationMap, tilePool.GetTileSetFromMapType(SelectedMapType));


        GenerationMap.GenerateMap();
    }


    
}
