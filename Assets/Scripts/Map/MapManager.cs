
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
    public static MapManager Instance { get; private set; }
    void Awake() { if (!Instance) Instance = this; }

    public Transform MapContainer;
    public GameObject[] materialPrefab;
    public bool isPrototype = false;

    // Start is called before the first frame update
    public int[,,] gameGrid = new int[MAX_MAPSIZE, MAX_MAPSIZE, MAX_LAYERSIZE]; //Storage of map information

    public void setGameGrid(int x, int y, layer l, mapMaterial material)
    {
        gameGrid[x + (MAX_MAPSIZE - 1) / 2, y + (MAX_MAPSIZE - 1) / 2, (int)l] = (int)material;
        return;
    }
    public int getGameGrid(int x, int y, layer l)
    {
        return gameGrid[x + (MAX_MAPSIZE - 1) / 2, y + (MAX_MAPSIZE - 1) / 2, (int)l];
    }
    public Vector2Int getLocOnGrid(Vector3 pos)
    {
        Vector2Int v = new Vector2Int(0, 0);
        v.x = -Mathf.RoundToInt(pos.x);
        v.y = -Mathf.RoundToInt(pos.z);
        return v;
    }
    public bool isPassable(int x, int y)
    {
        if (getGameGrid(x, y, 0) == (int)mapMaterial.ROAD || getGameGrid(x, y, 0) == (int)mapMaterial.BASE)
            return true;
        return false;
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

    public void RefreshGameGrid()
    {
        gameGrid = new int[MAX_MAPSIZE, MAX_MAPSIZE, MAX_LAYERSIZE];
        InitGameGrid();
    }

    void InitGameGrid()
    {
        GameObject[] grass = GameObject.FindGameObjectsWithTag("Grass");
        GameObject[] roads = GameObject.FindGameObjectsWithTag("Road");
        GameObject fortress = GameObject.FindGameObjectsWithTag("Base")[0];
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        Vector2Int pos = getLocOnGrid(fortress.transform.position);
        setGameGrid(pos.x, pos.y, layer.TERRAIN, mapMaterial.BASE);

        foreach (GameObject g in grass)
        {
            pos = getLocOnGrid(g.transform.position);
            setGameGrid(pos.x, pos.y, layer.TERRAIN, mapMaterial.CUBE);
        }

        foreach (GameObject r in roads)
        {
            pos = getLocOnGrid(r.transform.position);
            setGameGrid(pos.x, pos.y, layer.TERRAIN, mapMaterial.ROAD);
        }

        foreach (GameObject sp in spawnPoints)
        {
            pos = getLocOnGrid(sp.transform.position);
            setGameGrid(pos.x, pos.y, layer.TERRAIN, mapMaterial.SPAWNER);
        }
    }
}
