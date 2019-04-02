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
    public MapBiome SelectedMapBiome = MapBiome.Grass;
    public MapTypeX SelectedMapTypeX = MapTypeX.Plains;

    public ForestGenerator forestGenerator;
    public DisplaySelected displaySelected;
    public Canvas canvas;

    // Use this for initialization
    void Awake ()
    {
		if(instance == null)
        {
            instance = this;
            tilePool = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<TilePool>();

            GenerationMap = this.gameObject.AddComponent<GameMap>();
            GenerationMap.AttachMapGameObject();

            forestGenerator = this.gameObject.GetComponent<ForestGenerator>();
            GenerationMap.AttachForestGenerator(forestGenerator);

            GenerationMap.AttachTiles();

            
            displaySelected = canvas.GetComponent<DisplaySelected>();
        }
        else
        {
            instance.tilePool = this.gameObject.GetComponent<TilePool>();
            instance.canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
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

        //Reset map
        GenerationMap.CreateEmptyMap(Width);
        forestGenerator.DestroyOldTrees();

        switch (SelectedMapTypeX)
        {
            case MapTypeX.Plains:
                GeneratePlains();
                break;
            case MapTypeX.Costal:
                GenerateCostal();
                break;
            case MapTypeX.Islands:
                GenerateIslands();
                break;
            default:
                GeneratePlains();
                break;
        }



        //RandomTilesMap rtm = new RandomTilesMap();
        //rtm.GenerateRandomTileMap(GenerationMap, tilePool.GetTileSetFromMapType(SelectedMapType));

        //CreateNoiseBlob.CreateBlobAtPosition(GenerationMap, new MapPoint(30, 30),2, tilePool.GetWaterTile());

        //forestGenerator.AddTreeToMap(new MapPoint(10, 10));
        //forestGenerator.CreateTreeBlobAtPosition(GenerationMap, new MapPoint(24, 24), 2);

        GenerationMap.ApplySandNextToWater();
        GenerationMap.GenerateMap();
    }

    public GameMap GetGameMap()
    {
        return GenerationMap;
    }

    public void SetTopDownPos(float x, float y, float z)
    {
        cam2.transform.localPosition = new Vector3(x, y, z);
    }


    private void GeneratePlains()
    {
        NoiseGroundTiles.GenerateGroundTiles(GenerationMap, tilePool.GetTileSetFromMapType(SelectedMapBiome));
        ForestGenerator.GenerateTreesForForest(GenerationMap);

        //Creating Rivers
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
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(0, riverStartPoint), tilePool.GetWaterTile());
                    break;
                case 1:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(riverStartPoint, Height), tilePool.GetWaterTile());
                    break;
                case 2:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(Width, riverStartPoint), tilePool.GetWaterTile());
                    break;
                case 3:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(riverStartPoint, Height), tilePool.GetWaterTile());
                    break;
                default:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(0, riverStartPoint), tilePool.GetWaterTile());
                    break;

            }
        }
    }
    private void GenerateIslands()
    {

    }
    private void GenerateCostal()
    {
        NoiseRiver.AttachMap(GenerationMap, tilePool.GetWaterTile());
        NoiseGroundTiles.GenerateGroundTiles(GenerationMap, tilePool.GetTileSetFromMapType(SelectedMapBiome));
        ForestGenerator.GenerateTreesForForest(GenerationMap);


        int coastLineSide = CoastLineGenerator.GenerateCoastline(GenerationMap, tilePool.GetWaterTile());
        //X 0 X
        //1 X 3
        //X 2 X

        if (HasRivers)
        {
            int riverStartPoint = Mathf.FloorToInt(Width * UnityEngine.Random.Range(0.1F, 0.9F));
          
            switch (coastLineSide)
            {                
                case 0:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(riverStartPoint, 0), tilePool.GetWaterTile());                    
                    break;
                case 1:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(0, riverStartPoint), tilePool.GetWaterTile());
                    break;
                case 2:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(riverStartPoint, Height), tilePool.GetWaterTile());
                    break;
                case 3:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(Width, riverStartPoint), tilePool.GetWaterTile());                   
                    break;
                default:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(riverStartPoint, 0), tilePool.GetWaterTile());
                    break;

            }
        }

    }
}
