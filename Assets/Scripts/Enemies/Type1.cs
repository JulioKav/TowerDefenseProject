using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Type1 : Enemies
{
    new void Start()
    {
        // Enemy Attributes
        base.Start();
        _maxHealth = Difficulty.modified_type_healths[0];
        _health = _maxHealth;
        _attack = 5;
        _range = Difficulty.modified_type_speed[0];

        max_speed = Difficulty.modified_type_speed[0];
        speed = max_speed;
    }

    new void Update()
    {
        base.Update();
    }
}
