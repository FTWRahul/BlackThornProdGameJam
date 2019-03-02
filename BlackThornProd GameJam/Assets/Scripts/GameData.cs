using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {
    // Booleans for the medal system
    public bool dataUnlock5;
    public bool dataMedal1;
    public bool dataMedal2;
    public bool dataMedal3;
    public bool dataMedal4;
    public bool dataMedal5;

    public bool dataLoadState;

    public GameData(bool inUnlock5, bool inMedal1, bool inMedal2, bool inMedal3, bool inMedal4, bool inMedal5) {
        dataUnlock5 = inUnlock5;
        dataMedal1 = inMedal1;
        dataMedal2 = inMedal2;
        dataMedal3 = inMedal3;
        dataMedal4 = inMedal4;
        dataMedal5 = inMedal5;
    }

    // Load just the state at the beginning (for the load button)
    public GameData(bool inLoadState) {
        dataLoadState = inLoadState;
    }
}
