using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CBlindObject : MonoBehaviour
{
    private Renderer renderer;
    

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        Color green = new Color32(0, 212, 42, 1);
        Color yellow = new Color32(255, 194, 10, 1);
        

        if (PlayerPrefs.GetInt("ToggleBool") == 1)
        {

            renderer.material.color = yellow;
        }
        else
        {
            renderer.material.color = green;
        }
    }
}

