using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    //Prefabs
    public GameObject playerFieldPrefab;

    //Panels
    public GameObject menuPanel, lobbyPanel, creditsPanel;

    public Transform playerGrid;

    //Input fields
    public InputField userField, ipField, portField;

    int currentPlayerCount = 0;

    //Mobile types
    public GameMaster.MobileType[] mobileTypes;

    private void Awake()
    {
       mobileTypes = new GameMaster.MobileType[4];
       GameMaster.SetMobileTypes();
    }    

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
        creditsPanel.SetActive(false);
    }

    void AddPlayerName()
    {
        if(currentPlayerCount < 4)
        {
            GameObject newPlayerField = Instantiate(playerFieldPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            newPlayerField.transform.SetParent(playerGrid, false);
            newPlayerField.transform.GetChild(0).GetComponent<Text>().text = "Player" + currentPlayerCount;
            newPlayerField.GetComponent<MobileTypeChanger>().id = currentPlayerCount;
            newPlayerField.GetComponent<MobileTypeChanger>().menu = this;
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

    public void OnClickCreditBtn()
    {
        DisableAllPanels();
        creditsPanel.SetActive(true);
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
        GameMaster.mobileTypes = mobileTypes;
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
