using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImaginaryMage : MonoBehaviour
{
    // An interface for mage scripts

    bool[] skillsUnlocked;
    public StarDisplay starDisplay;

    // Start is called before the first frame update
    public void Start()
    {

        // by default they are not unlocked
        skillsUnlocked = new bool[] { false, false, false, false };
        // Calls Target_Search every chosen amount seconds.
        get_path_tiles();
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
    



    [Header("Turret Stats")]

    public float attack_speed = 1f;
    private float attack_countdown = 0f;
    

    [Header("Unity Required Stuff")]

    public string Road = "Road";
    public string Toxic_Road = "Toxic Road";

    //public GameObject start_effect;
    public int blocks_affected;


    public float search_radius = 0f;


    public GameObject toxic_floor;
    public GameObject regular_floor;

    public int damage = 20;


    public GameObject identity_of_shooter;

    public GameObject bulletprefab;
    public Transform firepoint;
    public Transform part_to_rotate;
    public float turn_speed = 5f;
    private Transform target;

    // Draws a 3D wire mesh range around turret.


    // Looks for closest target
    //void Target_Search()
    

    // Update is called once per frame
    void Update()
    {

        

        // shoot floor at start of round
        if (attack_countdown <= 0f)
        {
           shoot(target);
           attack_countdown = 1f / attack_speed;

        }

        attack_countdown -= Time.deltaTime;

        
    }





    //create a list of GameObject road_blocks with tag 'road', make new list randomly selecting a number 'blocks_affected' of them, then for each road block, shoot at them
    void get_path_tiles()
    {
        GameObject[] road_blocks = GameObject.FindGameObjectsWithTag(Road);

        GameObject[] toxic_road_blocks = GameObject.FindGameObjectsWithTag(Toxic_Road);


        float smallest_distance = Mathf.Infinity;
        int i;
        GameObject closest_road = null;
        List<GameObject> chosen_to_be_toxic_blocks = new List<GameObject>();

        for (i = 0; i < blocks_affected; i++)
        {
            int random_index_for_toxic = Random.Range(0, road_blocks.Length);
            chosen_to_be_toxic_blocks.Add(road_blocks[random_index_for_toxic]);
            
        }

        foreach (GameObject road in chosen_to_be_toxic_blocks)
        {

            float distance_to_target = Vector3.Distance(transform.position, road.transform.position);


            if (distance_to_target < smallest_distance)
            {
                smallest_distance = distance_to_target;

                closest_road = road;
            }
            shoot(road.transform);
            //make_floor_toxic(road.transform);
            


        }

        //foreach (GameObject toxic_road in toxic_road_blocks)
        //{
        //    make_floor_safe(toxic_road.transform);
            

        //}
    }

    public void make_floor_toxic(Transform floor)
    {


        Destroy(floor.gameObject);
        Instantiate(toxic_floor, floor.position, floor.rotation);
    }

    void make_floor_safe(Transform floor)
    {
        Destroy(floor.gameObject);
        Instantiate(regular_floor, floor.position, floor.rotation);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, search_radius);
    }

    //damage
    public void Imaginary_damage(Transform Enemy)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();

        if (enemy_component != null)
        {

            enemy_component.TakeDamage(damage * 3);

        }
    }

    
    // cloning bulletprefab at firepoint
    void shoot(Transform target)
    {

        GameObject bulletGO = (GameObject)Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
        ImaginaryBullet bullet = bulletGO.GetComponent<ImaginaryBullet>();


        if (bullet != null)
        {
            bullet.Chase(target);
        }


    }

}
