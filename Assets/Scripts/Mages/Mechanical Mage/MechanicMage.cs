using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicMage : MonoBehaviour
{
    public float damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Target_Search();
    }

    void Target_Search()
    {
        Collider[] collided_objects = Physics.OverlapSphere(transform.position, 20f);
        foreach (Collider collider in collided_objects)
        {

            if (collider.tag == "Enemy")
            {
                Throw(collider);


                if (collider.GetComponent<Tags>().HasTag("Mechanical Enemy"))
                {
                    Mechanical_damage(collider.transform);
                }

                else
                {
                    Damage_enemy(collider.transform);
                }


            }
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

    void Mechanical_damage(Transform Enemy)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();



        if (enemy_component != null)
        {

            enemy_component.TakeDamage(damage * 3);
        }
    }

    void Throw(Collider collider)
    {
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        GameObject spawn = spawnpoints[0];
        foreach (GameObject i in spawnpoints)
        {
            if ((collider.transform.position - i.transform.position).magnitude < (collider.transform.position - spawn.transform.position).magnitude)
            {
                spawn = i;
            }
            
        }

        Enemies enemy_component = collider.GetComponent<Enemies>();
        //enemy_component.speed = 0f;
        //GetComponent(Enemy).enabled = false;
        //collider.transform.position = spawn.transform.position * Time.deltaTime;
    }
}

