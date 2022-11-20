
using System.Collections;
using System.Collections.Generic;
using MapEnums;
using MapInstructionEnums;
using UnityEngine;
using static Constants;
using static MapEnums.direction;
using static MapEnums.mapMaterial;
using static MapInstructionEnums.mapInstructionType;

static class Constants
{
    //public const double Pi = 3.14159;
    public const int MAX_MAPSIZE = 129; //Make sure this is an odd numbers, so making sure it has a center.
    public const int origin = 0;//(MAX_MAPSIZE - 1) / 2;
    public const int MAX_LAYERSIZE = 3;// Can be edited, different game object on the same grid will be stored in different layer.


}

namespace MapEnums
{
    public enum mapMaterial
    {
        BASE,
        SPAWNER,
        ROAD,
        CUBE,
    };
    public enum direction
    {
        EAST,
        WEST,
        NORTH,
        SOUTH,
    }
    public enum layer
    {
        TERRAIN,
        CONSTRUCTION,
    }
}
public class MapManager : MonoBehaviour
{
    public Transform MapContainer;
    public GameObject[] materialPrefab;
    public bool isPrototype = false;

    // Start is called before the first frame update
    private int[,,] gameGrid = new int[MAX_MAPSIZE, MAX_MAPSIZE, MAX_LAYERSIZE]; //Storage of map information

    private void setGameGrid(int x, int y, layer l, mapMaterial material)
    {
        gameGrid[x + (MAX_MAPSIZE - 1) / 2, y + (MAX_MAPSIZE - 1) / 2, (int)l] = (int)material;
        return;
    }


    private void spawnMapMaterial(mapMaterial material, int x, int y, int height = 0) //Spawn specific material in the location x,y refer to the grid system
    {
        //Will add facing direction in the future, if mapMaterial is differed 

        Vector3 pos; pos.x = -x; pos.z = -y; pos.y = (0.5f * height);
        Vector3 angle; angle.x = 0.0f; angle.y = 0.0f; angle.z = 0.0f;

        setGameGrid(x, y, layer.TERRAIN, material); //Storing the info in grid

        Instantiate(materialPrefab[(int)material], pos, Quaternion.Euler(new Vector3(0, 0, 0)), MapContainer);
    }
    private void spawnLine(mapMaterial material, Vector2Int start, direction dir, int length, int height = 0) //Use specific material to draw a line from start location x,y to a direction , need to be a straight line
    {
        int[] increment = { 0, 0 };
        switch (dir)
        {
            case direction.EAST: increment[0]++; break;
            case direction.WEST: increment[0]--; break;
            case direction.NORTH: increment[1]++; break;
            case direction.SOUTH: increment[1]--; break;
            default: break;
        }
        for (int i = 0; i < length; i++)
        {
            spawnMapMaterial(material, start.x + increment[0] * i, start.y + increment[1] * i, height);
        }
    }
    private void initPrototype()
    {
        spawnLine(mapMaterial.ROAD, new Vector2Int(origin - 1, origin), direction.WEST, 10, -1);
        spawnLine(mapMaterial.ROAD, new Vector2Int(origin - 1, origin - 1), direction.WEST, 10, -1);
        spawnLine(mapMaterial.ROAD, new Vector2Int(origin - 1, origin + 1), direction.WEST, 10, -1);

        spawnMapMaterial(mapMaterial.CUBE, origin, origin + -1);
        spawnMapMaterial(mapMaterial.CUBE, origin, origin + 1);

        spawnLine(mapMaterial.CUBE, new Vector2Int(origin, origin + 2), direction.WEST, 11);
        spawnLine(mapMaterial.CUBE, new Vector2Int(origin, origin - 2), direction.WEST, 11);

    }

    private void drawMap(MapGenerator.MapInstr[] map)
    {
        foreach (var m in map)
        {
            switch (m.type)
            {
                case (LINE):
                    spawnLine(m.material, m.position, m.direction, m.length, m.height);
                    break;
                case (SINGLE):
                    spawnMapMaterial(m.material, m.position.x, m.position.y, m.height);
                    break;
                default:
                    break;
            }
        }

    }

    void Awake()
    {
        setGameGrid(0, 0, layer.TERRAIN, BASE);

        spawnMapMaterial(mapMaterial.BASE, origin, origin);
        if (isPrototype == true)
        {
            drawMap(MapGenerator.prototypeMap);
            // initPrototype();
        }
        //Debug.Log(gameGrid[origin, origin]);
    }
}
