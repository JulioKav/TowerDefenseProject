using UnityEditor;
using UnityEngine;

public class InventoryButtonManager : MonoBehaviour
{

    public GameObject tower;
    GameObject selectedTower;

    public GameObject inventoryFrame;
    public GameObject inventoryText;

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
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("PlacableTerrain")))
            {
                {
                    Vector3 pos = hit.transform.position;
                    pos.y += 0.25f;
                    selectedTower.transform.position = pos;
                    if (Input.GetMouseButtonUp(0))
                    {
                        DeselectTower();
                        GameObject originalPrefab = tower.GetComponent<TowerModelInfo>().originalPrefab;
                        hit.transform.GetComponent<BuildingPlacable>().PlaceTower(originalPrefab);
                    }
                }
            }
        }
    }

    void OnDisable()
    {
        DeselectTower();
    }

    public void HoverTower()
    {
        if (selectedTower) DeselectTower();
        else SelectTower();
    }

    public void DeselectTower()
    {
        Destroy(selectedTower);
        inventoryFrame.SetActive(false);
    }

    private void SelectTower()
    {
        selectedTower = Instantiate(tower, new Vector3(0, 0, 0), Quaternion.identity);
        inventoryFrame.SetActive(true);
    }
}
