using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    // Enemy Attributes
    public float speed = 4.0f;
    public bool slowed = false;
    protected float _maxHealth;
    protected float _health;
    protected float _attack;
    protected float _range;

    // Waypoint
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Transform Waypoints;
    private int wavepointIndex = 0;

    private SkillManager skillManager;


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
        skillManager = SkillManager.Instance;
    }

    protected void Update()
    {
        // Moves enemy to the direction of a waypoint
        if (target == null) return;
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // If enemy reaches a waypoint, move to next waypoint
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

        if (gameObject.tag == "AirborneEnemy")
        {
            StartCoroutine(LowerAfterTime(3, gameObject.transform, speed));
        }


    }
    void GetNextWaypoint()
    {
        // If enemies reach final base, subtract skill points and despawn enemy
        if (wavepointIndex >= Waypoints.childCount - 1)
        {
            skillManager.SubtractSkillPoints(20);
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.GetChild(wavepointIndex);
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
        Destroy(gameObject);
        skillManager.AddSkillPoints(20);
    }


    public void mechanical_lift(int time)
    {
        target = Waypoints.GetChild(0);
    }

    IEnumerator LowerAfterTime(float time, Transform Enemy, float saved_speed)
    {

        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        Lower(Enemy, saved_speed);
    }
    void Lower(Transform Enemy, float saved_speed)
    {
        // retrieves script aspect of enemy
        Enemies enemy_component = Enemy.GetComponent<Enemies>();
        if (enemy_component != null)
        {
            if (gameObject.transform.position.y == 2)
            {
                gameObject.tag = "Enemy";
                speed = 2;
                Tornado_Search();
                gameObject.transform.position = gameObject.transform.position - new Vector3(0, 2, 0);

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
    private float speed_storer(float speed)
    {
        float speed_clone = speed;
        return speed_clone;
    }


    
}



















