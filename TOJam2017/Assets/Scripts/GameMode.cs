using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AllMobiles;

public class GameMode : MonoBehaviour
{
    /// <summary>
    /// Shot timer, resets and starts after switchPlayer()
    /// takes UI Reference from canvas in editor
    /// </summary>
    public Timer shotTimer;

    // Camera
    CameraMovement mainCam;
    //



    // Players
    public int playerCount = 0;
    public List<ControllerScript> players = new List<ControllerScript>();
    public ControllerScript currentPlayer;
    public int currentPlayerTurn = 0;
    GameObject playerPrefab;
    public Transform currentProjectile;
    public Transform[] spawnPoints;
    // 

    //UI
    public AbilitiesUI abilitiesUI;
    public Text winText;
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
        if (abilitiesUI.mobile == null)
            abilitiesUI.mobile = players[currentPlayerTurn].GetComponent<Mobiles>();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main");
        }

        if (players.Count == 1)
        {
            winText.enabled = true;
        }
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
        for (int i = tempID; i < players.Count; i++)
        {
            players[i].SetID(players[i].playerID - 1);
        }
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
        shotTimer.StartTimer();
    }
}
