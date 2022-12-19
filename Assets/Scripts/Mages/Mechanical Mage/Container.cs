using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    // Start is called before the first frame update
    public float time_tornado_is_up = 3.0f;
    private float attack_range = 2f;



    void Start()
    {
        // Calls Target_Search every chosen amount seconds.
        

    }

    void Update()
    {
        Enemy_Alive_Checker();


    }

    // get airborne enemies with tag, check they're in range, if they are add to list, if list not empty tornado continues, if empty destroyed
    List<GameObject> enemies_in_range = new List<GameObject>();
    void Enemy_Alive_Checker()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("AirborneEnemy");
        foreach (GameObject enemy in enemies)
        {
            float distance_to_target = Vector3.Distance(transform.position, enemy.transform.position);


            if (distance_to_target < attack_range)
            {
                enemies_in_range.Add(enemy);
            }


        }

        if (enemies_in_range.Count == 0)
        {
            //death animation here
            Destroy(gameObject);

        }
        enemies_in_range.Clear();
    }

}
