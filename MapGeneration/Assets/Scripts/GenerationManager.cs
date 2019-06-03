using Algorithms;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GenerationManager : MonoBehaviour {

    public static GenerationManager instance = null;

    public GameObject cam1;
    public GameObject cam2;
    private Camera gamePlayCam;
    private Camera topDownCam;
    private bool showWholeMap = false;

    public int Height = 0;
    public int Width = 0;
    public bool HasRivers = true;
    public bool HasPaths = true;

    private GameMap GenerationMap;

    public TilePool tilePool;
    public MapBiome SelectedMapBiome = MapBiome.Grass;
    public MapTypeX SelectedMapTypeX = MapTypeX.Plains;

    public ForestGenerator forestGenerator;
    public DisplaySelected displaySelected;
    public Canvas canvas;
    public Canvas testingPhaseCanvas;


    private bool readyToGen = false;
    private bool autoGenActive = false;

    public GameObject GeneratingObject;
    private DateTime lastGenTime;

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

            CreatePaths.AttachMap(GenerationMap);
            CreatePaths.AttachTiles(GetCurrentBiomeTileSet().Road, GetCurrentBiomeTileSet().Shallows, GetCurrentBiomeTileSet().Water);

            displaySelected = canvas.GetComponent<DisplaySelected>();
        }
        else
        {
            instance.tilePool = this.gameObject.GetComponent<TilePool>();
            instance.canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
            Destroy(this);
        }

        
        gamePlayCam = cam1.GetComponent<Camera>();
        gamePlayCam.enabled = true;
        topDownCam = cam2.GetComponent<Camera>();
        topDownCam.enabled = false;

        testingPhaseCanvas.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cam1.GetComponent<Camera>().enabled = !cam1.GetComponent<Camera>().enabled;
            cam2.GetComponent<Camera>().enabled = !cam2.GetComponent<Camera>().enabled;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            showWholeMap = !showWholeMap;
            MoveGamePlayCam();
            
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            canvas.enabled = !canvas.enabled;

        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            canvas.enabled = !canvas.enabled;
            testingPhaseCanvas.enabled = !testingPhaseCanvas.enabled;
        }

        if (autoGenActive)
        {
            if (readyToGen)
            {
                if (Height <= 0 || Width <= 0)
                {
                    Height = 72;
                    Width = 72;
                }

                StartGenerationProcess();
                MoveGamePlayCam();
                lastGenTime = DateTime.Now;
                readyToGen = false;
                GeneratingObject.SetActive(false);
            }
            if (lastGenTime.AddSeconds(10) <= DateTime.Now)
            {
                readyToGen = true;
                GeneratingObject.SetActive(true);
            }
        }


    }
    public void StartGenerationProcess()
    {
        if (Height <= 0 || Width <= 0)
        {
            //EditorUtility.DisplayDialog("Empty Map", "The height and/or width = 0", "Coolio");
            return ;
        }
        GenerationMap.AttachTiles();
        CreatePaths.AttachTiles(GetCurrentBiomeTileSet().Road, GetCurrentBiomeTileSet().Shallows, GetCurrentBiomeTileSet().Water);

        //Reset map
        GenerationMap.CreateEmptyMap(Width);
        forestGenerator.ResetTrees();

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
        //rtm.GenerateRandomTileMap(GenerationMap, tilePool.GetTileSetFromBiomeType(SelectedMapType));

        //CreateNoiseBlob.CreateBlobAtPosition(GenerationMap, new MapPoint(30, 30),2, tilePool.GetWaterTile());

        //forestGenerator.AddTreeToMap(new MapPoint(10, 10));
        //forestGenerator.CreateTreeBlobAtPosition(GenerationMap, new MapPoint(24, 24), 2);

        GenerationMap.ApplySandNextToWater();
        GenerationMap.GenerateMap();

        MoveGamePlayCam();
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
        NoiseGroundTiles.GenerateGroundTiles(GenerationMap, GetCurrentBiomeTileSet().GroundTiles);
        ForestGenerator.GenerateTreesForForest(GenerationMap);

        //Creating Rivers
        if (HasRivers)
        {
            int numRivers = 0;
            //numRivers = UnityEngine.Random.Range(0, 4);

            switch (Width)
            {
                case 72:
                    numRivers = 1;
                    break;
                case 96:
                    numRivers = 1;
                    break;
                case 120:
                    numRivers = UnityEngine.Random.Range(1, 3);
                    break;
                case 144:
                    numRivers = UnityEngine.Random.Range(1, 3);
                    break;
                case 200:
                    numRivers = UnityEngine.Random.Range(2, 4);
                    break;
                //case 255:
                //    DifferentSizeIslands.GenerateGiganticIslands(GenerationMap, tilePool.GetTileSetFromBiomeType(SelectedMapBiome)[0]);
                //    break;
                default:
                    break;
            }

            int r = 0;
            while(r < numRivers)
            {
            
                int riverStartPoint = Mathf.FloorToInt(Width * UnityEngine.Random.Range(0.1F, 0.9F));

                int startAxis = UnityEngine.Random.Range(0, 4);

                switch (startAxis)
                {
                    //X 2 X
                    //1 X 3
                    //X 4 X

                    case 0:
                        NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(0, riverStartPoint), GetCurrentBiomeTileSet().Water);
                        break;
                    case 1:
                        NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(riverStartPoint, Height), GetCurrentBiomeTileSet().Water);
                        break;
                    case 2:
                        NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(Width, riverStartPoint), GetCurrentBiomeTileSet().Water);
                        break;
                    case 3:
                        NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(riverStartPoint, Height), GetCurrentBiomeTileSet().Water);
                        break;
                    default:
                        NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(0, riverStartPoint), GetCurrentBiomeTileSet().Water);
                        break;

                }
                r++;
            }
            
        }
        if (HasPaths)
        {
            GeneratePaths();
        }
    }
    private void GenerateIslands()
    {       
        switch (Width)
        {
            case 72:
                DifferentSizeIslands.GenerateTinyIslands(GenerationMap, GetCurrentBiomeTileSet().GroundTiles[0]);
                break;
            case 96:
                DifferentSizeIslands.GenerateSmallIslands(GenerationMap, GetCurrentBiomeTileSet().GroundTiles[0]);
                break;
            case 120:
                DifferentSizeIslands.GenerateMediumIslands(GenerationMap, GetCurrentBiomeTileSet().GroundTiles[0]);
                break;
            case 144:
                DifferentSizeIslands.GenerateLargeIslands(GenerationMap, GetCurrentBiomeTileSet().GroundTiles[0]);
                break;
            case 200:
                DifferentSizeIslands.GenerateHugeIslands(GenerationMap, GetCurrentBiomeTileSet().GroundTiles[0]);
                break;
            //case 255:
            //    DifferentSizeIslands.GenerateGiganticIslands(GenerationMap, tilePool.GetTileSetFromBiomeType(SelectedMapBiome)[0]);
            //    break;
            default:
                break;
        }
       
        ForestGenerator.GenerateTreesForForest(GenerationMap);
        GenerationMap.FillEmptySpaceWithTile(GetCurrentBiomeTileSet().Water);
    }
    private void GenerateCostal()
    {
        NoiseRiver.AttachMap(GenerationMap, GetCurrentBiomeTileSet().Water);
        NoiseGroundTiles.GenerateGroundTiles(GenerationMap, GetCurrentBiomeTileSet().GroundTiles);
        ForestGenerator.GenerateTreesForForest(GenerationMap);


        int coastLineSide = CoastLineGenerator.GenerateCoastline(GenerationMap, GetCurrentBiomeTileSet().Water);
        //X 0 X
        //1 X 3
        //X 2 X

        if (HasRivers)
        {
            int riverStartPoint = Mathf.FloorToInt(Width * UnityEngine.Random.Range(0.1F, 0.9F));
          
            switch (coastLineSide)
            {                
                case 0:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(riverStartPoint, 0), GetCurrentBiomeTileSet().Water);                    
                    break;
                case 1:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(0, riverStartPoint), GetCurrentBiomeTileSet().Water);
                    break;
                case 2:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(riverStartPoint, Height), GetCurrentBiomeTileSet().Water);
                    break;
                case 3:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(Width, riverStartPoint), GetCurrentBiomeTileSet().Water);                   
                    break;
                default:
                    NoiseRiver.BuildRiverFromMapEdge(GenerationMap, new MapPoint(riverStartPoint, 0), GetCurrentBiomeTileSet().Water);
                    break;

            }
        }
        if (HasPaths)
        {
            GeneratePaths();
        }

    }

    private void GeneratePaths()
    {
        int numPaths = UnityEngine.Random.Range(1, 3);
        int p = 0;
        while (p < numPaths)
        {


            bool newStartAndEnd = true;

            int minLength = 0;
            switch (Width)
            {
                case 72:
                    minLength = 1296;
                    break;
                case 96:
                    minLength = 2304;
                    break;
                case 120:
                    minLength = 3600;
                    break;
                case 144:
                    minLength = 5184;
                    break;
                case 200:
                    minLength = 10000;
                    break;
                //case 255:
                //    DifferentSizeIslands.GenerateGiganticIslands(GenerationMap, tilePool.GetTileSetFromBiomeType(SelectedMapBiome)[0]);
                //    break;
                default:
                    break;
            }

            int startX = 0, startY = 0, endX = 0, endY = 0;

            while (newStartAndEnd)
            {
                int startAxis = UnityEngine.Random.Range(0, 4);
                int pathStart = UnityEngine.Random.Range(0, Width);

                switch (startAxis)
                {
                    case 0:
                        startX = 0;
                        startY = UnityEngine.Random.Range(0, Height - 1);
                        break;
                    case 1:
                        startX = UnityEngine.Random.Range(0, Width - 1);
                        startY = 0;
                        break;
                    case 2:
                        startX = Width-1;
                        startY = UnityEngine.Random.Range(0, Height - 1);
                        break;
                    case 3:
                        startX = UnityEngine.Random.Range(0, Width - 1);
                        startY = Height-1;
                        break;
                }

                endX = UnityEngine.Random.Range(0, Width - 1);
                endY = UnityEngine.Random.Range(0, Height - 1);

                int distX = Math.Abs(startX - endX);
                int distY = Math.Abs(startY - endY);

                if ((distX * distX) + (distY * distY) > minLength)
                {
                    if (GenerationMap.GetTileAtPos(new MapPoint(startX, startY)) != GetCurrentBiomeTileSet().Water && GenerationMap.GetTileAtPos(new MapPoint(endX, endY)) != GetCurrentBiomeTileSet().Water)
                    {
                        newStartAndEnd = false;
                    }
                }
            }

            int previousTurnX = startX;
            int previousTurnY = startY;

            int numTurns = UnityEngine.Random.Range(1, 4);
            int t = 0;

            while (t < numTurns)
            {
                int randTurnX = 0;
                int randTurnY = 0;

                if (previousTurnX > endX)
                {
                    randTurnX = UnityEngine.Random.Range(previousTurnX, endX);
                }
                else if (previousTurnX == endX)
                {
                    randTurnX = endX;
                }
                else if (previousTurnX < endX)
                {
                    randTurnX = UnityEngine.Random.Range(endX, previousTurnX);
                }

                if (previousTurnY > endY)
                {
                    randTurnY = UnityEngine.Random.Range(previousTurnY, endY);
                }
                else if (previousTurnY == endY)
                {
                    randTurnY = endY;
                }
                else if (previousTurnY < endY)
                {
                    randTurnY = UnityEngine.Random.Range(endY, previousTurnY);
                }

                CreatePaths.CreatePathBetween2Points(new MapPoint(previousTurnX, previousTurnY), new MapPoint(randTurnX, randTurnY));

                previousTurnX = randTurnX;
                previousTurnY = randTurnY;
                t++;
            }

            CreatePaths.CreatePathBetween2Points(new MapPoint(previousTurnX, previousTurnY), new MapPoint(endX, endY));


            //CreatePaths.CreatePathBetween2Points(new MapPoint(startX, startY), new MapPoint(endX, endY));
            Debug.Log("Start: " + startX + "," + startY + "  || End: " + endX + "," + endY);

            //CreatePaths.CreatePathBetween2Points(new MapPoint(10, 5), new MapPoint(20, 30));
            //CreatePaths.CreatePathBetween2Points(new MapPoint(20, 30), new MapPoint(40, 35));
            p++;
        }
    }

    public void MoveGamePlayCam()
    {
        if (!showWholeMap)
        {
            cam1.transform.localPosition = new Vector3(0, 0, -5);
            gamePlayCam.orthographicSize = 10;
        }
        else
        {
            switch (Width)
            {
                case 72:
                    cam1.transform.localPosition = new Vector3(13, 13, -27);
                    gamePlayCam.orthographicSize = 37.5F;
                    break;
                case 96:
                    cam1.transform.localPosition = new Vector3(18, 18, -35);
                    gamePlayCam.orthographicSize = 47.5F;
                    break;
                case 120:
                    cam1.transform.localPosition = new Vector3(23, 23, -44);
                    gamePlayCam.orthographicSize = 57.5F;
                    break;
                case 144:
                    cam1.transform.localPosition = new Vector3(27.5F, 27.5F, -52);
                    gamePlayCam.orthographicSize = 70F;
                    break;
                case 200:
                    cam1.transform.localPosition = new Vector3(40, 40, -72);
                    gamePlayCam.orthographicSize = 95F;
                    break;
                    //case 255:
                    //    DifferentSizeIslands.GenerateGiganticIslands(GenerationMap, tilePool.GetTileSetFromBiomeType(SelectedMapBiome)[0]);
                    //    break;
            }
        }
    }
    public BiomeTileSet GetCurrentBiomeTileSet()
    {
        return tilePool.GetBiomeTileSetFromBiomeType(SelectedMapBiome);
    }

    public void ToggleAutoGen(bool active)
    {
        autoGenActive = active;
        lastGenTime = DateTime.Now;
    }
}
