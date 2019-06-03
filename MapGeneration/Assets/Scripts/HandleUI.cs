using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HandleUI : MonoBehaviour
{
    public GameObject BiomeDropdownLabel;
    public GameObject MapTypeDropdownLabel;
    public GameObject RiversToggle;
    public GameObject PathsToggle;
    public GameObject TestingMapLabel;

    private Text TestingMapText;

    public GameObject SavedMapToLoadInput;
    private Text SavedMapToLoadText;

    private Text BiomeText;
    private Text MapTypeText;

    private int MapSize;

    private MapClicks UIClicked;

    void Start()
    {   
        BiomeText = BiomeDropdownLabel.GetComponent<Text>();
        MapTypeText = MapTypeDropdownLabel.GetComponent<Text>();
        UIClicked = GameObject.FindGameObjectWithTag("TileMap").GetComponent<MapClicks>();
        SavedMapToLoadText = SavedMapToLoadInput.GetComponent<Text>();
        TestingMapText = TestingMapLabel.GetComponent<Text>();
    }

    public void GenerateButtonClick(GameObject _generatingText)
    {
        GenerationManager.instance.Height = MapSize;
        GenerationManager.instance.Width = MapSize;
        GenerationManager.instance.HasRivers = RiversToggle.GetComponent<Toggle>().isOn;
        GenerationManager.instance.HasPaths = PathsToggle.GetComponent<Toggle>().isOn;

        switch (MapSize)
        {
            case 72:
                GenerationManager.instance.SetTopDownPos(36F, 36.5F, 65F);
                break;
            case 96:
                GenerationManager.instance.SetTopDownPos(47.7F, 48.3F, 85F);
                break;
            case 120:
                GenerationManager.instance.SetTopDownPos(58.2F, 60F, 105F);
                break;
            case 144:
                GenerationManager.instance.SetTopDownPos(69.3F, 72.4F, 130F);
                break;
            case 200:
                GenerationManager.instance.SetTopDownPos(98.1F, 100F, 175F);
                break;
            case 255:
                GenerationManager.instance.SetTopDownPos(123.3F, 126.5F, 225F);
                break;
            default:

                break;
        }

        GenerationManager.instance.StartGenerationProcess();
    }

    public void SelectBiome()
    {
        switch (BiomeText.text)
        {
            case "Grass":
                GenerationManager.instance.SelectedMapBiome = MapBiome.Grass;
                break;
            case "Snow":
                GenerationManager.instance.SelectedMapBiome = MapBiome.Snow;
                break;
            case "Desert":
                GenerationManager.instance.SelectedMapBiome = MapBiome.Desert;
                break;
            default:
                GenerationManager.instance.SelectedMapBiome = MapBiome.Grass;
                break;
        }
    }

    public void SelectMapType()
    {
        switch (MapTypeText.text)
        {
            case "Plains":
                GenerationManager.instance.SelectedMapTypeX = MapTypeX.Plains;
                break;
            case "Costal":
                GenerationManager.instance.SelectedMapTypeX = MapTypeX.Costal;
                break;
            case "Islands":
                GenerationManager.instance.SelectedMapTypeX = MapTypeX.Islands;
                break;
            default:
                GenerationManager.instance.SelectedMapTypeX = MapTypeX.Plains;
                break;
        }
    }

    public void ToggleRivers(bool active)
    {
        GenerationManager.instance.HasRivers = active;
    }
    public void TogglePaths(bool active)
    {
        GenerationManager.instance.HasPaths = active;
    }

    public void SetSize(int _size)
    {
        MapSize = _size;

        GenerationManager.instance.Height = MapSize;
        GenerationManager.instance.Width = MapSize;
    }

    public void LoadFile()
    {
        List<string[]> listOfStringRows = HandleCSVFile.ReadCSVToListOfStringArrays(Application.dataPath + "/MapCSV's/Montazuma 1 Map Data.csv");     
        CSVToMap.BuildMapFromStringArrayList(listOfStringRows, MapBiome.Grass);
    }

    public void LoadSavedMap()
    {
        try
        {
            List<string[]> listOfStringRows = HandleCSVFile.ReadCSVToListOfStringArrays(Application.dataPath + "/SavedMaps/Map4.csv");
            CSVToMap.BuildMapFromStringArrayList(listOfStringRows, MapBiome.Desert);
        }
        catch
        {
            Debug.Log("Map with this number does not exist");
        }
    }

    public void SaveMap()
    {
        int numMap = Directory.GetFiles(Application.dataPath + "/SavedMaps/", "*.csv").Length;

        string[] mapData = MapToCSV.WriteMapToStringArrays(GenerationManager.instance.GetGameMap());
        HandleCSVFile.WriteStringArrayToCSV(mapData, Application.dataPath + "/SavedMaps/Map" + numMap + ".csv");
    }

    public void LoadTestingMap(string TestingMap)
    {
        // Show which map is being shown

        TestingMapText.text = TestingMap;

        if (TestingMap[0] == '1')
        {
            if (TestingMap[1] == 'A')
            {
                List<string[]> listOfStringRows = HandleCSVFile.ReadCSVToListOfStringArrays(Application.dataPath + "/SavedMaps/MapMont.csv");
                CSVToMap.BuildMapFromStringArrayList(listOfStringRows, MapBiome.Grass);
            }
            else if (TestingMap[1] == 'B')
            {
                List<string[]> listOfStringRows = HandleCSVFile.ReadCSVToListOfStringArrays(Application.dataPath + "/MapCSV's/Montazuma 1 Map Data.csv");
                CSVToMap.BuildMapFromStringArrayList(listOfStringRows, MapBiome.Mont1);
            }
        }
        else if (TestingMap[0] == '2')
        {
            if (TestingMap[1] == 'A')
            {
                List<string[]> listOfStringRows = HandleCSVFile.ReadCSVToListOfStringArrays(Application.dataPath + "/MapCSV's/Vindlandsaga Map Data.csv");              
                CSVToMap.BuildMapFromStringArrayList(listOfStringRows, MapBiome.Vindsaga);
            }
            else if (TestingMap[1] == 'B')
            {
                List<string[]> listOfStringRows = HandleCSVFile.ReadCSVToListOfStringArrays(Application.dataPath + "/SavedMaps/MapVind.csv");
                CSVToMap.BuildMapFromStringArrayList(listOfStringRows, MapBiome.Snow);
            }
        }
        else if (TestingMap[0] == '3')
        {
            if (TestingMap[1] == 'A')
            {
                List<string[]> listOfStringRows = HandleCSVFile.ReadCSVToListOfStringArrays(Application.dataPath + "/MapCSV's/Saladin 1 Map Data.csv");               
                CSVToMap.BuildMapFromStringArrayList(listOfStringRows, MapBiome.Saladin1);
            }
            else if (TestingMap[1] == 'B')
            {
                List<string[]> listOfStringRows = HandleCSVFile.ReadCSVToListOfStringArrays(Application.dataPath + "/SavedMaps/MapSal.csv");
                CSVToMap.BuildMapFromStringArrayList(listOfStringRows, MapBiome.Desert);
            }
        }
        else if (TestingMap[0] == '4')
        {
            if (TestingMap[1] == 'A')
            {
                List<string[]> listOfStringRows = HandleCSVFile.ReadCSVToListOfStringArrays(Application.dataPath + "/SavedMaps/MapElCid.csv");
                CSVToMap.BuildMapFromStringArrayList(listOfStringRows, MapBiome.Desert);
            }
            else if (TestingMap[1] == 'B')
            {
                List<string[]> listOfStringRows = HandleCSVFile.ReadCSVToListOfStringArrays(Application.dataPath + "/MapCSV's/El Cid 5 Map Data.csv");
                CSVToMap.BuildMapFromStringArrayList(listOfStringRows, MapBiome.ElCid5);
            }
        }      
    }
}
