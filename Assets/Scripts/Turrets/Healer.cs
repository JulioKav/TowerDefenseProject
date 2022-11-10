using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{

    public int heal_amount = 50;
    private string Allies = "Tower";
    public float heal_range;
    public float heal_speed;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("heal", 0f, heal_speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Radius of healing (Visual aid only)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, heal_range);
    }


    void heal_tower(GameObject Tower)
    {
        // retrieves script aspect of Turret, to get heal damage property
        Turret turret_component = Tower.GetComponent<Turret>();

        if (turret_component != null)
        {
            turret_component.HealDamage(heal_amount);
        }
    }

    void heal()
    {
        GameObject[] allies = GameObject.FindGameObjectsWithTag(Allies);
        // goes through each in range ally and heals
        foreach (GameObject in_range_allies in allies)
        {
            float distance = Vector3.Distance(transform.position, in_range_allies.transform.position);

            if (in_range_allies != null && distance <= heal_range)
            {
                heal_tower(in_range_allies);
            }
        }
    }

    



}
