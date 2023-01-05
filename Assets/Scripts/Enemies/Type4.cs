using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type4 : Enemies
{
    new void Start()
    {
        // Enemy attributes
        base.Start();
        _maxHealth = Difficulty.modified_type_healths[3];
        _health = _maxHealth;
        _attack = 5;
        _range = Difficulty.modified_type_speed[3];

        max_speed = Difficulty.modified_type_speed[3];
        speed = max_speed;
    }

    new void Update()
    {
        base.Update();
    }
}
