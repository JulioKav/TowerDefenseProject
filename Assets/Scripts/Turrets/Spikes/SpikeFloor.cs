using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpikeFloor : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("Unity Stuff")]
    public Image healthBar;

    [Header("Turret Stats")]

    public float attack_range;
    public float attack_speed = 50f;


    public float _health = 100;
    public float _maxHealth = 100;

    public int damage = 1;

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
        Gizmos.DrawWireSphere(transform.position, attack_range);
    }

    // gets colliders in physics sphere around tower, checks if they are enemy, then applies a chosen slow amount and does dmg.
    void Target_Search()
    {

        Collider[] collided_objects = Physics.OverlapSphere(transform.position, attack_range);
        foreach (Collider collider in collided_objects)
        {
            if (collider.tag == "Enemy")
            {

                Damage_enemy(collider.transform);


            }
        }
    }


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


    void Damage_enemy(Transform Enemy)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();



        if (enemy_component != null)
        {

            enemy_component.TakeDamage(damage);
        }
    }
}
