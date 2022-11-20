using UnityEngine;

public class ProtoMage : Mage
{
    // The golden tower prototype mage

    float[] attackSpeedBuffs;

    new public void Start()
    {
        base.Start();
        attackSpeedBuffs = new float[] { 0.5f, 0.5f, 1f, 2f };
    }

    void increaseAttackSpeed(float value)
    {
        gameObject.GetComponent<Turret>().attack_speed += value;
    }

    void decreaseAttackSpeed(float value)
    {
        increaseAttackSpeed(-value);
    }

    public override void ChangeSkillHandler(MageClass mageClass, string id, bool unlocked)
    {

    }
}
