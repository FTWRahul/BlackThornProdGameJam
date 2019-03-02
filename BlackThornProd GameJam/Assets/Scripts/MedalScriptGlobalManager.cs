using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalScriptGlobalManager : MonoBehaviour
{
    public GlobalManager globalMng;
    public GameObject medal1;
    public GameObject medal2;
    public GameObject medal3;
    public GameObject medal4;
    public GameObject medal5;

    // Start is called before the first frame update
    void Start()
    {
        globalMng = FindObjectOfType<GlobalManager>();
        if(globalMng.blnMedal1)
        {
            medal1.SetActive(true);
        }
        if(globalMng.blnMedal2)
        {
            medal2.SetActive(true);
        }
        if(globalMng.blnMedal3)
        {
            medal3.SetActive(true);
        }
        if(globalMng.blnMedal4)
        {
            medal4.SetActive(true);
        }
        if (globalMng.blnMedal5)
        {
            medal5.SetActive(true);
        }
    }
}
