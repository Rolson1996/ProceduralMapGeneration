using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleUI : MonoBehaviour
{
    public GameObject BiomeDropdownLabel;
    public GameObject MapTypeDropdownLabel;
    public GameObject RiversToggle;

    private Text BiomeText;
    private Text MapTypeText;

    private int MapSize;

    private MapClicks UIClicked;

    void Start()
    {   
        BiomeText = BiomeDropdownLabel.GetComponent<Text>();
        MapTypeText = MapTypeDropdownLabel.GetComponent<Text>();
        UIClicked = GameObject.FindGameObjectWithTag("TileMap").GetComponent<MapClicks>();
    }

    public void GenerateButtonClick(GameObject _generatingText)
    {
        var test = Time.time;
        GenerationManager.instance.Height = MapSize;
        GenerationManager.instance.Width = MapSize;
        GenerationManager.instance.HasRivers = RiversToggle.GetComponent<Toggle>().isOn;

        switch (MapSize)
        {
            case 72:
                GenerationManager.instance.SetTopDownPos(27.6F, 54F, 29.5F);
                break;
            case 96:
                GenerationManager.instance.SetTopDownPos(29.1F, 69F, 38.4F);
                break;
            case 120:
                GenerationManager.instance.SetTopDownPos(36.4F, 87.6F, 48.1F);
                break;
            case 144:
                GenerationManager.instance.SetTopDownPos(43.6F, 100.79F, 57.4F);
                break;
            case 200:
                GenerationManager.instance.SetTopDownPos(58.8F, 142.5F, 80.7F);
                break;
            case 255:
                GenerationManager.instance.SetTopDownPos(81.4F, 187.7F, 101.2F);
                break;
            default:

                break;
        }

        _generatingText.SetActive(true);
        Canvas.ForceUpdateCanvases();
        GenerationManager.instance.StartGenerationProcess();
        _generatingText.SetActive(false);

        var test2 = Time.time;

    }

    public void SelectBiome()
    {
        switch (BiomeText.text)
        {
            case "Grass":
                GenerationManager.instance.SelectedMapType = MapType.GrassPlains;
                break;
            case "Snow":
                GenerationManager.instance.SelectedMapType = MapType.SnowPlains;
                break;
            default:
                GenerationManager.instance.SelectedMapType = MapType.GrassPlains;
                break;
        }

        UIClicked.CanvasClicked();

    }

    public void SelectMapType()
    {
        switch (MapTypeText.text)
        {
            case "Plains":
                
                break;
            case "Costal":
               
                break;
            case "Islands":
               
                break;
            case "Forest":
               
                break;
            default:
                
                break;
        }
        UIClicked.CanvasClicked();
    }

    public void SetSize(int _size)
    {
        MapSize = _size;      
    }
}
