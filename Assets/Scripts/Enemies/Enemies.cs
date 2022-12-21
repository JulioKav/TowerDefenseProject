using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    //Retrieve gamegrid from Map Manager
    private MapManager mapManager;
    private Pathfinding pathFinding;
    // Enemy Attributes
    public float speed = 4.0f;
    protected float _maxHealth;
    protected float _health;
    protected float _attack;
    protected float _range;

    //Enemy State
    public bool isBackward = false;
    private Vector3 spawner;

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
        spawner = transform.position;
        skillManager = SkillManager.Instance;
        mapManager = FindObjectOfType<MapManager>();
        pathFinding = FindObjectOfType<Pathfinding>();
        
    }

    protected void Update()
    {
        
        if (target == null) return;
        float xdiff = transform.position.x - Mathf.RoundToInt(transform.position.x), zdiff = transform.position.z - Mathf.RoundToInt(transform.position.z);
        Vector3 direction;
        if (isBackward != true)
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
            direction.x -= 2.5f*xdiff;
        }
        else if(direction.z==0.0f)
        {
            direction.z -= 2.5f*zdiff;
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

