using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Type1 : Enemies
{
    void Start()
    {
        _health = 100;
        _attack = 5;
        _range = 10;
        base.Start();
    }
    
    void Update()
    {
        base.Update();
    }
}
