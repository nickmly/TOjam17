using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MobileTypeChanger : MonoBehaviour {

    public int id = 0;
    public Dropdown dropdown;
    public MenuScript menu;

    public void OnChangeMobileType()
    {
        menu.mobileTypes[id] = (GameMaster.MobileType)dropdown.value;
    }
	
}
