using UnityEngine;

public class InventoryButtonManager : MonoBehaviour
{

    public GameObject tower;
    GameObject selectedTower;

    // Start is called before the first frame update
    void Start()
    {
        selectedTower = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedTower)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // TODO: Make ray only collide with terrain
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                selectedTower.transform.position = hit.point;
                Debug.Log(hit.point);
            }
        }
    }

    public void SpawnTower()
    {
        selectedTower = Instantiate(tower, new Vector3(0, 0, 0), Quaternion.identity);
        // TODO: remove turret script properly - maybe second prefab without scripts
        Destroy(selectedTower.GetComponent<Turret>());
    }
}
