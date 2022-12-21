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
    public float mech_explosion_radius = 5f;
    public float mech_damage = 50f;
    public GameObject impact_effect;
    public GameObject death_effect;

    // Waypoint
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Transform Waypoints;
    private int wavepointIndex = 0;

    private SkillManager skillManager;

    private string Tornado = "Tornado";

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

        
    }

    protected void Update()
    {
        
        if (gameObject.tag == "AirborneEnemy")
        {
            StartCoroutine(LowerAfterTime(3, gameObject.transform, speed));
        }

        // Moves enemy to the direction of a waypoint
        if (target == null) return;
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        // If enemy reaches a waypoint, move to next waypoint
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
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
        Destroy(gameObject);
        skillManager.AddSkillPoints(20);
    }


    IEnumerator ImpactDMGAfterTime(float time)
    {
        Debug.Log("ff");
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        Explode();
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
            if (gameObject.transform.position.y >= 2 )
            {
                
                gameObject.tag = "Enemy";
                speed = 2;
                Tornado_Search();
                gameObject.transform.position = gameObject.transform.position - new Vector3(0, 2, 0);
                
                
                if (gameObject.GetComponent<Tags>().HasTag("Mechanical Enemy"))
                {
                    //GameObject effect_instance = (GameObject)Instantiate(impact_effect, transform.position, transform.rotation);
                    //Destroy(effect_instance, 5f);
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
                    enemy_component.TakeDamage(mech_damage*3);
                }

                else
                {
                    enemy_component.TakeDamage(mech_damage);
                }
            }




        }

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("success1");
        if (other.gameObject.GetComponent<Tags>().HasTag("Wall") == true)
        {
            Debug.Log("success");
            speed = 0f;
        }
    }
    

}




















