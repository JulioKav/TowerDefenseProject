using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPlacable : MonoBehaviour
{
    //public Transform parent;
    // Start is called before the first frame update
    public void PlaceTower(GameObject turret, float yOffset)
    {
        Vector3 pos = transform.position;
        pos.y += yOffset;

        Instantiate(turret, pos, transform.rotation);
    }
}
