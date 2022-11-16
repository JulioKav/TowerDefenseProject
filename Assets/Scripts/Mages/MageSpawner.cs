using UnityEngine;

public class MageSpawner : MonoBehaviour
{

    public SkillManager SkillTree;

    public GameObject[] magesPrefabs;
    GameObject[] mages;
    Vector3[] magePositions = new Vector3[] {
        new Vector3(1, 0.25f, 1),
        new Vector3(1, 0.25f, -1),
        new Vector3(-1, 0.25f, 1),
        new Vector3(-1, 0.25f, -1)
};

    void Start()
    {
        mages = new GameObject[4];
    }

    public GameObject SpawnMage(SkillManager.SkillClass skillClass)
    {
        int sId = (int)skillClass;
        // Called to spawn the mage of a specific skill class
        mages[sId] = Instantiate(magesPrefabs[sId], magePositions[sId], Quaternion.identity);
        mages[sId].transform.SetParent(transform, false);
        return mages[sId];
    }

    public void DespawnMage(SkillManager.SkillClass skillClass)
    {
        // Called to despawn the mage of a specific skill class
        Destroy(mages[(int)skillClass]);
        mages[(int)skillClass] = null;
    }
}
