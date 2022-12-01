using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Maker : MonoBehaviour
{
    public GameObject wall_prefab;
    public string Other_Wall = "Other Wall";



    private float search_range = 100;
    void Target_Search()
    {

        Collider[] collided_objects = Physics.OverlapSphere(transform.position, search_range);
        foreach (Collider collider in collided_objects)
        {
            if (collider.tag == "Other Wall")
            {

                make_wall(collider.transform);
                //make_wall(gameObject.transform);

            }
        }
    }

    void make_wall(Transform wall)
    {
        Vector3 distance_between = (wall.position - gameObject.transform.position)/2 + gameObject.transform.position;
        Quaternion look_at_target = Quaternion.LookRotation(wall.position - gameObject.transform.position);
        //look_at_target = look_at_target


        Destroy(wall.gameObject);
        Destroy(gameObject);
        Instantiate(wall_prefab, distance_between, look_at_target);
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
        Target_Search();
    }

    void WaveEndHandler()
    {
        foreach (var tile in GameObject.FindGameObjectsWithTag(Other_Wall))
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
        //make_floor_safe(tile.transform);
    }
}
