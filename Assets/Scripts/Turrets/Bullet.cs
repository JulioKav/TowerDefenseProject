using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;

    private Transform target;

    public float explosion_radius = 0f;

    public GameObject impact_effect;

    public int damage = 50;


    public void Chase(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {   // bullet destroyed if target dies, MAYBE CHANGE TO BULLET DROP?
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distance_per_frame = speed * Time.deltaTime;

        if (direction.magnitude <= distance_per_frame)
        {
            hit_target();
            return;
        }

        transform.Translate(direction.normalized * distance_per_frame, Space.World);
        transform.LookAt(target);




    }



    void hit_target()
    {
        GameObject effect_instance = (GameObject)Instantiate(impact_effect, transform.position, transform.rotation);

        Destroy(effect_instance, 5f);

        // for cannonball etc
        if (explosion_radius > 0f)
        {
            Explode();
        }
        else
        {
            if (target.tag == "Enemy")
            {
                if (target.GetComponent<Tags>().HasTag("Magic Enemy"))
                {
                    Magic_damage(target);
                }
                else

                if (target.GetComponent<Tags>().HasTag("Physical Enemy"))
                {
                    Physical_damage(target);
                }
                else

                if (target.GetComponent<Tags>().HasTag("Imaginary Enemy"))
                {
                    Imaginary_damage(target);
                }
                else

                if (target.GetComponent<Tags>().HasTag("Mechanical Enemy"))
                {
                    Mechanical_damage(target);
                }
                else
                    Damage_enemy(target);
            }
            if (target.tag == "Tower") Damage_tower(target);
        }


        Destroy(gameObject);

    }

    void Explode()
    {
        Collider[] collided_objects = Physics.OverlapSphere(transform.position, explosion_radius);
        foreach (Collider collider in collided_objects)
        {
            if (collider.tag == "Enemy")
            {
                if (target.GetComponent<Tags>().HasTag("Magic Enemy"))
                {
                    Magic_damage(collider.transform);
                }

                if (target.GetComponent<Tags>().HasTag("Physical Enemy"))
                {
                    Physical_damage(collider.transform);
                }

                else if (target.GetComponent<Tags>().HasTag("Imaginary Enemy"))
                {
                    Imaginary_damage(collider.transform);
                }
                else if (target.GetComponent<Tags>().HasTag("Mechanical Enemy"))
                {
                    Mechanical_damage(collider.transform);
                }
                else
                    Damage_enemy(collider.transform);
            }



            if (collider.tag == "Tower")
            {
                Damage_tower(collider.transform);
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

    void Damage_tower(Transform Tower)
    {
        // retrieves script aspect of enemy
        Turret turret_component = Tower.GetComponent<Turret>();

        if (turret_component != null)
        {
            turret_component.TakeDamage(damage);
        }
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

    void Physical_damage(Transform Enemy)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();



        if (enemy_component != null)
        {

            enemy_component.TakeDamage(damage * 3);
        }
    }

    void Imaginary_damage(Transform Enemy)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();



        if (enemy_component != null)
        {

            enemy_component.TakeDamage(damage * 3);
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

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosion_radius);
    }
}
