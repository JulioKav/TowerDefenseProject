using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Difficulty : MonoBehaviour
{
    public static GameObject instance;
    
    public static float bullet_dmg = 20;

   
    static double[] modifier2 = new double[] { 0.6f, 0.8f, 1.0f, 1.2f, 1.4f, 1.6f, 2.0f };
    //dmg
    static float[] bullet_dmg_array = new float[] { bullet_dmg * 6, bullet_dmg * 8, bullet_dmg * 10, bullet_dmg * 12, bullet_dmg * 14, bullet_dmg * 16, bullet_dmg * 20 };
    //health
    public static double[] type_healths = new double[] { 200f, 250f, 300f, 300f, 250f, 200f};
    public static float[] modified_type_healths = new float[] { 200f, 250f, 300f, 300f, 250f, 200f };
    //speed
    public static double[] type_speed = new double[] { 1f, 2f, 3f, 4f, 4f, 4f };
    public static float[] modified_type_speed = new float[] { 200f, 250f, 300f, 300f, 250f, 200f };
    //skillpoints
    public static int skillpoints = 25;
    public static int[] modified_skillpoints = new int[] { (int)(skillpoints * 1.4), (int)(skillpoints * 1.2), (int)(skillpoints * 1.0), (int)(skillpoints * 0.9), (int)(skillpoints * 0.8), (int)(skillpoints * 0.7), (int)(skillpoints * 0.6) };
    //attack range
    public static float range = 2;
    public static float[] modified_range = new float[] { (float)(range * 0.6), (float)(range * 0.8), (float)(range * 1.0), (float)(range * 1.05), (float)(range * 1.1), (float)(range * 1.15), (float)(range * 1.25) };


    // Start is called before the first frame update
    void Awake()
    {
        

        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);

            return;
        }

        DontDestroyOnLoad(gameObject);

        

        
        
        //bullet_dmg = bullet_dmg_array[PlayerPrefs.GetInt("difficultyIndex")];
        
        //type healths



    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "LevelSelect")
        {

            //dmg
            bullet_dmg = bullet_dmg_array[PlayerPrefs.GetInt("difficultyIndex")];
            //skillpoints
            skillpoints = modified_skillpoints[PlayerPrefs.GetInt("difficultyIndex")];
            //attack range
            range = modified_range[PlayerPrefs.GetInt("difficultyIndex")];

            //health
            for (int i = 0; i != modified_type_healths.Length; i++)
            {
                modified_type_healths[i] = (float)(type_healths[i] * modifier2[PlayerPrefs.GetInt("difficultyIndex")]);
            }

            //speed
            for (int i = 0; i != modified_type_speed.Length; i++)
            {
                modified_type_speed[i] = (float)(type_speed[i] * modifier2[PlayerPrefs.GetInt("difficultyIndex")]);
            }
            print(modified_type_speed[0]);
        }
    }


    }
