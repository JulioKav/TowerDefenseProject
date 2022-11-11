using UnityEngine;

public class MageSpawner : MonoBehaviour
{

    public SkillManager SkillTree;

    public GameObject ProtoMage;

    GameObject[] mages;

    void Start()
    {
        mages = new GameObject[4];
    }

    public GameObject SpawnMage(SkillManager.SkillClass skillClass)
    {
        // Called to spawn the mage of a specific skill class
        mages[(int)skillClass] = Instantiate(ProtoMage, new Vector3(0, 0.25f, 0), Quaternion.identity);
        return mages[(int)skillClass];
    }

    public void DespawnMage(SkillManager.SkillClass skillClass)
    {
        // Called to despawn the mage of a specific skill class
        Destroy(mages[(int)skillClass]);
        mages[(int)skillClass] = null;
    }
}
