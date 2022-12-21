using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type4 : Enemies
{
    new void Start()
    {
        // Enemy attributes
        base.Start();
        _maxHealth = 150;
        _health = _maxHealth;
        _attack = 5;
        _range = 10;
    }

    new void Update()
    {
        base.Update();
    }
}
