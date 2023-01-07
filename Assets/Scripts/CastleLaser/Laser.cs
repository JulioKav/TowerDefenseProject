using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    public bool isAttacking = false;

    private Transform target;
    private Enemy targetEnemy;

    [Header("Unity Stuff")]
    public Image healthBar;

    [Header("Laser")]
    public LineRenderer lineRenderer;

    [Header("Turret Stats")]
    public float damage = 2f;
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

    public bool finalskillunlocked = false;
    // Start is called before the first frame update
    void Start()
    {
        // Calls Target_Search every chosen amount seconds.
        InvokeRepeating("Target_Search", 0f, 0.1f);


    }

    // Draws a 3D wire mesh range around turret.
    

    // Looks for closest target
    void Target_Search()
    {   //Creates an array of Target Enemies with tag "Enemy".
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(Enemy);

        float smallest_distance = Mathf.Infinity;

        GameObject closest_enemy = null;

        
        //Array search of Targets for closest distance to target, updating closest enemy with shortest distance
        foreach (GameObject enemy in enemies)
        {
            


           if(enemy.GetComponent<Tags>().HasTag("Boss"))
            {
                float distance_to_target = Vector3.Distance(transform.position, enemy.transform.position);
                closest_enemy = enemy;
            }
               
            
           
        }

       
        if (closest_enemy != null  )
        {
            
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
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;

            }

            return;
        }
        

       
        if (finalskillunlocked == true)
        {
            Laser_shoot();
        }
        


    }


    // turret shooting out a bullet, instantiating the bulletprefab, bullet chases target.


    void Laser_shoot()
    {
        target.GetComponent<Boss>().TakeDamage(damage );
        if(!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;

        }
        lineRenderer.SetPosition(0, firepoint.position);
        lineRenderer.SetPosition(1, target.position + new Vector3(0,1,0));
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
