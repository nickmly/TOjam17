using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    //Prefabs
    public GameObject playerFieldPrefab;

    //Panels
    public GameObject menuPanel, lobbyPanel;

    public Transform playerGrid;

    //Input fields
    public InputField userField, ipField, portField;

    int currentPlayerCount = 0;

	void Start ()
    {
	
	}	
	
	void Update ()
    {

	}    

    void DisableAllPanels()
    {
        menuPanel.SetActive(false);
        lobbyPanel.SetActive(false);
    }

    void AddPlayerName()
    {
        if(currentPlayerCount < 4)
        {
            GameObject newPlayerField = Instantiate(playerFieldPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            newPlayerField.transform.SetParent(playerGrid, false);
            newPlayerField.transform.GetChild(0).GetComponent<Text>().text = "Player" + currentPlayerCount;
            currentPlayerCount += 1;
        }    
    }


    // ALL button clicks are called from their events in the scene
    // Check the button object for details

    /// <summary>
    /// Called when you click the start button in the main panel
    /// </summary>
    public void OnClickStartBtn()
    {      
        DisableAllPanels();
        lobbyPanel.SetActive(true);
        AddPlayerName();
    }

    public void OnClickAddPlayerBtn()
    {
        AddPlayerName();
    }
   
    /// <summary>
    /// Called when you click the start button in the lobby
    /// </summary>
    public void OnClickStartGameBtn()
    {
        GameMaster.playerCount = currentPlayerCount;
        SceneManager.LoadScene("Test");
    }
    
    /// <summary>
    /// Called from button event in scene
    /// </summary>
    public void OnClickBackBtn()
    {
        DisableAllPanels();
        menuPanel.SetActive(true);
    }
}
