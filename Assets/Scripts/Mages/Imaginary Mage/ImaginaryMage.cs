using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImaginaryMage : Mage
{

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

    new void Start()
    {
        base.Start();
    }


    // Update is called once per frame
    void Update()
    {



        // shoot floor at start of round
        // if (attack_countdown <= 0f)
        // {
        //     shoot(target);
        //     attack_countdown = 1f / attack_speed;

        // }

        // attack_countdown -= Time.deltaTime;


    }





    //create a list of GameObject road_blocks with tag 'road', make new list randomly selecting a number 'blocks_affected' of them, then for each road block, shoot at them
    void get_path_tiles()
    {
        GameObject[] road_blocks = GameObject.FindGameObjectsWithTag(Road);


        int i;
        List<GameObject> chosen_to_be_toxic_blocks = new List<GameObject>();

        for (i = 0; i < blocks_affected; i++)
        {
            int random_index_for_toxic = Random.Range(0, road_blocks.Length);
            chosen_to_be_toxic_blocks.Add(road_blocks[random_index_for_toxic]);

        }

        foreach (GameObject road in chosen_to_be_toxic_blocks)
        {

            float distance_to_target = Vector3.Distance(transform.position, road.transform.position);

            var rnd = new System.Random();
            float delay = (float)rnd.NextDouble() * (WaveSpawner.WaveCountdownTime - 1);
            StartCoroutine(shoot(road.transform, delay));
            //make_floor_toxic(road.transform);



        }

        //foreach (GameObject toxic_road in toxic_road_blocks)
        //{
        //    make_floor_safe(toxic_road.transform);


        //}
    }

    // public void make_floor_toxic(Transform floor)
    // {
    //     Destroy(floor.gameObject);
    //     Instantiate(toxic_floor, floor.position, floor.rotation);
    // }

    void make_floor_safe(Transform floor)
    {
        Destroy(floor.gameObject);
        Instantiate(regular_floor, floor.position, floor.rotation);
    }
    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireSphere(transform.position, search_radius);
    // }

    // //damage
    // public void Imaginary_damage(Transform Enemy)
    // {
    //     // retrieves script aspect of enemy
    //     Enemies enemy_component = Enemy.GetComponent<Enemies>();

    //     if (enemy_component != null)
    //     {

    //         enemy_component.TakeDamage(damage * 3);

    //     }
    // }


    // cloning bulletprefab at firepoint
    IEnumerator shoot(Transform target, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject bulletGO = (GameObject)Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
        ImaginaryBullet bullet = bulletGO.GetComponent<ImaginaryBullet>();


        if (bullet != null)
        {
            bullet.Chase(target);
        }
    }

    void OnEnable()
    {
        StartButton.OnWaveStart += WaveStartHandler;
        WaveSpawner.OnRoundEnd += WaveEndHandler;
    }

    void OnDisable()
    {
        StartButton.OnWaveStart -= WaveStartHandler;
        WaveSpawner.OnRoundEnd -= WaveEndHandler;
    }

    void WaveStartHandler()
    {
        get_path_tiles();
    }

    void WaveEndHandler()
    {
        foreach (var tile in GameObject.FindGameObjectsWithTag(Toxic_Road))
        {
            var rnd = new System.Random();
            float delay = (float)rnd.NextDouble();
            StartCoroutine(DespawnTower(tile, delay));
        }
    }

    private IEnumerator DespawnTower(GameObject tile, float delay)
    {
        tile.GetComponentInChildren<ImagineTower>().enabled = false;
        yield return new WaitForSeconds(delay);
        tile.GetComponent<Animator>().SetTrigger("EndOfRound");
        yield return new WaitForSeconds(3);
        make_floor_safe(tile.transform);
    }

}
