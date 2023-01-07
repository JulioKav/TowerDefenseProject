using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundingGenerator : MonoBehaviour
{

    public GameObject surroundingCube;

    public GameObject[] trees;
    public GameObject[] grass;

    Queue<Vector2Int> mapBorder;

    // Start is called before the first frame update
    void Start()
    {
        mapBorder = new Queue<Vector2Int>();
        GenerateMapBorder();
        GenerateSurrounding();
    }

    void GenerateMapBorder()
    {
        for (int x = -22; x <= 22; x++)
            for (int y = -22; y <= 22; y++)
            {
                if ((x == -1 && y == -1) || (x == 1 && y == 1) || (x == 1 && y == -1) || (x == -1 && y == 1)) continue;
                // If at any given cell there is an object (the map), check the four surrounding
                // cells. If any of them don't have an object, it is one of the bordering cells
                // of the map. In that case spawn a tree.
                if (Physics.Raycast(new Vector3(x, 10, y), Vector3.down, 20)) continue;

                if (
                    !Physics.Raycast(new Vector3(x + 1, 10, y + 1), Vector3.down, 20) &&
                    !Physics.Raycast(new Vector3(x + 1, 10, y - 1), Vector3.down, 20) &&
                    !Physics.Raycast(new Vector3(x - 1, 10, y + 1), Vector3.down, 20) &&
                    !Physics.Raycast(new Vector3(x - 1, 10, y - 1), Vector3.down, 20)
                ) continue;

                mapBorder.Enqueue(new Vector2Int(x, y));
            }
        while (mapBorder.Count > 0)
        {
            Vector2Int pos = mapBorder.Dequeue();
            GameObject floor = Instantiate(surroundingCube, new Vector3(pos.x, 0, pos.y), Quaternion.identity, transform);
            floor.layer = 0;
            SpawnFoliage(pos.x, pos.y, true);
        }
    }

    void GenerateSurrounding()
    {
        for (int x = -22; x <= 22; x++)
            for (int y = -22; y <= 22; y++)
            {
                if ((x == -1 && y == -1) || (x == 1 && y == 1) || (x == 1 && y == -1) || (x == -1 && y == 1)) continue;
                if (!Physics.Raycast(new Vector3(x, 10, y), Vector3.down, 20))
                    Instantiate(surroundingCube, new Vector3(x, 0, y), Quaternion.identity, transform);
            }

        for (int x = -50; x <= 50; x++)
            for (int y = -50; y <= 50; y++)
            {
                if (Random.Range(0f, 1f) > 0.1f) continue;

                Vector3 raycastSource = new Vector3(x, 10, y);

                // Spawn only foliage if cell is a "surrounding" block
                RaycastHit hit;
                if (Physics.Raycast(raycastSource, Vector3.down, out hit, 20))
                    if (hit.transform.gameObject.layer == 7) SpawnFoliage(x, y);
            }
    }

    void SpawnFoliage(int x, int y, bool treesOnly = false)
    {
        int length = trees.Length + ((treesOnly) ? 0 : grass.Length);
        int id = Random.Range(0, length);
        float rotation = Random.Range(0, 359);

        GameObject foliage;
        if (treesOnly || id < trees.Length) foliage = trees[id];
        else foliage = grass[id - trees.Length];

        Instantiate(foliage, new Vector3(x, 0, y), Quaternion.Euler(0, rotation, 0), transform);
    }
}
