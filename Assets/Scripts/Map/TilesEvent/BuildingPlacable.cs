using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPlacable : MonoBehaviour
{
    public GameObject placedObject;

    void Start()
    {
        placedObject = null;
    }

    public void PlaceTower(GameObject turret, float yOffset)
    {
        Vector3 pos = transform.position;
        pos.y += yOffset;

        placedObject = Instantiate(turret, pos, transform.rotation);
        // gameObject.layer = 0;
    }

    void OnEnable()
    {
        GameStateManager.OnStateChange += StateChangeHandler;
    }

    void OnDisable()
    {
        GameStateManager.OnStateChange -= StateChangeHandler;
    }

    private void StateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.IDLE:
                // if (placedObject == null) gameObject.layer = 6;
                break;
            default: break;
        }
    }

    void OnDestroy()
    {
        if (placedObject == null) return;
        Inventory.Instance.ReturnToInventory(placedObject.name);
        Destroy(placedObject);
    }

    public bool CanPlaceTurret()
    {
        return placedObject == null;
    }
}
