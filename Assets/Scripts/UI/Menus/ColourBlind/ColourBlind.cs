
using UnityEngine;
using UnityEngine.UI;

public class ColourBlind : MonoBehaviour
{
    public Toggle colourBlind;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("ToggleBool") == 1)
        {
            colourBlind.isOn = true;
        }
        else
        {
            colourBlind.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (colourBlind.isOn == true)
        {
            PlayerPrefs.SetInt("ToggleBool",1);
        }
        else
        {
            PlayerPrefs.SetInt("ToggleBool", 0);
        }
    }
}
