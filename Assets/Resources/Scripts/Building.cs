using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingPlacement
{
    VALID,
    FIXED,
    INVALID
}

public class Building
{
    private BuildingData _data;
    private BuildingManager _buildingManager;
    private Transform _transform;
    private int _currentHealth;
    private BuildingPlacement _placement;
    private List<Material> _materials;

    //Function purpose: Constructor filling the objects properties via call by reference

    //Creating new Building Instance with a Building data reference to give it the metadata
    //Gebäude mit der der klassenconstructor callen als Argument und die Methodenfelder füllen
    //Gebäude laden mit gameobjects instantiating methode und resource load funktion als Argument.
    //Transform nicht vergessen.
    public Building(BuildingData data)
    {
        _data = data;
        _currentHealth = data.HP;

        GameObject g = GameObject.Instantiate(Resources.Load($"Prefabs/Units/Beastmasters/Buildings/{_data.Code}")) as GameObject;
        _transform = g.transform;
        _placement = BuildingPlacement.VALID;

        //list gets filled with the materials when constructor is called
        _materials = new List<Material>();
        foreach (Material material in _transform.Find("Mesh").GetComponent<Renderer>().materials)
        {
            _materials.Add(new Material(material));
        }

        // for the validity state of the object

        _buildingManager = g.GetComponent<BuildingManager>();
        _placement = BuildingPlacement.VALID;
        SetMaterials();
    }

    public void SetMaterials() { SetMaterials(_placement); }

    //Function purpose: Change the Material
    public void SetMaterials(BuildingPlacement placement)
    {
        List<Material> materials;
        if (placement == BuildingPlacement.VALID)
        {
            Material refMaterial = Resources.Load("Materials/Valid") as Material;
            materials = new List<Material>();
            for (int i = 0; i < _materials.Count; i++)
            {
                materials.Add(refMaterial);
            }
        }
        else if (placement == BuildingPlacement.INVALID)
        {
            Material refMaterial = Resources.Load("Materials/Invalid") as Material;
            materials = new List<Material>();
            for (int i = 0; i < _materials.Count; i++)
            {
                materials.Add(refMaterial);
            }
        }
        else if (placement == BuildingPlacement.FIXED)
        {
            materials = _materials;
        }
        else
        {
            return;
        }
        _transform.Find("Mesh").GetComponent<Renderer>().materials = materials.ToArray();
    }

    //Function purpose: Set the position of the object in the worldspace

    //Setting the Position of the Gameobject in the scene with a methode
    //same as Transform.position
    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }

    //Function purpose: Fixes the Object by signaling it via BuildingPlacement Enum and fixing the Object via Boxcollider
    //set placement state and remove "is trigger" flag from box collider to allow collisions
    public void Place()
    {
        _placement = BuildingPlacement.FIXED;
        // change building materials
        SetMaterials();
        _transform.GetComponent<BoxCollider>().isTrigger = false;
    }

    public void CheckValidPlacement()
    {
        if (_placement == BuildingPlacement.FIXED) return;
        _placement = _buildingManager.CheckPlacement()
            ? BuildingPlacement.VALID
            : BuildingPlacement.INVALID;
    }

    //Setting the Getters
    public bool IsFixed { get => _placement == BuildingPlacement.FIXED; }
    public string Code { get => _data.Code; }
    public Transform Transform { get => _transform; }
    public int HP { get => _currentHealth; set => _currentHealth = value; }
    public int MaxHP { get => _data.HP; }
    public bool HasValidPlacement { get => _placement == BuildingPlacement.VALID; }

    //“computed” property that gives us the index of the abstract building type data instance in the global list
    //DataIndex ist ein getter
    //Ein Loop als For wird benutzt, indem der Array BUILDING_DATA verwendet wird um alle Gebaüde Instanzen zu erhalten aus dem Globals
    //Wenn der Code == data.code ist wird i wieder ausgegeben
    public int DataIndex
    {
      get {
          for (int i = 0; i < Globals.BUILDING_DATA.Length; i++)
          {
              if (Globals.BUILDING_DATA[i].Code == _data.Code)
              {
                  return i;
              }
          }
          return -1;
      }
    }
}
