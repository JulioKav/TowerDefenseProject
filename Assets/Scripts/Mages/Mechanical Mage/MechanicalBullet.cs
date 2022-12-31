using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalBullet : MonoBehaviour
{
    private float speed = 10f;
    private Transform target;
    public float explosion_radius = 0f;
    public GameObject impact_effect;
    public GameObject MechanicPrefab;
    public float damage = 20;
    public string Enemy = "Enemy";

    public void Chase(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {   // bullet destroyed if target dies, MAYBE CHANGE TO BULLET DROP?
        if (target == null || target.tag == "AirborneEnemyMechanical")
        {
            Target_Search();
            if (target == null || target.tag == "AirborneEnemyMechanical")
            {
                Destroy(gameObject);
            }

            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distance_per_frame = speed * Time.deltaTime;

        // if the bullet slows, it calls that it has hit target
        if (direction.magnitude <= distance_per_frame)
        {
            hit_target();
            return;
        }
        //bullet follows target
        transform.Translate(direction.normalized * distance_per_frame, Space.World);
        transform.LookAt(target);




    }



    // on hit check if there is explosion radius, explode
    void hit_target()
    {
        //GameObject effect_instance = (GameObject)Instantiate(impact_effect, transform.position, transform.rotation);

        //Destroy(effect_instance, 5f);

        // for cannonball etc
        if (explosion_radius > 0f)
        {
            Explode();
        }



        Destroy(gameObject);

    }

    // checks tags in a collider list in a sphere around explosion point, then deals dmg to all injured enemies
    public void Explode()
    {
        Collider[] collided_objects = Physics.OverlapSphere(transform.position, explosion_radius);
        foreach (Collider collider in collided_objects)
        {

            if (collider.tag == "Enemy")
            {
                if (collider.GetComponent<Tags>().HasTag("Mechanical Enemy"))
                {

                    Raise(collider.transform);
                    Mechanical_damage(collider.transform);

                }


                else
                {

                    Damage_enemy(collider.transform);
                }

            }




        }

    }

    void Target_Search()
    {   //Creates an array of Target Enemies with tag "Enemy".
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Enemy);

        float smallest_distance = Mathf.Infinity;

        GameObject closest_enemy = null;

        //Array search of Targets for closest distance to target, updating closest enemy with shortest distance
        foreach (GameObject enemy in enemies)
        {
            float distance_to_target = Vector3.Distance(transform.position, enemy.transform.position);


            if (distance_to_target < smallest_distance)
            {
                smallest_distance = distance_to_target;

                closest_enemy = enemy;
            }
        }

        // If the enemy is in attack range, it becomes target.
        if (closest_enemy != null)
        {
            target = closest_enemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // updates enemy hp with dmg
    void Damage_enemy(Transform Enemy)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();



        if (enemy_component != null)
        {

            enemy_component.TakeDamage(damage);

        }
    }

    // updates tower hp with dmg

    // specific type dmg
    void Mechanical_damage(Transform Enemy)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();



        if (enemy_component != null)
        {

            enemy_component.TakeDamage(damage * 3);

        }
    }
    /// <summary>
    /// ////////////////////////////////
    /// </summary>
    /// need to smoothly raise rather than teleport the enemies into air!!!!!!! also need animation
    /// need to cancel movement to next waypoint in order to get enemies to float up
    void Raise(Transform Enemy)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();



        if (enemy_component != null && Enemy.tag == "Enemy")
        {
            float saved_speed = enemy_component.speed;
            enemy_component.speed = 0;
            if (Enemy.position.y < 2)
                Enemy.position = Enemy.position + new Vector3(0, 2, 0);
            Enemy.tag = "AirborneEnemyMechanical";
            GameObject tornado = (GameObject)Instantiate(MechanicPrefab, Enemy.position, Enemy.rotation);



        }
    }




    //visual explosion range
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosion_radius);
    }
}
