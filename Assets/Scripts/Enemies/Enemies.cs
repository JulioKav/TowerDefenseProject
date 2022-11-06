using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    public float speed = 4.0f;
    protected float _maxHealth;
    protected float _health;
    protected float _attack;
    protected float _range;
    protected Transform _target;
    
    [Header("Unity Stuff")] 
    public Image healthBar;

    enum subtype
    {
        Physical,
        Mechanic,
        Magic,
    };
    
    private int wavepointIndex = 0;
    
    protected void Start()
    {
        _target = Waypoints.points[0];
    }

    protected void Update()
    {
        Vector3 direction = _target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    


    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        _target = Waypoints.points[wavepointIndex];
    }


    //Enemy death/damage taken

    public void TakeDamage(int amountOfDamage)
    {
        _health -= amountOfDamage;

        healthBar.fillAmount = _health / _maxHealth;

        if (_health <= 0)
        {//could place death animation here
            Die();
        }
    }

    void Die()
    {
        
        SkillManager skillpoints_on_death = gameObject.GetComponent<SkillManager>();

        skillpoints_on_death.AddSkillPoints(50);
        Destroy(gameObject);
    }
























}

