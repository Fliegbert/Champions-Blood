using System.Collections.Generic;

public class Globals
{
    //declaring the Terrain
    // statics are shared across all instances of a class
    public static int TERRAIN_LAYER_MASK = 1 << 3;

    // Selected Units list
    public static List<UnitManager> SELECTED_UNITS = new List<UnitManager>();
    //instantiate an array of new BuildingData Objects
    public static BuildingData[] BUILDING_DATA = new BuildingData[]
    {
        new BuildingData("Hut", 100, new Dictionary<string, int>()
        {
          {"gold", 100}
        })
    };
    //Function purpose: instantiates Resources and set initial amount. the first string ist the key, Gameresource is the value
    public static Dictionary<string, GameResource> GAME_RESOURCES = new Dictionary<string, GameResource>()
    {   //Dictionary{string, GameResource(string name, int initialAmount)}
        { "gold", new GameResource("Gold", 300) },
        { "wood", new GameResource("Wood", 300) },
        { "stone", new GameResource("Stone", 300) }
    };
}
