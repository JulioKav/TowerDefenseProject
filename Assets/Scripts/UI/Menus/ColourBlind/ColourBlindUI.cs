using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourBlindUI : MonoBehaviour
{
    private Renderer renderer;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        Color red = new Color32(255, 40, 23, 1);
        Color blue = new Color32(12, 123, 220, 1);
        image = GetComponent<Image>();

        if (PlayerPrefs.GetInt("ToggleBool") == 1)
        {

            image.material.color = blue;
        }
        else
        {
            image.material.color = red;
        }
    }


}
