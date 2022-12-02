using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagineTower : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("Unity Stuff")]
    public Image healthBar;

    [Header("Turret Stats")]

    public float slow_range;
    public float attack_speed = 50f;


    public float _health = 100;
    public float _maxHealth = 100;

    public float damage = 1;

    public string Enemy = "Enemy";
    void Start()
    {
        // Calls Target_Search every chosen amount seconds.
        InvokeRepeating("Target_Search", 0f, 1 / attack_speed);

    }

    // Draws a 3D wire mesh range around turret.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, slow_range);
    }

    // gets colliders in physics sphere around tower, checks if they are enemy, then applies a chosen slow amount and does dmg.
    void Target_Search()
    {




        Collider[] collided_objects = Physics.OverlapSphere(transform.position, slow_range);
        foreach (Collider collider in collided_objects)
        {
            if (collider.tag == "Enemy")
            {
                slow_enemy_x2();

                if (collider.GetComponent<Tags>().HasTag("Imaginary Enemy"))
                {
                    Imaginary_damage(collider.transform);
                }

                else
                {
                    Damage_enemy(collider.transform);
                }


            }
        }
    }


    //
    //Not sure if needed

    //void FixedUpdate()
    //{
    //    if (target == null)
    //    {
    //        return;
    //    }


    //}




    // hp gets minused from current hp
    // healthbar is made used the ratio of health to max health 
    public void TakeDamage(int amountOfDamage)
    {
        _health -= amountOfDamage;

        healthBar.fillAmount = _health / _maxHealth;

        if (_health <= 0)
        {//could place death animation here
            Die();
        }
    }

    // hp gets added from current hp
    // healthbar is made used the ratio of health to max health 
    public void HealDamage(int amountOfDamage)
    {
        _health += amountOfDamage;

        healthBar.fillAmount = _health / _maxHealth;

        if (_health <= 0)
        {//could place death animation here
            Die();
        }
    }

    //destroys game object
    void Die()
    {
        Destroy(gameObject);
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

    void Damage_enemy(Transform Enemy)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();



        if (enemy_component != null)
        {

            enemy_component.TakeDamage(damage);
        }
    }


    //sets enemy speed as slower
    void slow_enemy_x2()
    {
        Collider[] collided_objects = Physics.OverlapSphere(transform.position, slow_range);
        foreach (Collider collider in collided_objects)
        {
            Enemies enemy_component = collider.GetComponent<Enemies>();

            if (collider.tag == "Enemy")
            {
                if (collider.GetComponent<Tags>().HasTag("Magic Enemy"))
                {
                    enemy_component.speed = 1;
                }
                else

                if (collider.GetComponent<Tags>().HasTag("Physical Enemy"))
                {
                    enemy_component.speed = 1;
                }
                else

                if (collider.GetComponent<Tags>().HasTag("Imaginary Enemy"))
                {
                    enemy_component.speed = 1;
                }
                else

                if (collider.GetComponent<Tags>().HasTag("Mechanical Enemy"))
                {
                    enemy_component.speed = 1;
                }

                else
                {
                    enemy_component.speed = 1;
                }
            }
        }
    }

    void slow_enemy_x4()
    {
        Collider[] collided_objects = Physics.OverlapSphere(transform.position, slow_range);
        foreach (Collider collider in collided_objects)
        {
            Enemies enemy_component = collider.GetComponent<Enemies>();

            if (collider.tag == "Enemy")
            {
                if (collider.GetComponent<Tags>().HasTag("Magic Enemy"))
                {
                    enemy_component.speed = 0.5f;
                }
                else

                if (collider.GetComponent<Tags>().HasTag("Physical Enemy"))
                {
                    enemy_component.speed = 0.5f;
                }
                else

                if (collider.GetComponent<Tags>().HasTag("Imaginary Enemy"))
                {
                    enemy_component.speed = 0.5f;
                }
                else

                if (collider.GetComponent<Tags>().HasTag("Mechanical Enemy"))
                {
                    enemy_component.speed = 0.5f;
                }

                else
                {
                    enemy_component.speed = 0.5f;
                }
            }
        }
    }

}
