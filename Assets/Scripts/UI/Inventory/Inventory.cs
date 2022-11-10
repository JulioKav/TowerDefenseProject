using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    GameObject selectedTower;
    InventoryButton selectedButton;
    Plane plane;
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
            // TODO: Make ray only collide with terrain
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("PlacableTerrain")))
            {
                ghostTowerMat.color = new Color(40 / 255f, 40 / 255f, 40 / 255f, 185 / 255f);

                if (EventSystem.current.IsPointerOverGameObject()) return;
                {
                    Vector3 pos = hit.transform.position;
                    pos.y += selectedButton.yOffset;
                    selectedTower.transform.position = pos;
                    if (Input.GetMouseButtonUp(0))
                    {
                        GameObject originalPrefab = selectedButton.towerModel.gameObject.GetComponent<TowerModelInfo>().originalPrefab;
                        hit.transform.GetComponent<BuildingPlacable>().PlaceTower(originalPrefab, selectedButton.yOffset);
                        selectedButton.numTowers--;
                        DeselectTower();
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

    public void InventoryButtonClick(InventoryButton button)
    {
        if (button == selectedButton) DeselectTower();
        else
        {
            if (selectedButton) DeselectTower();
            selectedButton = button;
            SelectTower();
        }
    }

    void SelectTower()
    {
        selectedTower = Instantiate(selectedButton.towerModel.gameObject, new Vector3(0, -100, 0), Quaternion.identity);
        selectedButton.inventoryFrame.SetActive(true);
    }

    void DeselectTower()
    {
        if (!selectedButton) return;
        selectedButton.inventoryFrame.SetActive(false);
        selectedButton = null;
        Destroy(selectedTower);
    }
}
