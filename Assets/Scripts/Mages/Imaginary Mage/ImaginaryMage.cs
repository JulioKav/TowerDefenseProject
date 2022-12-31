using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImaginaryMage : Mage
{
    static string SLOWING = "slowing", RANGE = "range", MORE_TOWERS = "more_towers";

    static float[] SLOWING_MULTIPLIERS = new float[] { 1.4f, 1.8f, 2.2f, 2.5f };
    static float[] SLOWING_RANGES = new float[] { 1f, 1.5f, 2f, 3f };
    static int[] NUM_TOWERS = new int[] { 2, 4, 6, 8 };

    // Make Mage a Singleton for easier access, since only one of each mage can exist
    public static ImaginaryMage Instance { get; private set; }
    private void Awake() { if (!Instance) Instance = this; }

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
    public GameObject toxicRoadLvl1Prefab, toxicRoadLvl2Prefab, toxicRoadLvl3Prefab;
    public Transform firepoint;
    public Transform part_to_rotate;
    public float turn_speed = 5f;
    private Transform target;

    // Draws a 3D wire mesh range around turret.


    // Looks for closest target

    public new void Start()
    {
        base.Start();
        FillDictionary(new string[] { SLOWING, RANGE, MORE_TOWERS });
        mageClass = MageClass.Imaginary;
    }


    // Update is called once per frame
    void Update()
    {

        blocks_affected = NUM_TOWERS[GetSkillLevel(MORE_TOWERS)];

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




        }

    }


    void make_floor_safe(Transform floor)
    {
        Destroy(floor.gameObject);
        Instantiate(regular_floor, floor.position, floor.rotation);
    }


    // cloning bulletprefab at firepoint
    IEnumerator shoot(Transform target, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject bulletGO = (GameObject)Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
        ImaginaryBullet bullet = bulletGO.GetComponent<ImaginaryBullet>();
        bullet.slowing_amount = SLOWING_MULTIPLIERS[GetSkillLevel(SLOWING)];
        bullet.slowing_range = SLOWING_RANGES[GetSkillLevel(RANGE)];


        if (bullet != null)
        {
            bullet.Chase(target);
        }
    }

    new public void OnEnable()
    {
        base.OnEnable();
        GameStateManager.OnStateChange += StateChangeHandler;
    }

    new public void OnDisable()
    {
        base.OnDisable();
        GameStateManager.OnStateChange -= StateChangeHandler;
    }

    void StateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.PRE_ROUND:
                get_path_tiles();
                break;
            case GameState.POST_ROUND:
                DespawnAllObjects();
                break;
            default:
                break;
        }
    }

    void DespawnAllObjects()
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

    public GameObject GetToxicRoadPrefab()
    {
        int skillLevel = GetSkillLevel(SLOWING);
        if (skillLevel == 3) return toxicRoadLvl3Prefab;
        if (skillLevel == 2) return toxicRoadLvl2Prefab;
        return toxicRoadLvl1Prefab;
    }

}
