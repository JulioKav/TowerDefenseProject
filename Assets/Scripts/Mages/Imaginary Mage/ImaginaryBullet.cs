using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImaginaryBullet : MonoBehaviour
{

    private Transform target;

    public float search_radius = 0f;


    public GameObject toxic_floor;

    public int damage = 50;


    public GameObject identity_of_shooter;
    public void Chase(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {   // bullet destroyed if target dies, MAYBE CHANGE TO BULLET DROP?
       




    }


    // on hit check if there is explosion radius or not, then check type of enemy and return a form of take damage or explode
    

    // checks tags in a collider list in a sphere around explosion point, then deals dmg to all injured enemies
    void get_path_tiles()
    {
        Collider[] collided_objects = Physics.OverlapSphere(transform.position, search_radius);
        foreach (Collider collider in collided_objects)
        {
            if (collider.tag == "Road")
            {

                make_floor_toxic(collider.transform);

            }




        }

    }

    void make_floor_toxic(Transform floor)
    {
        Destroy(floor);
        Instantiate(toxic_floor, floor.position, floor.rotation);
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




    void Imaginary_damage(Transform Enemy)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();



        if (enemy_component != null)
        {

            enemy_component.TakeDamage(damage * 3);

        }
    }

    //visual explosion range
    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, search_radius);
    }
}
