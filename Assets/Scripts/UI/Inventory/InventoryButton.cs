using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{

    public TowerModelInfo towerModel;

    public float yOffset;

    public GameObject inventoryFrame;
    TextMeshProUGUI inventoryText;

    [HideInInspector]
    public int numTowers;

    void Start()
    {
        numTowers = 1;
        // Subscribes this buttons onClick to the parent (Inventory sript)'s InventoryButtonClick function with this button as the argument
        gameObject.GetComponent<Button>().onClick.AddListener(() => transform.parent.GetComponent<Inventory>().InventoryButtonClick(this));
        inventoryText = gameObject.GetComponentsInChildren<TextMeshProUGUI>()[0];
    }

    void Update()
    {
        gameObject.GetComponent<Button>().interactable = numTowers > 0;
        if (inventoryText) inventoryText.text = "" + numTowers;
    }

    // Subscribe to RoundEndEvent
    void OnEnable() { WaveSpawner.OnRoundEnd += RoundEndHandler; }
    void OnDisable() { WaveSpawner.OnRoundEnd -= RoundEndHandler; }

    void RoundEndHandler()
    {
        // Add 1 tower each round end
        AddTowers(1);
    }

    void AddTowers(int num)
    {
        numTowers += num;
    }

}
