using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour {

    // Booleans for the medal system
    public bool blnUnlock5;
    public bool blnMedal1;
    public bool blnMedal2;
    public bool blnMedal3;
    public bool blnMedal4;
    public bool blnMedal5;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void UnlockLevel5() {
        blnUnlock5 = blnMedal1 && blnMedal2 && blnMedal3 && blnMedal4 && blnMedal5;
    }

}
