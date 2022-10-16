using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.UI;

public class BuildingPlacable : MonoBehaviour
{
    public Transform turrent;
    //public Transform parent;
    // Start is called before the first frame update
    private void OnMouseUp()
    {
        Vector3 pos = transform.position;
        pos.y += 0.25f;

        Instantiate(turrent, pos, transform.rotation);
    }
}
