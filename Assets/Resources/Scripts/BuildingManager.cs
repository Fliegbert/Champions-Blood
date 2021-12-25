using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BuildingManager : MonoBehaviour
{
    private BoxCollider _collider;

    private Building _building = null;
    private int _nCollisions = 0;

    public void Initialize(Building building)
    {
        _collider = GetComponent<BoxCollider>();
        _building = building;
    }

    //Function purpose: If the collider triggers isTrigger on other object than terrain increase the collision count and execute CheckPlacement()
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrain") return;
        _nCollisions++;
        CheckPlacement();
    }

    //Function purpose: If the collider exits triggered objectc, collision count gets less and executes CheckPlacement()
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Terrain") return;
        _nCollisions--;
        CheckPlacement();
    }

    //Function purpose: If there is no building choosen or fixed the bool will be false. Otherwise it will execute HasValidPlacement() and sets the material of the building accordingly with the enum BuildingPlacement. it returns validPlacement.
    public bool CheckPlacement()
    {
        if (_building == null) return false;
        if (_building.IsFixed) return false;
        bool validPlacement = HasValidPlacement();
        if (!validPlacement)
        {
            _building.SetMaterials(BuildingPlacement.INVALID);
        }
        else
        {
            _building.SetMaterials(BuildingPlacement.VALID);
        }
        return validPlacement;
    }

    public bool HasValidPlacement()
    //Bool purpose: sets bool false if the count of collisions is higher than 0. Checks the distance from the corner to the ground and if more than one is not valid the placement will be invalid.
    {
        if (_nCollisions > 0) return false;

        // get 4 bottom corner positions
        Vector3 p = transform.position;
        Vector3 c = _collider.center;
        Vector3 e = _collider.size / 2f;
        float bottomHeight = c.y - e.y + 0.5f;
        Vector3[] bottomCorners = new Vector3[]
        {
            new Vector3(c.x - e.x, bottomHeight, c.z - e.z),
            new Vector3(c.x - e.x, bottomHeight, c.z + e.z),
            new Vector3(c.x + e.x, bottomHeight, c.z - e.z),
            new Vector3(c.x + e.x, bottomHeight, c.z + e.z)
        };
        // cast a small ray beneath the corner to check for a close ground
        // (if at least two are not valid, then placement is invalid)
        int invalidCornersCount = 0;
        foreach (Vector3 corner in bottomCorners)
        {
            if (!Physics.Raycast(
                p + corner,
                Vector3.up * -1f,
                2f,
                Globals.TERRAIN_LAYER_MASK
            ))
                invalidCornersCount++;
        }
        return invalidCornersCount < 3;
    }
}
