using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("Unity Stuff")]
    public Image healthBar;

    [Header("Turret Stats")]

    public float attack_range;
    public float attack_speed = 1f;
    private float attack_countdown = 0f;
    public float _health = 100;
    public float _maxHealth = 100;

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
        InvokeRepeating("Target_Search", 0f, 0.1f);
        
        
    }

    // Draws a 3D wire mesh range around turret.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attack_range);
    }

    // Looks for closest target
    void Target_Search()
    {   //Creates an array of Target Enemies with tag "Enemy".
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Enemy);

        float smallest_distance = Mathf.Infinity;

        GameObject closest_enemy = null;

        bool wall_active = false;
        //Array search of Targets for closest distance to target, updating closest enemy with shortest distance
        foreach (GameObject enemy in enemies)
        {
            float distance_to_target = Vector3.Distance(transform.position, enemy.transform.position);


            if (distance_to_target < smallest_distance)
            {
                smallest_distance = distance_to_target;

                closest_enemy = enemy;
            }
            /*
            if (gameObject.tag == "Enemy" && enemy.GetComponent<Tags>().HasTag("Wall") == true && distance_to_target <= attack_range)
            {
                wall_active = true;
                gameObject.GetComponent<Enemies>().speed = 0;

            }
            */
        }
        
        // If the enemy is in attack range, it becomes target.
        /*
        if(wall_active == false)
        {
            gameObject.GetComponent<Enemies>().speed = 4;
        }
        */
        if (closest_enemy != null && smallest_distance <= attack_range)
            {   if(wall_active == false)
                target = closest_enemy.transform;

            }

            else
            {
                target = null;

            }


        

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            attack_countdown = attack_speed / 2;
            Target_Search();
            return;
        }


        //Make turret face direction of target (enemy)
        Vector3 direction = target.position - transform.position;
        Quaternion look_at_target = Quaternion.LookRotation(direction);
        Vector3 look_at_target_euler = Quaternion.Lerp(transform.rotation, look_at_target, Time.deltaTime * turn_speed).eulerAngles;


        //transform.rotation = Quaternion.Euler (0f, look_at_target_euler.y, 0f);
        // WITH ASSET INCLUDE BELOW
        //Chance there may be 90degree offset
        part_to_rotate.rotation = Quaternion.Euler(0f, look_at_target_euler.y, 0f);

        if (attack_countdown <= 0f)
        {
            shoot();
            attack_countdown = 1f / attack_speed;
        }

        attack_countdown -= Time.deltaTime;


    }


    // turret shooting out a bullet, instantiating the bulletprefab, bullet chases target.
    void shoot()
    {

        GameObject bulletGO = (GameObject)Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();


        if (bullet != null)
        {
            bullet.Chase(target);
        }


    }

    // hp gets minused from current hp
    // healthbar is made used the ratio of health to max health 
    public void TakeDamage(float amountOfDamage)
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
    public void HealDamage(float amountOfDamage)
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

    

    
}


