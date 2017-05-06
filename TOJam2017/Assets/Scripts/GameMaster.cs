using UnityEngine;
using System.Collections;

/// <summary>
/// Stores global variables and functions that are called in between levels/scenes.
/// </summary>
public class GameMaster {
    public enum MobileType
    {
        Skitty,
        Goaty
    }
    public static MobileType[] mobileTypes;
    public static int playerCount = 0;
    public static GameMode gameMode;

    /// <summary>
    /// Initializes mobile types as an array of size 4
    /// </summary>
    public static void SetMobileTypes()
    {
        mobileTypes = new MobileType[4];
    }
}
