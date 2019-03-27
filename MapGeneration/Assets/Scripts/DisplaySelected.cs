using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySelected : MonoBehaviour {

    public GameObject LocationTextGameObject;
    public GameObject TitleTextGameObject;
    public GameObject TerrainImage;

    private Text LocationText;
    private Text TitleText;
    private Image TerrainSprite;

    private void Start()
    {
        TitleText = TitleTextGameObject.GetComponent<Text>();
        LocationText = LocationTextGameObject.GetComponent<Text>();
        TerrainSprite = TerrainImage.GetComponent<Image>();
    }
    public void Deselect()
    {
        TitleText.text = "";
        LocationText.text = "";
        TerrainSprite.sprite =null;
    }
    public void Select(string title, int x, int y, Sprite sprite)
    {
        TitleText.text = title;
        LocationText.text = "X: " + x.ToString() + " | Y: " + y.ToString();
        TerrainSprite.sprite = sprite;

    }
}
