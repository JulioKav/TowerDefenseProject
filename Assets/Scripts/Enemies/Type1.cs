using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Type1 : Enemies
{
    new void Start()
    {
        _maxHealth = 100;
        _health = _maxHealth;
        _attack = 5;
        _range = 10;
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }
}
