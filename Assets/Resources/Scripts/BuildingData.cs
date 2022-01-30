using System.Collections.Generic;
using UnityEngine;

//allows unity to add an associated menu for this scriptable objects to create quicker assets from this class
[CreateAssetMenu(fileName = "Building", menuName = "Scriptable Objects/Building", order = 1)]
public class BuildingData : ScriptableObject
{
    //fields for the scriptable object assets
    public string code;
    public string unitName;
    public int healthpoints;
    public GameObject prefab;
    public List<ResourceValue> cost;

    //checks if can buy
    public bool CanBuy()
    {
        foreach (ResourceValue resource in cost)
            if (Globals.GAME_RESOURCES[resource.code].Amount < resource.amount)
                return false;
        return true;
    }
}
