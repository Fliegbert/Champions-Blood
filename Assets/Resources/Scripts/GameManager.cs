using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Loads datahandler as soon the game is started
    private void Awake()
    {
        DataHandler.LoadGameData();
    }
}
