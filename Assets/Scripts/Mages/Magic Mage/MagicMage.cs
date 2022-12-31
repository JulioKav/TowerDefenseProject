using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMage : Mage
{
    static string RAISE_TIME = "raise_time", ATTACK_SPEED = "attack_speed", DOT_DMG = "dot_damage";

    static float[] RAISE_TIMES = new float[] { 5f, 3.5f, 2f, 1f };
    static float[] ATTACK_SPEEDS = new float[] { 0.5f, 1.25f, 2f, 2.5f };
    static float[] DOT_DMGS = new float[] { 2f, 3f, 4f, 5f };

    // Start is called before the first frame update
    public new void Start()
    {
        base.Start();
        // by default they are not unlocked
        // Calls Target_Search every chosen amount seconds.
        InvokeRepeating("Target_Search", 0f, 1.0f);
        FillDictionary(new string[] { RAISE_TIME, ATTACK_SPEED, DOT_DMG });
        mageClass = MageClass.Magic;
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

        attack_speed = ATTACK_SPEEDS[GetSkillLevel(ATTACK_SPEED)];


    }


    // turret shooting out a bullet, instantiating the bulletprefab, bullet chases target.
    void shoot()
    {   //make new function to swap out shoot after upgrade

        Enemies.magic_airborne_time = RAISE_TIMES[GetSkillLevel(RAISE_TIME)];

        GameObject effect_instance = (GameObject)Instantiate(start_effect, transform.position, transform.rotation);

        Destroy(effect_instance, 1f);

        GameObject bulletGO = (GameObject)Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
        MagicBullet bullet = bulletGO.GetComponent<MagicBullet>();
        bullet.damage_over_time = DOT_DMGS[GetSkillLevel(DOT_DMG)];
        if (bullet != null)
        {
            bullet.Chase(target);
        }


    }
}

