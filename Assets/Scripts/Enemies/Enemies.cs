using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    // Enemy Attributes
    public float speed = 4.0f;
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
        skillManager = GameObject.FindObjectsOfType<SkillManager>()[0];
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
    public void TakeDamage(int amountOfDamage)
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





















}

