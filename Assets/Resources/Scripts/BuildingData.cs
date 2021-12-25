using System.Collections.Generic;

public class BuildingData
{
    //declaring fields
    private string _code;
    private int _healthpoints;

    //constructor mit code und lebenspunkten und die Felder in dem Konstruktor nutzbar machen
    public BuildingData(string code, int healthpoints)
    {
        _code = code;
        _healthpoints = healthpoints;
    }

    //declaring getters to access values
    public string Code { get => _code; }
    public int HP { get => _healthpoints; }

}
