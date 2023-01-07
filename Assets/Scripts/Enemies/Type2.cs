using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type2 : Enemies
{
    new void Start()
    {
        // Enemy attributes
        base.Start();
        _maxHealth = _maxHealth = Difficulty.modified_type_healths[1];
        _health = _maxHealth;
        _attack = 5;
        _range = Difficulty.modified_type_speed[1];

        max_speed = Difficulty.modified_type_speed[1];
        speed = max_speed;
    }

    new void Update()
    {
        base.Update();
    }
}
