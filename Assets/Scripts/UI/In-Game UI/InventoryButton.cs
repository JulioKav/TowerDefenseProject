using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{

    public TowerModelInfo towerModel;

    public float yOffset;

    public GameObject inventoryFrame;
    TextMeshProUGUI inventoryText;

    private int _numTowers;
    public int NumTowers
    {
        get
        {
            return _numTowers;
        }
        set
        {
            _numTowers = value;
            UpdateText();
            CheckInteractability();
        }
    }

    void Start()
    {
        // Subscribes this buttons onClick to the parent (Inventory sript)'s InventoryButtonClick function with this button as the argument
        GetComponent<Button>().onClick.AddListener(() => transform.parent.GetComponent<Inventory>().InventoryButtonClick(this));
        inventoryText = gameObject.GetComponentsInChildren<TextMeshProUGUI>()[0];
        NumTowers = 1;
    }

    void OnEnable()
    {
        GameStateManager.OnStateChange += StateChangeHandler;
    }
    void OnDisable()
    {
        GameStateManager.OnStateChange -= StateChangeHandler;
    }

    void StateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.POST_ROUND:
                AddTowers(1);
                break;
            case GameState.PRE_ROUND:
            case GameState.IDLE:
                CheckInteractability();
                break;
            default:
                break;
        }
    }

    void AddTowers(int num)
    {
        NumTowers += num;
    }

    void UpdateText()
    {
        if (inventoryText) inventoryText.text = "" + NumTowers;
    }

    void CheckInteractability()
    {
        GetComponent<Button>().interactable = GameStateManager.Instance.State == GameState.IDLE && NumTowers > 0;
    }

}
