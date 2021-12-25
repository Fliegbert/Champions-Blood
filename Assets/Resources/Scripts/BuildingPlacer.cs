using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    //instantiate empty building object
    private Building _placedBuilding = null;

    //declaring fields for raycast
    private Ray _ray;
    private RaycastHit _raycastHit;
    private Vector3 _lastPlacementPosition;

    //start the phantom building through _PreparePlacedBuilding
    void Start()
    {
        //first pick first building type with 0
        _PreparePlacedBuilding(0);
    }

    //taking in information whether building was choosen or not and is getting executed if building is choosen
    //call method to destroy building if esc is pressed and if building is choosen
    //return is used to get back to the main function with the value

    //Function purpose: Following the Phantom Building and and checking the placements validity. If the placement is valid you can place the building by clicking the mouse which executes the _PlaceBuilding function
    void Update()
    {
        if (_placedBuilding != null)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                _CancelPlacedBuilding();
                return;
            }

            //declaring a ray from the camera
            //if the the raycast hits the terrain set the position of the _placedBuilding to raycast hits
            //Physics.Raycast(origin of raycast, direction, max distant, layermask to ignore)
            //use SetPosition to set a position for the building
            //should the _lastPlacementPosition not be the same as the raycastpoint it should be check the placementvalidity of the _placedBuilding
            //also _lastPlacementPosition must be set same as raycasthit.points
            //if the buildingplace has a valid place and button is clicked place building
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _raycastHit, 1000f, Globals.TERRAIN_LAYER_MASK))
            {
                _placedBuilding.SetPosition(_raycastHit.point);
                if (_lastPlacementPosition != _raycastHit.point)
                {
                    _placedBuilding.CheckValidPlacement();
                }
                _lastPlacementPosition = _raycastHit.point;

                if (_placedBuilding.HasValidPlacement && Input.GetMouseButtonDown(0))
                {
                    _PlaceBuilding();
                }
            }
        }
    }

    //Function purpose: Destroys existing choosen building and equips new one

    //checks if building is picked and destroys it
    //takes generates new building with argument int from Globals Array and sets the placementposition at the last of scene
    void _PreparePlacedBuilding(int buildingDataIndex)
    {
        Building building = new Building(
            Globals.BUILDING_DATA[buildingDataIndex]
        );
        // link the data into the manager
        building.Transform.GetComponent<BuildingManager>().Initialize(building);
        _placedBuilding = building;
        _lastPlacementPosition = Vector3.zero;
    }

    //Function purpose: Place the building with the help of the Place Function and Set up the same building type as next choosen building

    //place the building and keep the same building type
    void _PlaceBuilding()
    {
        _placedBuilding.Place();
        _PreparePlacedBuilding(_placedBuilding.DataIndex);
    }

    //Function purpose: Delete Phantom Building and set the choosen building to none

    //Destroy phantom building
    void _CancelPlacedBuilding()
    {
        Destroy(_placedBuilding.Transform.gameObject);
        _placedBuilding = null;
    }
}
