using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Boon : MonoBehaviour
{
    string spId;

    public Dictionary<string, TowerInfo> boon { get; private set; }
    public struct TowerInfo
    {
        public string name;
        public int value;
        public TowerInfo(string name)
        {
            this.name = name;
            this.value = 1;
        }
        public TowerInfo(string name, int value)
        {
            this.name = name;
            this.value = value;
        }
        public void Increment() { value++; }
    }

    void Start()
    {
        InitButton();
        spId = BoonManager.spId;
    }

    void InitButton()
    {
        GetComponent<Button>().onClick.AddListener
        (() =>
            {
                SelectBoon();
                UIStateManager.Instance.BoonSelected();
            }
        );
    }

    void SelectBoon()
    {
        // Add effects to boon
        foreach (string id in boon.Keys)
        {
            if (id == BoonManager.spId) // Skill points are treated differently than everything else
                SkillManager.Instance.AddSkillPoints(boon[id].value);
            else // get inventory button with variable name 'id'; e.g. cannon, healer; and add value
                BoonManager.Instance.GetInventoryButton(id).AddTowers(boon[id].value);
        }
    }

    public void GenerateBoon(int skillPointValue)
    {
        // Reset boon dictionary
        boon = new Dictionary<string, TowerInfo>();
        // Fill boon dictionary with bonuses
        while (true)
        {
            int tId = BoonManager.Instance.rnd.Next(BoonManager.Instance.boonsJson.towers.Length);
            var tower = BoonManager.Instance.boonsJson.towers[tId];
            if (skillPointValue >= tower.value)
            // Tower can still be added to boon
            {
                var id = tower.id;
                if (boon.ContainsKey(id))
                {
                    var ti = boon[id];
                    ti.value++;
                    boon[id] = ti;
                }
                else boon[id] = new TowerInfo(tower.name, 1);
                skillPointValue -= tower.value;
            }
            else break;
        }
        // Add remaining skill points to boon
        if (skillPointValue > 0) boon[BoonManager.spId] = new TowerInfo("Skill Points", skillPointValue);
        UpdateText();
    }

    void UpdateText()
    {
        TextMeshProUGUI tmp = GetComponentInChildren<TextMeshProUGUI>();
        string finalText = "";
        foreach (string id in boon.Keys)
        {
            if (id == spId) continue;
            finalText += "+" + boon[id].value + " " + boon[id].name;
            finalText += (boon[id].value > 1) ? "s" : "";
            finalText += "\n";
        }
        if (boon.ContainsKey(spId)) finalText += "\n+" + boon[spId].value + " " + boon[spId].name;
        tmp.text = finalText;
    }
}
