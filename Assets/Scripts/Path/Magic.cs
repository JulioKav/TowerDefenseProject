using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Magic : Enemies
{
    private void Start()
    {
        _wavepointIndex = 0;
        _attackRange = 10;
        _attackSpeed = 2;
        _attackDamage = 5;
        _health = 100;
        _target = Waypoints.points[0];
    }

    private new void Update()
    {
        base.Update();
    }
}
