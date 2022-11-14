using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{

    public delegate void WaveStartEvent();
    public static event WaveStartEvent OnWaveStart;

    public WaveSpawner waveSpawner;

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Button>().interactable = !waveSpawner.waveOnGoing;
    }

    public void EmitWaveStartEvent()
    {
        if (OnWaveStart != null) OnWaveStart();
    }
}
