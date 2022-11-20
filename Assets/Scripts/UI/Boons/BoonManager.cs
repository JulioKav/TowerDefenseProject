using UnityEngine;

public class BoonManager : MonoBehaviour
{
    public static BoonManager Instance;
    void Awake() { if (!Instance) Instance = this; }

    public const string spId = "skillpoints";

    public BoonsJSONParser.Boons boonsJson;
    int skillPointValue;

    // These names have to match "id" values in Assets/Texts/Boons/Boons.json
    public InventoryButton cannon, healer, archer;

    public System.Random rnd;

    void Start()
    {
        boonsJson = BoonsJSONParser.Instance.boonsJson;
        skillPointValue = boonsJson.startingValue;
        rnd = new System.Random();
    }

    void OnEnable()
    {
        UIStateManager.OnStateChange += StateChangeHandler;
    }

    void OnDisable()
    {
        UIStateManager.OnStateChange -= StateChangeHandler;
    }

    void StateChangeHandler(UIState newState)
    {
        switch (newState)
        {
            case UIState.BOONS:
                GenerateBoons();
                break;
            default:
                break;
        }
    }

    void GenerateBoons()
    {
        foreach (Boon b in GetComponentsInChildren<Boon>()) b.GenerateBoon(skillPointValue);
        skillPointValue += boonsJson.valueIncrease;
    }

    public InventoryButton GetInventoryButton(string id)
    {
        return ((InventoryButton)this.GetType().GetField(id).GetValue(this));
    }
}