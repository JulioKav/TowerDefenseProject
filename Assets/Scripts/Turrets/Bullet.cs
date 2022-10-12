using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;

    private Transform target;

    public float explosion_radius = 0f;

    public GameObject impact_effect;



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
        } else
        {
            Damage(target);
        }


        Destroy(gameObject);

    }
    
    void Explode ()
    {
        Collider [] collided_objects = Physics.OverlapSphere(transform.position, explosion_radius);
        foreach (Collider collider in collided_objects)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }

    }    
    
    void Damage (Transform Enemy)
    {
        Destroy(Enemy.gameObject);
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosion_radius);
    }
}
