using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    // Start is called before the first frame update
    public float attack_range = 2f;
    public float damage = 2;
    public float attack_speed = 50f;
    private Vector3 offset = new Vector3(0, -2, 0);
    private Transform target;
    public Animator m_Animator;
    void Start()
    {
        // Calls Target_Search every chosen amount seconds.
        InvokeRepeating("Target_Search", 0f, 1 / attack_speed);
        following_Search();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Enemy_Alive_Checker();
        if (target != null) transform.position = target.transform.position + offset;

    }
    // damage
    void Target_Search()
    {

        Collider[] collided_objects = Physics.OverlapSphere(transform.position, attack_range);
        foreach (Collider collider in collided_objects)
        {
            if (collider.tag == "Enemy" || collider.tag == "AirborneEnemyMagic")
            {

                if (collider.GetComponent<Tags>().HasTag("Imaginary Enemy"))
                {

                    Magic_damage(collider.transform);
                }

                else
                {
                    Damage_enemy(collider.transform);
                }


            }
        }
    }

    // get airborne enemies with tag, check they're in range, if they are add to list, if list not empty tornado continues, if empty destroyed
    List<GameObject> enemies_in_range = new List<GameObject>();
    void Enemy_Alive_Checker()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("AirborneEnemyMagic");
        foreach (GameObject enemy in enemies)
        {
            float distance_to_target = Vector3.Distance(transform.position, enemy.transform.position);


            if (distance_to_target < attack_range)
            {
                enemies_in_range.Add(enemy);
            }


        }

        if (enemies_in_range.Count == 0)
        {
            //death animation here
            m_Animator.SetTrigger("Death");
            //m_Animator.ResetTrigger("Death");
            Debug.Log("anim");
            //Destroy(gameObject);

        }
        enemies_in_range.Clear();
    }


    void Magic_damage(Transform Enemy)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();



        if (enemy_component != null)
        {

            enemy_component.TakeDamage(damage * 3);

        }
    }

    void Damage_enemy(Transform Enemy)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();



        if (enemy_component != null)
        {

            enemy_component.TakeDamage(damage);

        }
    }

    void following_Search()
    {   //Creates an array of Target Enemies with tag "Enemy".
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("AirborneEnemyMagic");

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
            target = closest_enemy.transform;

        }

    }
}
