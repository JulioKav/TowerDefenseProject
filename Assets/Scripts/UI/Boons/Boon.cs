using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InitButton();
    }

    void InitButton()
    {
        GetComponent<Button>().onClick.AddListener(UIStateManager.Instance.BoonSelected);
    }
}
