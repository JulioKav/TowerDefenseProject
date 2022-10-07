using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    private Transform Target;
    public GameObject impact_effect;




    public void Chase (Transform _Target)
    {
        Target = _Target;
    }

    // Update is called once per frame
    void Update()
    {   // bullet destroyed if target dies, MAYBE CHANGE TO BULLET DROP?
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = Target.position - transform.position;
        float distance_per_frame = speed * Time.deltaTime;

        if(direction.magnitude <= distance_per_frame)
        {
            hit_target();
            return;
        }

        transform.Translate(direction.normalized * distance_per_frame, Space.World);




    }

    void hit_target()
    {
        GameObject effect_instance = (GameObject) Instantiate(impact_effect, transform.position, transform.rotation);

        Destroy(effect_instance, 1f);
        Destroy(Target.gameObject);
        Destroy(gameObject);
    }
}
