using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    void Awake() { if (!Instance) Instance = this; }

    // Currently selected tower and button
    GameObject selectedTower;
    InventoryButton selectedButton;
    // Plane for intersection tests
    Plane plane;
    // Ghost material for hovered towers to be transparent
    public Material ghostTowerMat;

    // Start is called before the first frame update
    void Start()
    {
        selectedTower = null;
        selectedButton = null;
        plane = new Plane(Vector3.up, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedTower)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Ray can only collide with terrain
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("PlacableTerrain")) &&
                hit.transform.GetComponent<BuildingPlacable>().CanPlaceTurret())
            {
                ghostTowerMat.color = new Color(40 / 255f, 40 / 255f, 40 / 255f, 185 / 255f);

                if (EventSystem.current.IsPointerOverGameObject()) return;
                {
                    // If a placable terrain is hit, update the tower to be on its position
                    Vector3 pos = hit.transform.position;
                    pos.y += selectedButton.yOffset;
                    selectedTower.transform.position = pos;
                    if (Input.GetMouseButtonUp(0))
                    {
                        // When a selected tower is placed, spawn it at the right spot and deselect it after.
                        GameObject originalPrefab = selectedButton.towerModel.gameObject.GetComponent<TowerModelInfo>().originalPrefab;
                        hit.transform.GetComponent<BuildingPlacable>().PlaceTower(originalPrefab, selectedButton.yOffset);
                        selectedButton.NumTowers--;
                        DeselectTower();
                    }
                }
            }
            // otherwise it will get a position from the flat plane variable, and map to that
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

    // This is called by buttons to tell the inventory script theyve been clicked
    public void InventoryButtonClick(InventoryButton button)
    {
        // If same button is clicked thats previously selected, deselect it
        if (button == selectedButton) DeselectTower();
        else
        // different button is clicked: select it (and deselect previous)
        {
            if (selectedButton) DeselectTower();
            selectedButton = button;
            SelectTower();
        }
    }

    void SelectTower()
    {
        // Make instance of tower, and highlight the button
        selectedTower = Instantiate(selectedButton.towerModel.gameObject, new Vector3(0, -100, 0), Quaternion.identity);
        selectedButton.inventoryFrame.SetActive(true);
    }

    void DeselectTower()
    {
        // Delete instance of tower, and unhighlight the previously selected button
        if (!selectedButton) return;
        selectedButton.inventoryFrame.SetActive(false);
        selectedButton = null;
        Destroy(selectedTower);
    }

    public void ReturnToInventory(string name)
    {
        foreach (Transform child in transform)
        {
            if (name.StartsWith(child.name))
            {
                child.GetComponent<InventoryButton>().AddTowers(1);
                return;
            }
        }
    }
}
