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
                GenerationManager.instance.SelectedMapBiome = MapBiome.Grass;
                break;
            case "Snow":
                GenerationManager.instance.SelectedMapBiome = MapBiome.Snow;
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

    public void SetSize(int _size)
    {
        MapSize = _size;      
    }
}
