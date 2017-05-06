using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{

    // Camera
    CameraMovement mainCam;
    //

    // Players
    public int playerCount = 0;
    public List<ControllerScript> players = new List<ControllerScript>();
    public ControllerScript currentPlayer;
    public int currentPlayerTurn = 0;
    GameObject playerPrefab;

    public Transform[] spawnPoints;
    // 

    //UI
    AbilitiesUI abilitiesUI;
    //

    void Awake()
    {
        GameMaster.gameMode = this;
        mainCam = Camera.main.GetComponent<CameraMovement>();
        abilitiesUI = FindObjectOfType<AbilitiesUI>();

        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        InitGame();
    }

    /// <summary>
    /// Called at the start of every game
    /// </summary>
    void InitGame()
    {
        playerCount = GameMaster.playerCount; // TODO: UNCOMMENT THIS, just for testing purposes it is left out
        SpawnAllPlayers();
        SwitchToPlayer(currentPlayerTurn);
    }

    /// <summary>
    /// Spawns all necessary players
    /// </summary>
    void SpawnAllPlayers()
    {
        for (int i = 0; i < playerCount; i++)
        {
            GameObject newPlayer = Instantiate(playerPrefab, spawnPoints[i].position, playerPrefab.transform.rotation);
            ControllerScript cs = newPlayer.GetComponent<ControllerScript>();
            cs.SetID(players.Count);
            players.Add(newPlayer.GetComponent<ControllerScript>());
        }
    }

    void Start()
    {

    }


    void Update()
    {

    }

    /// <summary>
    /// Add a new player to the list of players
    /// </summary>
    /// <param name="_player"></param>
    public void AddPlayer(ControllerScript _player)
    {
        players.Add(_player);
    }

    public void RemovePlayer(ControllerScript _player)
    {
        int tempID = _player.playerID;
        players.Remove(_player);
        for(int i = tempID; i < players.Count; i++)
        {
            players[i].SetID(players[i].playerID - 1);
        }
    }

    /// <summary>
    /// Go to next player's turn
    /// </summary>
    public void AdvanceTurn()
    {
        Invoke("UpdateTurn", 3.0f); // delay this function by a second
    }

    void UpdateTurn()
    {
        if (currentPlayerTurn < players.Count - 1)
        {
            currentPlayerTurn++;
            SwitchToPlayer(currentPlayerTurn);
        }
        else
        {
            currentPlayerTurn = 0;
            SwitchToPlayer(currentPlayerTurn);
        }
    }

    /// <summary>
    /// Locks camera to new player and activates them
    /// </summary>
    /// <param name="index"></param>
    void SwitchToPlayer(int index)
    {
        mainCam.FollowTarget(players[index].transform);
        players[index].Activate();

        currentPlayer = players[index];
        abilitiesUI.SetMobile(currentPlayer.GetComponent<AllMobiles.Mobiles>());

    }
}
