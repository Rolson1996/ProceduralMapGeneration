using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleUI : MonoBehaviour {

    public GameObject HeightText;
    public GameObject WidthText;

    public void GenerateButtonClick()
    {

        int.TryParse(HeightText.GetComponent<Text>().text, out GenerationManager.instance.Height);
        int.TryParse(WidthText.GetComponent<Text>().text, out GenerationManager.instance.Width);


        GenerationManager.instance.StartGenerationProcess();
    }
}
