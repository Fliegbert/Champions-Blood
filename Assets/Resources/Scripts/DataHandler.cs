using UnityEngine;

public static class DataHandler
{
    // Function purpose: loading the scriptable objects in the buildings
    public static void LoadGameData()
    {
        Globals.BUILDING_DATA = Resources.LoadAll<BuildingData>("Scriptable Objects/Units/Beastmasters/Buildings") as BuildingData[];
    }
}
