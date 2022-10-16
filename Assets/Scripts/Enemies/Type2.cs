using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type2 : Enemies
{
    // Start is called before the first frame update
    void Start()
    {
        _health = 150;
        _attack = 5;
        _range = 10;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
