using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleUI : MonoBehaviour
{

    public GameObject HeightText;
    public GameObject WidthText;

    public void GenerateButtonClick(GameObject _generatingText)
    {
        int.TryParse(HeightText.GetComponent<Text>().text, out GenerationManager.instance.Height);
        int.TryParse(WidthText.GetComponent<Text>().text, out GenerationManager.instance.Width);

        _generatingText.SetActive(true);
        Canvas.ForceUpdateCanvases();
        GenerationManager.instance.StartGenerationProcess();
        _generatingText.SetActive(false);

    }
}
