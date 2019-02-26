using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleUI : MonoBehaviour
{

    public GameObject HeightInput;
    public GameObject WidthInput;
    public GameObject BiomeDropdownLabel;

    private Text HeightText;
    private Text WidthText;
    private Text BiomeText;

    void Start()
    {
        HeightText = HeightInput.GetComponent<Text>();
        WidthText = WidthInput.GetComponent<Text>();
        BiomeText = BiomeDropdownLabel.GetComponent<Text>();
    }

    public void GenerateButtonClick(GameObject _generatingText)
    {
        var test = Time.time;
        int.TryParse(HeightText.text, out GenerationManager.instance.Height);
        int.TryParse(WidthText.text, out GenerationManager.instance.Width);

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
}
