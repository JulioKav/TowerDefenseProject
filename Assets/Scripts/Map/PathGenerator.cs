using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public static PathGenerator Instance { get; private set; }
    void Awake() { if (!Instance) Instance = this; }

    public GameObject roadPrefab;
    public float pathCurviness = 4.5f;

    [HideInInspector] public List<Transform> activeSpawnPoints;
    [HideInInspector] public Transform startingSpawnPoint = null;
    Queue<Transform> inactiveSpawnPoints;
    public bool allSpawnPointsActive() { return inactiveSpawnPoints.Count == 0; }

    // Specifies how much the path stays on route
    // A lower factor means stronger variation
    float pathCoherenceFactor = 0.25f;

    BuildingPlacable[] GrassTiles;

    Queue<GameObject> TilesToDestroy;

    Vector3 direction;

    void GeneratePath(Vector3 _from, Vector3 _to)
    {
        Vector2 from = new Vector2(_from.x, _from.z);
        Vector2 to = new Vector2(_to.x, _to.z);

        Vector2 direction = to - from;
        int distance = (int)direction.magnitude + 1;
        Vector2 step = direction.normalized * direction.magnitude / distance;

        Vector2 perpendicular = new Vector2(step.y, -step.x).normalized;

        Vector2 currentPos = from;
        float previousOffset = 0f;
        Vector2 previousWaypoint = currentPos;
        for (int i = 0; i < distance - 1; i++)
        {
            currentPos += step;

            float offset = pathCoherenceFactor * previousOffset + (1f - pathCoherenceFactor) * UnityEngine.Random.Range(-pathCurviness, pathCurviness);
            Vector2 waypoint = currentPos + (offset * perpendicular);

            DestroyGrassAlongPath(previousWaypoint, waypoint, true);
            previousOffset = offset;
            previousWaypoint = waypoint;
        }

        DestroyGrassAlongPath(previousWaypoint, to, true);

        StartCoroutine(ReplaceWithRoad());
    }

    void DestroyGrassAlongPath(Vector2 from, Vector2 to, bool recursive)
    {
        Vector2 direction = to - from;
        int numSteps = 4 * ((int)direction.magnitude + 1);
        Vector2 step = direction / numSteps;

        float width = 0.35f;
        Vector2 perpendicular = (width / 2f) * new Vector2(step.y, -step.x).normalized;

        Vector2 currentPos = from;
        for (int i = 0; i < numSteps; i++)
        {
            Vector3 raycastSourcePos = new Vector3(currentPos.x, 10, currentPos.y);
            RaycastHit hit;
            if (Physics.Raycast(raycastSourcePos, -Vector3.up, out hit, 100f, LayerMask.GetMask("PlacableTerrain")))
            {
                Debug.DrawRay(raycastSourcePos, -Vector3.up);
                GameObject hitGO = hit.transform.gameObject;
                BuildingPlacable _;
                if (hitGO.TryGetComponent<BuildingPlacable>(out _)) TilesToDestroy.Enqueue(hitGO);
            }
            if (recursive) DestroyGrassAlongPath(currentPos - perpendicular, currentPos + perpendicular, false);
            currentPos += step;
        }
    }

    IEnumerator ReplaceWithRoad()
    {
        while (TilesToDestroy.Count > 0)
        {
            GameObject grass = TilesToDestroy.Dequeue();
            if (grass == null) continue; // Since same gameobject can enter the queue multiple time
            GameObject road = Instantiate(roadPrefab, grass.transform.position, Quaternion.identity);
            road.transform.position += 0.5f * Vector3.down;
            Destroy(grass);
            yield return new WaitForSeconds(0.025f);
        }

        MapManager.Instance.RefreshGameGrid();
        StartCoroutine(PathGenerationDone());
    }

    IEnumerator PathGenerationDone()
    {
        yield return new WaitForSeconds(1);
        GameStateManager.Instance.PathGenerationDone();
    }

    void Start()
    {
        TilesToDestroy = new Queue<GameObject>();
        GrassTiles = FindObjectsOfType<BuildingPlacable>();
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        // Fisher-Yates shuffle to randomize the list order
        for (int n = spawnPoints.Length - 1; n > 0; n--)
        {
            int k = UnityEngine.Random.Range(0, n);
            GameObject value = spawnPoints[k];
            spawnPoints[k] = spawnPoints[n];
            spawnPoints[n] = value;
        }
        inactiveSpawnPoints = new Queue<Transform>();
        foreach (GameObject sp in spawnPoints) inactiveSpawnPoints.Enqueue(sp.transform);

        activeSpawnPoints = new List<Transform>();
    }

    public void ActivateSpawnPoint()
    {
        if (inactiveSpawnPoints.Count == 0) return;
        Transform spawnPoint = inactiveSpawnPoints.Dequeue();
        if (startingSpawnPoint == null) startingSpawnPoint = spawnPoint;
        spawnPoint.GetComponent<MeshRenderer>().enabled = false;
        GeneratePath(spawnPoint.position, Vector3.zero);
        activeSpawnPoints.Add(spawnPoint);
    }
}
