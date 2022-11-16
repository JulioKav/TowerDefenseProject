using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImaginaryBullet : MonoBehaviour
{

    private Transform target;
    private float speed = 15;

    //Bullet assigns target (in this case 'floor')
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

        // if the bullet slows, it calls that it has hit target, so destroy floor and replace with imaginary tower
        if (direction.magnitude <= distance_per_frame)
        {
            hit_target();


            make_floor_toxic(target);

            return;
        }
        //bullet follows target
        transform.Translate(direction.normalized * distance_per_frame, Space.World);





    }


    // destroy bullet on collision
    void hit_target()
    {

        Destroy(gameObject);


    }


    //clone toxic floor to replace floor
    void make_floor_toxic(Transform floor)
    {
        Destroy(floor.gameObject);
        var toxic_floor = ImaginaryMage.Instance.GetToxicRoadPrefab();
        Instantiate(toxic_floor, floor.position, floor.rotation);
    }



}
