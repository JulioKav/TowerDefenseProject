
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

        public MapInstr rotate(int times = 0)
        {
            //This function return a position which is rotational symmetric by the origin.
            //times is how many times of rotation, 90 degrees reverseclockwise per times. So 4 = 0 Easy to understand
            //reverseclockwise Rotation, (x,y) -> (-y,x)
            int x = this.position.x, y = this.position.y;
            for(int i = 0; i < times; i++)
            {
                int temp = x;
                x = -y;
                y = temp;
                switch (this.direction)
                {
                    case EAST: this.direction= NORTH; break;
                    case WEST: this.direction = SOUTH; ; break;
                    case NORTH: this.direction = WEST; ; break;
                    case SOUTH: this.direction = EAST; ; break;
                    default: break;
                }
            }
            this.position.x = x;
            this.position.y = y;
            return this;
        }
        public MapInstr slide(direction dir, int distance)
        {
            for(int i = 0; i < distance; i++)
            {
                switch (dir)
                {
                    case EAST: this.position.x += 1; break;
                    case WEST: this.position.x -= 1; break;
                    case NORTH: this.position.y += 1; break;
                    case SOUTH: this.position.y -= 1; break;
                    default: break;
                }
            }
            return this;
        }
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
       
        //W
        new MapInstr(LINE, ROAD, new Vector2Int(0, 0), WEST, 5, -1),
        new MapInstr(LINE, ROAD, new Vector2Int(-3, 1), WEST, 2, -1),
        new MapInstr(LINE, ROAD, new Vector2Int(-3, -1), WEST, 2, -1),

        new MapInstr(LINE,ROAD, new Vector2Int(-7,1),SOUTH,12,-1),
        new MapInstr(LINE,ROAD, new Vector2Int(-7,1),SOUTH,12,-1).slide(EAST,1),
        new MapInstr(LINE,ROAD, new Vector2Int(-7,1),SOUTH,12,-1).slide(EAST,2),

        new MapInstr(LINE,ROAD, new Vector2Int(-7,-9),WEST,2,-1),
        new MapInstr(LINE,ROAD, new Vector2Int(-9,-9),NORTH,14,-1),

        new MapInstr(LINE,CUBE, new Vector2Int(-1,1),WEST,2),
        new MapInstr(LINE,CUBE, new Vector2Int(-1,2),WEST,8),
        new MapInstr(LINE,CUBE, new Vector2Int(-2,3),WEST,7),
        new MapInstr(LINE,CUBE, new Vector2Int(-2,3),WEST,7).slide(NORTH,1),
        new MapInstr(LINE,CUBE, new Vector2Int(-8,1),SOUTH,10),
        new MapInstr(LINE,CUBE, new Vector2Int(-8,-10),WEST,3),
        new MapInstr(LINE,CUBE, new Vector2Int(-10,-9),NORTH,14),

        new MapInstr(SINGLE,SPAWNER, new Vector2Int(-6,-11)),
        new MapInstr(SINGLE,CUBE, new Vector2Int(-6,-11)).slide(EAST,1),
        new MapInstr(LINE,CUBE, new Vector2Int(-7,-11),WEST,4),
        new MapInstr(LINE,CUBE, new Vector2Int(-11,-11),NORTH,16),


        //S
        new MapInstr(LINE, ROAD, new Vector2Int(0, 0), WEST, 5, -1).rotate(1),
        new MapInstr(LINE, ROAD, new Vector2Int(-3, 1), WEST, 2, -1).rotate(1),
        new MapInstr(LINE, ROAD, new Vector2Int(-3, -1), WEST, 2, -1).rotate(1),

        new MapInstr(LINE,ROAD, new Vector2Int(-7,1),SOUTH,12,-1).rotate(1),
        new MapInstr(LINE,ROAD, new Vector2Int(-7,1),SOUTH,12,-1).slide(EAST,1).rotate(1),
        new MapInstr(LINE,ROAD, new Vector2Int(-7,1),SOUTH,12,-1).slide(EAST,2).rotate(1),

        new MapInstr(LINE,ROAD, new Vector2Int(-7,-9),WEST,2,-1).rotate(1),
        new MapInstr(LINE,ROAD, new Vector2Int(-9,-9),NORTH,14,-1).rotate(1),

        new MapInstr(LINE,CUBE, new Vector2Int(-1,1),WEST,2).rotate(1),
        new MapInstr(LINE,CUBE, new Vector2Int(-1,2),WEST,8).rotate(1),
        new MapInstr(LINE,CUBE, new Vector2Int(-2,3),WEST,7).rotate(1),
        new MapInstr(LINE,CUBE, new Vector2Int(-2,3),WEST,7).slide(NORTH,1).rotate(1),
        new MapInstr(LINE,CUBE, new Vector2Int(-8,1),SOUTH,10).rotate(1),
        new MapInstr(LINE,CUBE, new Vector2Int(-8,-10),WEST,3).rotate(1),
        new MapInstr(LINE,CUBE, new Vector2Int(-10,-9),NORTH,14).rotate(1),

        new MapInstr(SINGLE,SPAWNER, new Vector2Int(-6,-11)).rotate(1),
        new MapInstr(SINGLE,CUBE, new Vector2Int(-6,-11)).slide(EAST,1).rotate(1),
        new MapInstr(LINE,CUBE, new Vector2Int(-7,-11),WEST,4).rotate(1),
        new MapInstr(LINE,CUBE, new Vector2Int(-11,-11),NORTH,16).rotate(1),
        
        //E
        new MapInstr(LINE, ROAD, new Vector2Int(0, 0), WEST, 5, -1).rotate(2),
        new MapInstr(LINE, ROAD, new Vector2Int(-3, 1), WEST, 2, -1).rotate(2),
        new MapInstr(LINE, ROAD, new Vector2Int(-3, -1), WEST, 2, -1).rotate(2),

        new MapInstr(LINE,ROAD, new Vector2Int(-7,1),SOUTH,12,-1).rotate(2),
        new MapInstr(LINE,ROAD, new Vector2Int(-7,1),SOUTH,12,-1).slide(EAST,1).rotate(2),
        new MapInstr(LINE,ROAD, new Vector2Int(-7,1),SOUTH,12,-1).slide(EAST,2).rotate(2),

        new MapInstr(LINE,ROAD, new Vector2Int(-7,-9),WEST,2,-1).rotate(2),
        new MapInstr(LINE,ROAD, new Vector2Int(-9,-9),NORTH,14,-1).rotate(2),

        new MapInstr(LINE,CUBE, new Vector2Int(-1,1),WEST,2).rotate(2),
        new MapInstr(LINE,CUBE, new Vector2Int(-1,2),WEST,8).rotate(2),
        new MapInstr(LINE,CUBE, new Vector2Int(-2,3),WEST,7).rotate(2),
        new MapInstr(LINE,CUBE, new Vector2Int(-2,3),WEST,7).slide(NORTH,1).rotate(2),
        new MapInstr(LINE,CUBE, new Vector2Int(-8,1),SOUTH,10).rotate(2),
        new MapInstr(LINE,CUBE, new Vector2Int(-8,-10),WEST,3).rotate(2),
        new MapInstr(LINE,CUBE, new Vector2Int(-10,-9),NORTH,14).rotate(2),

        new MapInstr(SINGLE,SPAWNER, new Vector2Int(-6,-11)).rotate(2),
        new MapInstr(SINGLE,CUBE, new Vector2Int(-6,-11)).slide(EAST,1).rotate(2),
        new MapInstr(LINE,CUBE, new Vector2Int(-7,-11),WEST,4).rotate(2),
        new MapInstr(LINE,CUBE, new Vector2Int(-11,-11),NORTH,16).rotate(2),
        //N
        new MapInstr(LINE, ROAD, new Vector2Int(0, 0), WEST, 5, -1).rotate(3),
        new MapInstr(LINE, ROAD, new Vector2Int(-3, 1), WEST, 2, -1).rotate(3),
        new MapInstr(LINE, ROAD, new Vector2Int(-3, -1), WEST, 2, -1).rotate(3),

        new MapInstr(LINE,ROAD, new Vector2Int(-7,1),SOUTH,12,-1).rotate(3),
        new MapInstr(LINE,ROAD, new Vector2Int(-7,1),SOUTH,12,-1).slide(EAST,1).rotate(3),
        new MapInstr(LINE,ROAD, new Vector2Int(-7,1),SOUTH,12,-1).slide(EAST,2).rotate(3),

        new MapInstr(LINE,ROAD, new Vector2Int(-7,-9),WEST,2,-1).rotate(3),
        new MapInstr(LINE,ROAD, new Vector2Int(-9,-9),NORTH,14,-1).rotate(3),

        new MapInstr(LINE,CUBE, new Vector2Int(-1,1),WEST,2).rotate(3),
        new MapInstr(LINE,CUBE, new Vector2Int(-1,2),WEST,8).rotate(3),
        new MapInstr(LINE,CUBE, new Vector2Int(-2,3),WEST,7).rotate(3),
        new MapInstr(LINE,CUBE, new Vector2Int(-2,3),WEST,7).slide(NORTH,1).rotate(3),
        new MapInstr(LINE,CUBE, new Vector2Int(-8,1),SOUTH,10).rotate(3),
        new MapInstr(LINE,CUBE, new Vector2Int(-8,-10),WEST,3).rotate(3),
        new MapInstr(LINE,CUBE, new Vector2Int(-10,-9),NORTH,14).rotate(3),

        new MapInstr(SINGLE,SPAWNER, new Vector2Int(-6,-11)).rotate(3),
        new MapInstr(SINGLE,CUBE, new Vector2Int(-6,-11)).slide(EAST,1).rotate(3),
        new MapInstr(LINE,CUBE, new Vector2Int(-7,-11),WEST,4).rotate(3),
        new MapInstr(LINE,CUBE, new Vector2Int(-11,-11),NORTH,16).rotate(3),
        //new MapInstr(LINE, ROAD, new Vector2Int(-1, 0), WEST, 10, -1),

        //new MapInstr(SINGLE, CUBE, new Vector2Int(0, -1)),
        //new MapInstr(SINGLE, CUBE, new Vector2Int(0,  1)),

        //new MapInstr(LINE, CUBE, new Vector2Int(0, 2), WEST, 11),
        //new MapInstr(LINE, CUBE, new Vector2Int(0, -2), WEST, 11),
    };
}