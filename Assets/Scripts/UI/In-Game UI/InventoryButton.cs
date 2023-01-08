using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{

    public TowerModelInfo towerModel;

    public float yOffset;

    public GameObject inventoryFrame;
    TextMeshProUGUI inventoryText;

    public int numStartingTowers = 0;

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
        NumTowers = numStartingTowers;
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
            case GameState.PRE_ROUND:
            case GameState.IDLE:
                CheckInteractability(newState);
                break;
            default:
                break;
        }
    }

    public void AddTowers(int num)
    {
        NumTowers += num;
    }

    void UpdateText()
    {
        if (inventoryText) inventoryText.text = "" + NumTowers;
    }

    void CheckInteractability(GameState state = GameState.NONE)
    {
        if (state == GameState.NONE) state = GameStateManager.Instance.State;
        GetComponent<Button>().interactable = state == GameState.IDLE && NumTowers > 0;
    }

}
