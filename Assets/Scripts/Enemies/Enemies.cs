using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    //Retrieve gamegrid from Map Manager
    private MapManager mapManager;
    private Pathfinding pathFinding;
    // Enemy Attributes
    public float speed;
    public float max_speed = 4.0f;
    public bool slowed = false;
    protected float _maxHealth;
    protected float _health;
    protected float _attack;
    protected float _range;
    public GameObject impact_effect;
    public GameObject death_effect;

    public static float mech_airborne_time = 2;
    public static float mech_explosion_radius = 5f;
    public static float mech_damage = 50f;

    public static float magic_airborne_time = 5;

    Animator m_Animator;
    //Enemy State
    public bool isBackward = false;
    private Vector3 spawner;

    // Waypoint
    [HideInInspector]
    public Transform target;

    private SkillManager skillManager;
    private Achievements achievements;

    private string Tornado = "Tornado";
    Vector3 direction;

    [Header("Unity Stuff")]
    public Image healthBar;

    enum subtype
    {
        Physical,
        Mechanic,
        Magic,
    };

    public void Start()
    {

        skillManager = GameObject.FindObjectsOfType<SkillManager>()[0];
        achievements = GameObject.FindObjectsOfType<Achievements>()[0];
        spawner = transform.position;
        mapManager = FindObjectOfType<MapManager>();
        pathFinding = FindObjectOfType<Pathfinding>();
        m_Animator = gameObject.GetComponent<Animator>();


    }

    protected void Update()
    {

        if (gameObject.tag == "AirborneEnemyMechanical")
            StartCoroutine(LowerAfterTime(mech_airborne_time, gameObject.transform));

        if (gameObject.tag == "AirborneEnemyMagic")
            StartCoroutine(LowerAfterTime(magic_airborne_time, gameObject.transform));


        float xdiff = transform.position.x - Mathf.RoundToInt(transform.position.x), zdiff = transform.position.z - Mathf.RoundToInt(transform.position.z);
        if (gameObject.tag == "Enemy" && slowed == false)
        {
            speed = max_speed;
        }

        Turret t;
        bool isAttacking = TryGetComponent<Turret>(out t) && t.isAttacking;

        if (!isAttacking)
        {
            try
            {

                if (!isBackward)
                {
                    direction = pathFinding.findDirection(mapManager.getLocOnGrid(transform.position), mapManager, new Vector2Int(0, 0));
                }
                else
                {
                    direction = pathFinding.findDirection(mapManager.getLocOnGrid(transform.position), mapManager, mapManager.getLocOnGrid(spawner));
                }

                // Softly snap the enemy into grid
                if (direction.x == 0.0f)
                {
                    direction.x -= 2.5f * xdiff;
                }
                else if (direction.z == 0.0f)
                {
                    direction.z -= 2.5f * zdiff;
                }
                //target.position - transform.position;
                //Debug.Log(direction);

                transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);



                if (mapManager.getLocOnGrid(transform.position) == new Vector2Int(0, 0))
                {
                    skillManager.SubtractSkillPoints(20);
                    Destroy(gameObject);
                    return;
                }
            }
            catch
            {
                Debug.Log("pathfinding error");
            }
        }

        if (transform.position.y < -10)
        {
            int spawnId = UnityEngine.Random.Range(0, PathGenerator.Instance.activeSpawnPoints.Count);
            transform.position = PathGenerator.Instance.activeSpawnPoints[spawnId].position;
        }

        // If enemy reaches a waypoint, move to next waypoint
        /*
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
            //Debug.Log(mapManager.getGameGrid(1,1,MapEnums.layer.TERRAIN));
            //Debug.Log("x:"+transform.localPosition.x+",y:"+transform.localPosition.y+",z:"+transform.localPosition.z);
            Debug.Log(mapManager.getLocOnGrid(transform.position));
        }*/

        //Give information of yourself, and the map you're in to pathfind
        //It will return a direction to go.



    }



    //Enemy death/damage taken
    public void TakeDamage(float amountOfDamage)
    {
        // Healthbar value
        _health -= amountOfDamage;
        healthBar.fillAmount = _health / _maxHealth;

        if (_health <= 0)
        {//could place death animation here
            Die();
        }
    }

    void Die()
    {
        if (gameObject.GetComponent<Tags>().HasTag("Boss") == true)
        {
            GameObject effect_instance = (GameObject)Instantiate(death_effect, transform.position, transform.rotation);
            Destroy(effect_instance, 5f);
        }
        else
        {
            GameObject effect_instance = (GameObject)Instantiate(death_effect, transform.position, transform.rotation);
            Destroy(effect_instance, 1f);
        }
        if (gameObject.GetComponent<Tags>().HasTag("Mechanical Enemy")) achievements.mechkills += 1;
        if (gameObject.GetComponent<Tags>().HasTag("Magic Enemy")) achievements.magickills += 1;
        if (gameObject.GetComponent<Tags>().HasTag("Physical Enemy")) achievements.physkills += 1;
        if (gameObject.GetComponent<Tags>().HasTag("Imaginary Enemy")) achievements.imaginarykills += 1;
        Destroy(gameObject);
        skillManager.AddSkillPoints(Difficulty.skillpoints);
        achievements.kills += 1;
    }


    IEnumerator ImpactDMGAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        Explode();
    }

    IEnumerator LowerAfterTime(float time, Transform Enemy)
    {

        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        Lower(Enemy);
    }
    void Lower(Transform Enemy)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();
        if (enemy_component != null)
        {
            if (Enemy.transform.position.y >= 2)
            {
                Enemy.transform.position = Enemy.transform.position - new Vector3(0, 2, 0);

                Enemy.tag = "Enemy";

                if (Enemy.GetComponent<Tags>().HasTag("Magic Enemy"))
                {
                    enemy_component.isBackward = false;


                }

                Tornado_Search();

                GameObject effect_instance_1 = (GameObject)Instantiate(impact_effect, transform.position, transform.rotation);
                Destroy(effect_instance_1, 5f);

                if (Enemy.GetComponent<Tags>().HasTag("Mechanical Enemy"))
                {
                    GameObject effect_instance = (GameObject)Instantiate(impact_effect, transform.position, transform.rotation);
                    Destroy(effect_instance, 5f);
                    StartCoroutine(ImpactDMGAfterTime(0.2f));

                }

            }




        }
    }


    void Tornado_Search()
    {
        //GameObject[] tornados = GameObject.FindGameObjectsWithTag(Tornado);

        //foreach (GameObject tornado in tornados)
        //{
        //    Destroy(tornado);
        //}
        Collider[] collided_objects = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider collider in collided_objects)
        {
            Enemies enemy_component = collider.GetComponent<Enemies>();
            if (collider.tag == "Tornado")
            {

                Destroy(collider.gameObject);

            }




        }


    }


    void Explode()
    {

        Collider[] collided_objects = Physics.OverlapSphere(transform.position, mech_explosion_radius);
        foreach (Collider collider in collided_objects)
        {
            Enemies enemy_component = collider.GetComponent<Enemies>();
            if (collider.tag == "Enemy")
            {
                if (collider.GetComponent<Tags>().HasTag("Mechanical Enemy"))
                {
                    enemy_component.TakeDamage(mech_damage * 3);
                }

                else
                {
                    enemy_component.TakeDamage(mech_damage);
                }
            }




        }

    }




}




















