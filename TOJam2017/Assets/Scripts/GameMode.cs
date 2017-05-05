using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{

    // Camera
    CameraMovement mainCam;
    //

    // Players
    List<ControllerScript> players = new List<ControllerScript>();
    public int currentPlayerTurn = 0;

    GameObject playerPrefab;

    public Transform[] spawnPoints;
    // 

    void Awake()
    {
        GameMaster.gameMode = this;
        mainCam = Camera.main.GetComponent<CameraMovement>();

        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        InitGame();
    }


    /// <summary>
    /// Called at the start of every game
    /// </summary>
    void InitGame()
    {
        SpawnAllPlayers();
    }

    /// <summary>
    /// Spawns all necessary players
    /// </summary>
    void SpawnAllPlayers()
    {
        for (int i = 0; i < GameMaster.playerCount; i++)
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

    /// <summary>
    /// Go to next player's turn
    /// </summary>
    public void AdvanceTurn()
    {
        Invoke("UpdateTurn", 1.0f); // delay this function by a second
    }

    void UpdateTurn()
    {
        if (currentPlayerTurn < players.Count - 1)
        {
            currentPlayerTurn++;
            mainCam.FollowTarget(players[currentPlayerTurn].transform);
            players[currentPlayerTurn].Activate();
        }
        else
        {
            currentPlayerTurn = 0;
            mainCam.FollowTarget(players[currentPlayerTurn].transform);
            players[currentPlayerTurn].Activate();
        }
    }
}
