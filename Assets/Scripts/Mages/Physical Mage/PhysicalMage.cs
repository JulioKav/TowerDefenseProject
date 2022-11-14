using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalMage : MonoBehaviour { 
// An interface for mage scripts

    bool[] skillsUnlocked;
    public StarDisplay starDisplay;

// Start is called before the first frame update
public void Start()
{   

    // by default they are not unlocked
    skillsUnlocked = new bool[] { false, false, false, false };
        // Calls Target_Search every chosen amount seconds.
    InvokeRepeating("Target_Search", 0f, 1.0f);
    }

public virtual void UnlockSkill(int id)
{
    // unlocks the skill and shows the star
    skillsUnlocked[id] = true;
    starDisplay.UnlockSkill(id);
}

public virtual void LockSkill(int id)
{
    // locks unlocked skill and hides scar
    skillsUnlocked[id] = false;
    starDisplay.LockSkill(id);
}

public virtual void Skill1()
{
    if (!skillsUnlocked[0]) return;
}

public virtual void Skill2()
{
    if (!skillsUnlocked[1]) return;
}

public virtual void Skill3()
{
    if (!skillsUnlocked[2]) return;
}

public virtual void Skill4()
{
    if (!skillsUnlocked[3]) return;
}

    ///////////////////////////////////////////////////////////////
    /// <shooting>
    /// ///////////////////////////////
    /// </summary>
    ////////////////////////////////////////////////////
    private Transform target;
    private Enemy targetEnemy;

    

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
    public GameObject start_effect;

    // USE THIS WHEN YOU HAVE ASSETS, NAMELY A PART OF TURRET YOU WANT TO ROTATE NOT WHOLE THING
    public Transform part_to_rotate;



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

        //Array search of Targets for closest distance to target, updating closest enemy with shortest distance
        foreach (GameObject enemy in enemies)
        {
            float distance_to_target = Vector3.Distance(transform.position, enemy.transform.position);


            if (distance_to_target < smallest_distance)
            {
                smallest_distance = distance_to_target;

                closest_enemy = enemy;
            }
        }

        // If the enemy is in attack range, it becomes target.
        if (closest_enemy != null && smallest_distance <= attack_range)
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
    {   //make new function to swap out shoot after upgrade

        GameObject effect_instance = (GameObject)Instantiate(start_effect, transform.position, transform.rotation);

        Destroy(effect_instance, 1f);

        GameObject bulletGO = (GameObject)Instantiate(bulletprefab, firepoint.position + new Vector3( 0, 16f, 0), firepoint.rotation);
        GameObject bulletGO1 = (GameObject)Instantiate(bulletprefab, firepoint.position + new Vector3(0, 8f, 0), firepoint.rotation);
        GameObject bulletGO2 = (GameObject)Instantiate(bulletprefab, firepoint.position + new Vector3(0, 10f, 0), firepoint.rotation);
        GameObject bulletGO3 = (GameObject)Instantiate(bulletprefab, firepoint.position + new Vector3(0, 12f, 0), firepoint.rotation);
        GameObject bulletGO4 = (GameObject)Instantiate(bulletprefab, firepoint.position + new Vector3(0, 14f, 0), firepoint.rotation);
        PhysicalBullet bullet = bulletGO.GetComponent<PhysicalBullet>();
        PhysicalBullet bullet1 = bulletGO1.GetComponent<PhysicalBullet>();
        PhysicalBullet bullet2 = bulletGO2.GetComponent<PhysicalBullet>();
        PhysicalBullet bullet3 = bulletGO3.GetComponent<PhysicalBullet>();
        PhysicalBullet bullet4 = bulletGO4.GetComponent<PhysicalBullet>();
        if (bullet != null && bullet1 != null && bullet2 != null && bullet3 != null)
        {
            bullet.Chase(target);
            bullet1.Chase(target);
            bullet2.Chase(target);
            bullet3.Chase(target);
            bullet4.Chase(target);
        }


    }

    
}
