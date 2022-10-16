using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour 
{
    public float speed = 10f;
    
    protected int _wavepointIndex = 0;
    protected float _attackRange;
    protected float _attackDamage;
    protected float _attackSpeed;
    protected float _health;
    protected Transform _target;
    
    

    void Start()
    {
        _target = Waypoints.points[0];
    }

    protected void Update()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (_wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        _wavepointIndex++;
        _target = Waypoints.points[_wavepointIndex];
    }
}
