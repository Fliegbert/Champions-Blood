using System.Collections.Generic;

public class BuildingData
{
    //declaring fields
    private string _code;
    private int _healthpoints;
    private Dictionary<string, int> _cost;

    //constructor mit code und lebenspunkten und die Felder in dem Konstruktor nutzbar machen and adding Dictionary for resources
    public BuildingData(string code, int healthpoints, Dictionary<string, int> cost)
    {
        _code = code;
        _healthpoints = healthpoints;
        _cost = cost;
    }

    // Bool purpose: determines whether building can be bought or not
    public bool CanBuy()
    {
        foreach (KeyValuePair<string, int> pair in _cost)
        {
            if (Globals.GAME_RESOURCES[pair.Key].Amount < pair.Value)
            {
                return false;
            }
        }
        return true;
    }

    //declaring getters to access values
    public string Code { get => _code; }
    public int HP { get => _healthpoints; }
    public Dictionary<string, int> Cost { get => _cost; }
}
