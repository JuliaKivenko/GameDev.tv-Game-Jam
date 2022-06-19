using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{

    public void Save()
    {
        SaveObject saveObject = new SaveObject();
        saveObject.health = PlayerController.sharedInstance.health.baseHealth;

        string json = string.Empty;

        json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/save.json", json);

        Debug.Log(json);
    }

    public void Load()
    {
        string json = File.ReadAllText(Application.dataPath + "/save.json");
        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(json);

        PlayerController.sharedInstance.health.baseHealth = saveObject.health;

        Debug.Log(PlayerController.sharedInstance.health.baseHealth);
    }

}

public class SaveObject
{
    public float health;
}

