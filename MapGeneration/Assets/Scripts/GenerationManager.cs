using Algorithms;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GenerationManager : MonoBehaviour {

    public static GenerationManager instance = null;

    public GameObject cam1;
    public GameObject cam2;
    private Camera topDownCam;

    public int Height = 10;
    public int Width = 10;
    public bool HasRivers = false;

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

        cam1.GetComponent<Camera>().enabled = true;
        topDownCam = cam2.GetComponent<Camera>();
        topDownCam.enabled = false;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            cam1.GetComponent<Camera>().enabled = !cam1.GetComponent<Camera>().enabled;
            cam2.GetComponent<Camera>().enabled = !cam2.GetComponent<Camera>().enabled;
        }
    }
    public void StartGenerationProcess()
    {
        if (Height <= 0 || Width <= 0)
        {
            EditorUtility.DisplayDialog("Empty Map", "The height and/or width = 0", "Coolio");
            return ;
        }
        
        GenerationMap.CreateEmptyMap(Width);

        NoiseGroundTiles.GenerateGroundTiles(GenerationMap, tilePool.GetTileSetFromMapType(SelectedMapType));

        //RandomTilesMap rtm = new RandomTilesMap();
        //rtm.GenerateRandomTileMap(GenerationMap, tilePool.GetTileSetFromMapType(SelectedMapType));

        //CreateNoiseBlob.CreateBlobAtPosition(GenerationMap, new MapPoint(30, 30),2, tilePool.GetWaterTile());

        if (HasRivers)
        {
            int riverStartPoint = Mathf.FloorToInt(Width * UnityEngine.Random.Range(0.1F, 0.9F));

            int startAxis = UnityEngine.Random.Range(0, 4);

            switch (startAxis)
            {
                //X 2 X
                //1 X 3
                //X 4 X

                case 0:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(0, riverStartPoint), tilePool.GetWaterTile(), tilePool.GetSandTile());
                    break;
                case 1:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(riverStartPoint, Height), tilePool.GetWaterTile(), tilePool.GetSandTile());
                    break;
                case 2:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(Width, riverStartPoint), tilePool.GetWaterTile(), tilePool.GetSandTile());
                    break;
                case 3:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(riverStartPoint, Height), tilePool.GetWaterTile(), tilePool.GetSandTile());
                    break;
                default:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(0, riverStartPoint), tilePool.GetWaterTile(), tilePool.GetSandTile());
                    break;

            }
        }

        

        GenerationMap.ApplySandNextToWater();
        GenerationMap.GenerateMap();
    }

    public GameMap GetGameMap()
    {
        return GenerationMap;
    }

    public void SetTopDownPos(float x, float y, float z)
    {
        cam2.transform.position = new Vector3(x, y, z);
    }


    
}
