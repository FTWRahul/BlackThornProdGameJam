//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class ToggleButton : MonoBehaviour {

//    public Button loadButton;

//    public GlobalManager globalMng;

//    private void Awake() {
//        globalMng = FindObjectOfType<GlobalManager>();
//        globalMng.LoadState();
//        Debug.Log("AWAKE");
//        if (globalMng.blnLoadState) {
//            loadButton.interactable = true;
//        }
//    }

//    public void ActivateLoadButton() {
//        loadButton.interactable = true;
//    }
//}
