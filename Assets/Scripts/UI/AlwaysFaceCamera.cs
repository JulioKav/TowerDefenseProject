using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFaceCamera : MonoBehaviour
{
    public bool rescaleUI = true;

    Transform mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main.transform;
        if (rescaleUI) transform.localScale /= transform.parent.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCam, mainCam.up);
    }
}
