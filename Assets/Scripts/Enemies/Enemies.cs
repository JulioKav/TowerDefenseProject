using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    public float speed = 4.0f;
    protected float _maxHealth;
    protected float _health;
    protected float _attack;
    protected float _range;


    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Transform Waypoints;
    private SkillManager skillManager;


    [Header("Unity Stuff")]
    public Image healthBar;

    enum subtype
    {
        Physical,
        Mechanic,
        Magic,
    };

    private int wavepointIndex = 0;

    public void Start()
    {
        skillManager = GameObject.FindObjectsOfType<SkillManager>()[0];
    }

    protected void Update()
    {
        if (target == null) return;
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }




    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.childCount - 1)
        {
            skillManager.SubtractSkillPoints(25);
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.GetChild(wavepointIndex);
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
        Destroy(gameObject);
        skillManager.AddSkillPoints(50);
    }
























}

