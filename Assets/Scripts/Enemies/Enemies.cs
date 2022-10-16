using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float speed = 4.0f;
    protected float _health;
    protected float _attack;
    protected float _range;
    protected Transform _target;
    
    private int wavepointIndex = 0;
    
    protected void Start()
    {
        _target = Waypoints.points[0];
    }

    protected void Update()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        _target = Waypoints.points[wavepointIndex];
    }
}
