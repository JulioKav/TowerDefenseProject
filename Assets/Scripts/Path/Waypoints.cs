using System;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    // The waypoints are gameobjects grouped under a Waypoints-SPX-PathY game object, for spawn point X and a different
    // path Y. Those Waypoints-SPX-PathY variables are added here, to hold the different paths in code.
    public Transform[] Paths;

}
