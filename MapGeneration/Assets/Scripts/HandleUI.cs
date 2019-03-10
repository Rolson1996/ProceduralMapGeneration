using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleUI : MonoBehaviour
{
    public GameObject BiomeDropdownLabel;
    private Text BiomeText;

    private int MapSize;

    void Start()
    {   
        BiomeText = BiomeDropdownLabel.GetComponent<Text>();
    }

    public void GenerateButtonClick(GameObject _generatingText)
    {
        var test = Time.time;
       GenerationManager.instance.Height = MapSize;
       GenerationManager.instance.Width = MapSize;

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

    }

    public void SetSize(int _size)
    {
        MapSize = _size;
    }
}
