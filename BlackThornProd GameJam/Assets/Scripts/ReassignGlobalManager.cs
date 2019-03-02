using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReassignGlobalManager : MonoBehaviour {

    public GlobalManager globalMng;
    public Button level5Button;

    // Start is called before the first frame update
    void Start()
    {
        level5Button = GetComponent<Button>();
        globalMng = FindObjectOfType<GlobalManager>();
        level5Button.onClick.AddListener(globalMng.Level5);
    }
}
