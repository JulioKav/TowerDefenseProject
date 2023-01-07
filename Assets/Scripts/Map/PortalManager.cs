using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    // Inspired by: https://answers.unity.com/questions/429697/how-will-i-get-animated-gif-images-in-scene.html

    public bool portalActive = false;

    public GameObject front;
    public GameObject back;

    public Light pointLight;

    public Texture2D inactivePortal;
    public Texture2D[] activePortalFrames;
    int framesPerSecond = 10;

    // Start is called before the first frame update
    void Start()
    {
        pointLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!portalActive) return;

        int index = (int)(Time.time * framesPerSecond) % activePortalFrames.Length;
        front.GetComponent<MeshRenderer>().material.mainTexture = activePortalFrames[index];
        back.GetComponent<MeshRenderer>().material.mainTexture = activePortalFrames[index];
    }

    public void ActivatePortal()
    {
        portalActive = true;
        pointLight.enabled = true;
    }
}
