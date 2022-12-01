using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public float speed = 50f;
    public float time_airborne = 1f;
    private Transform target;

    public float explosion_radius = 0f;

    public GameObject impact_effect;
    public GameObject tornadoprefab;
    public int damage = 50;

    public GameObject identity_of_shooter;
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


    // on hit check if there is explosion radius or not, then check type of enemy and return a form of take damage or explode
    void hit_target()
    {
        //GameObject effect_instance = (GameObject)Instantiate(impact_effect, transform.position, transform.rotation);

        //Destroy(effect_instance, 5f);

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
                    Damage_enemy(target);
            }
            
        }


        Destroy(gameObject);

    }

    // checks tags in a collider list in a sphere around explosion point, then deals dmg to all injured enemies
    void Explode()
    {
        Collider[] collided_objects = Physics.OverlapSphere(transform.position, explosion_radius);
        foreach (Collider collider in collided_objects)
        {
            if (collider.tag == "Enemy")
            {
                if (collider.GetComponent<Tags>().HasTag("Magic Enemy"))
                {
                    
                        Raise(collider.transform);
                        Magic_damage(collider.transform);
                        
                }

              
                else
                {
                    
                    Damage_enemy(collider.transform);
                }

            }




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
    void Magic_damage(Transform Enemy)
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
    /// need to smoothly raise rather than teleport the enemies into air!!!!!!! also need animation + lowering to work
    void Raise(Transform Enemy)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();



        if (enemy_component != null)
        {
            float saved_speed = enemy_component.speed;
            enemy_component.speed = 0;
            if (Enemy.position.y<2)
                Enemy.position = Enemy.position + new Vector3(0, 2, 0);
                Enemy.tag = "Airborne Enemy";
                GameObject tornado = (GameObject)Instantiate(tornadoprefab, Enemy.position - new Vector3(0, 2, 0), Enemy.rotation);
                
            //StartCoroutine(LowerAfterTime(1, Enemy, saved_speed));

        }
    }

    IEnumerator LowerAfterTime(float time, Transform Enemy, float saved_speed)
    {
        
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        Lower(Enemy, saved_speed);
    }
    void Lower(Transform Enemy,float saved_speed)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();
        if (enemy_component != null)
        {
            if (Enemy.position.y == 2)
                Enemy.position = Enemy.position - new Vector3(0, 2, 0);
                Enemy.tag = "Enemy";
                enemy_component.speed = saved_speed;
        }
    }

    //visual explosion range
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosion_radius);
    }
}

