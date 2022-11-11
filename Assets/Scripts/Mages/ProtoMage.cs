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

    public override void UnlockSkill(int id)
    {
        base.UnlockSkill(id);
        increaseAttackSpeed(attackSpeedBuffs[id]);
    }

    public override void LockSkill(int id)
    {
        base.LockSkill(id);
        decreaseAttackSpeed(attackSpeedBuffs[id]);
    }

    void increaseAttackSpeed(float value)
    {
        gameObject.GetComponent<Turret>().attack_speed += value;
    }

    void decreaseAttackSpeed(float value)
    {
        increaseAttackSpeed(-value);
    }

}
