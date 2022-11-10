using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButtonManager : MonoBehaviour
{

    public GameObject tower;
    GameObject selectedTower;

    public float yOffset;

    public GameObject inventoryFrame;
    public GameObject inventoryText;

    public Material ghostTowerMat;
    Plane plane;

    // Start is called before the first frame update
    void Start()
    {
        selectedTower = null;
        plane = new Plane(Vector3.up, Vector3.zero);
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
                ghostTowerMat.color = new Color(40 / 255f, 40 / 255f, 40 / 255f, 185 / 255f);

                if (EventSystem.current.IsPointerOverGameObject()) return;
                {
                    Vector3 pos = hit.transform.position;
                    pos.y += yOffset;
                    selectedTower.transform.position = pos;
                    if (Input.GetMouseButtonUp(0))
                    {
                        DeselectTower();
                        GameObject originalPrefab = tower.GetComponent<TowerModelInfo>().originalPrefab;
                        hit.transform.GetComponent<BuildingPlacable>().PlaceTower(originalPrefab, yOffset);
                    }
                }
            }
            else
            {
                ghostTowerMat.color = new Color(185 / 255f, 40 / 255f, 40 / 255f, 185 / 255f);
                // From https://answers.unity.com/questions/750801/get-world-position-of-mouse-click-with-z-equals-to.html
                float distance;
                plane.Raycast(ray, out distance);
                selectedTower.transform.position = ray.GetPoint(distance);
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
        selectedTower = Instantiate(tower, new Vector3(0, -100, 0), Quaternion.identity);
        inventoryFrame.SetActive(true);
    }
}
