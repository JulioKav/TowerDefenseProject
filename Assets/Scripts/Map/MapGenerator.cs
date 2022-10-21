
using static MapInstructionEnums.mapInstructionType;
using static MapEnums.direction;
using static MapEnums.mapMaterial;
using MapEnums;
using MapInstructionEnums;
using UnityEngine;
using static Constants;

namespace MapInstructionEnums
{
    public enum mapInstructionType
    {
        LINE,
        SINGLE,
    }
}

public static class MapGenerator
{

    public struct MapInstr
    {
        public mapInstructionType type; // line or cube
        public mapMaterial material;
        public Vector2Int position;
        public direction direction;
        public int length;
        public int height;

        public MapInstr(mapInstructionType type, mapMaterial material, Vector2Int position, direction direction, int length, int height = 0)
        {
            this.type = type;
            this.material = material;
            this.position = position;
            this.direction = direction;
            this.length = length;
            this.height = height;
        }

        public MapInstr(mapInstructionType type, mapMaterial material, Vector2Int position, int height = 0)
        {
            this.type = type;
            this.material = material;
            this.position = position;
            this.height = height;
            // These need to be set but will be irrelevant
            direction = EAST;
            length = 0;
        }
    }

    public static MapInstr[] prototypeMap = {
        new MapInstr(LINE, ROAD, new Vector2Int(origin-1, origin-1), WEST, 10, -1),
        new MapInstr(LINE, ROAD, new Vector2Int(origin-1, origin+1), WEST, 10, -1),
        new MapInstr(LINE, ROAD, new Vector2Int(origin-1, origin), WEST, 10, -1),

        new MapInstr(SINGLE, CUBE, new Vector2Int(origin, origin-1)),
        new MapInstr(SINGLE, CUBE, new Vector2Int(origin, origin + 1)),

        new MapInstr(LINE, CUBE, new Vector2Int(origin, origin+2), WEST, 11),
        new MapInstr(LINE, CUBE, new Vector2Int(origin, origin-2), WEST, 11),
    };
}