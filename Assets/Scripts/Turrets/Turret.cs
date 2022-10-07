using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform Target;

    [Header("Turret Stats")]

    public float attack_range;
    public float attack_speed = 1f;
    private float attack_countdown = 0f;

    [Header("Unity Required Stuff")]

    public string Enemy = "Enemy";
    public float turn_speed = 5f;
    public GameObject bulletprefab;
    public Transform firepoint;
    
    // USE THIS WHEN YOU HAVE ASSETS, NAMELY A PART OF TURRET YOU WANT TO ROTATE NOT WHOLE THING
    public Transform part_to_rotate;


    // Start is called before the first frame update
    void Start()
    {
        // Calls Target_Search every chosen amount seconds.
        InvokeRepeating("Target_Search", 0f, 1.0f);
    }

    // Draws a 3D wire mesh range around turret.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attack_range);
    }

    // Looks for closest target
    void Target_Search ()
    {   //Creates an array of Target Enemies with tag "Enemy".
        GameObject[] Targets = GameObject.FindGameObjectsWithTag(Enemy);
        
        float smallest_distance = Mathf.Infinity;

        GameObject closest_enemy = null;

        //Array search of Targets for closest distance to target, updating closest enemy with shortest distance
        foreach(GameObject enemy in Targets)
        {
            float distance_to_target = Vector3.Distance(transform.position, enemy.transform.position);


            if (distance_to_target < smallest_distance)
            {
                smallest_distance = distance_to_target;

                closest_enemy = enemy;
            }
        }

        // If the enemy is in attack range, it becomes target.
        if (closest_enemy != null && smallest_distance <= attack_range)
        {
            Target = closest_enemy.transform;
        }
        else
        {
            Target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
            return;


        
        //Make turret face direction of target (enemy)
        Vector3 direction = Target.position - transform.position;
        Quaternion look_at_target = Quaternion.LookRotation(direction);
        Vector3 look_at_target_euler = Quaternion.Lerp(transform.rotation,look_at_target, Time.deltaTime * turn_speed).eulerAngles;

        transform.rotation = Quaternion.Euler (0f, look_at_target_euler.y, 0f);
        // WITH ASSET INCLUDE BELOW
        //Chance there may be 90degree offset
        //partToRotate.rotation = Quaternion.Euler (0f, look_at_target_euler.y, 0f);

        if (attack_countdown <= 0f)
        {
            shoot();
            attack_countdown = 1f / attack_speed;
        }

        attack_countdown -= Time.deltaTime;

    }

    void shoot()
    {
       GameObject bulletGO = (GameObject)Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
       Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Chase(Target);
        }


    }

}

