using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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

    public void SaveFile() {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) {
            file = File.OpenWrite(destination);
        } else {
            file = File.Create(destination);
        }

        GameData data = new GameData(blnUnlock5, blnMedal1, blnMedal2, blnMedal3, blnMedal4, blnMedal5);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadFile() {
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) {
            file = File.OpenRead(destination);
        } else {
            Debug.LogError("File not found");
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        blnUnlock5 = data.dataUnlock5;
        blnMedal1 = data.dataMedal1;
        blnMedal2 = data.dataMedal2;
        blnMedal3 = data.dataMedal3;
        blnMedal4 = data.dataMedal4;
        blnMedal5 = data.dataMedal5;

        Debug.Log(data.dataUnlock5);
        Debug.Log(data.dataMedal1);
        Debug.Log(data.dataMedal2);
        Debug.Log(data.dataMedal3);
        Debug.Log(data.dataMedal4);
        Debug.Log(data.dataMedal5);
    }
}
