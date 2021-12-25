using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private BuildingPlacer _buildingPlacer;

    //Displays in the inspector
    public Transform buildingMenu;
    public GameObject buildingButtonPrefab;

    //Function purpose: Creating the Buttons by looping through the globals building types. If a button is pressed, it will executed the _AddBuildingButtonListener Callback function and uses i to get the right building in the loop
    private void Awake()
    {
        _buildingPlacer = GetComponent<BuildingPlacer>();

        // create buttons for each building type
        for (int i = 0; i < Globals.BUILDING_DATA.Length; i++)
        {
            GameObject button = Instantiate(buildingButtonPrefab);
            string code = Globals.BUILDING_DATA[i].Code;
            button.name = code;
            button.transform.Find("Text").GetComponent<Text>().text = code;
            Button b = button.GetComponent<Button>();
            _AddBuildingButtonListener(b, i);
            button.transform.SetParent(buildingMenu);
        }
    }

    //Function purpose: calls _buildingPlacer.SelectPlacedBuilding() if button is clicked
    //Callback function
    private void _AddBuildingButtonListener(Button b, int i)
    {
        b.onClick.AddListener(() => _buildingPlacer.SelectPlacedBuilding(i));
    }
}
