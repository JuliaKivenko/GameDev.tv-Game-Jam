using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{

    public void Save()
    {
        SaveObject saveObject = new SaveObject();
        saveObject.health = PlayerController.sharedInstance.playerStats.health;
        saveObject.damage = PlayerController.sharedInstance.playerStats.damage;

        string json = string.Empty;

        json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/save.json", json);

        Debug.Log(json);
    }

    public void Load()
    {
        string json = File.ReadAllText(Application.dataPath + "/save.json");
        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(json);

        PlayerController.sharedInstance.playerStats.health = saveObject.health;
        PlayerController.sharedInstance.playerStats.damage = saveObject.damage;

        Debug.Log(PlayerController.sharedInstance.health.baseHealth + " " + PlayerController.sharedInstance.playerStats.damage);
    }

}

public class SaveObject
{
    public float health;
    public float damage;
}

