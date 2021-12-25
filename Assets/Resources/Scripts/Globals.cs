 public class Globals
{
    //declaring the Terrain
    // statics are shared across all instances of a class
    public static int TERRAIN_LAYER_MASK = 1 << 3;
    //instantiate an array of new BuildingData Objects
    public static BuildingData[] BUILDING_DATA = new BuildingData[]
    {
        new BuildingData("Hut", 100)
    };

}
