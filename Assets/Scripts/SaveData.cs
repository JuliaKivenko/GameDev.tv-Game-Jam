using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableList<T>
{
    public List<T> list = new List<T>();
}

public class SaveData : MonoBehaviour
{
    [SerializeField] Upgrade[] upgrades;

    public void Save()
    {
        SaveObject saveObject = new SaveObject();
        saveObject.SavedPlayerHealth = PlayerController.sharedInstance.playerStats.health;
        saveObject.SavedPlayerDamage = PlayerController.sharedInstance.playerStats.damage;
        saveObject.SavedPoints = GameManager.sharedInstance.points;
        saveObject.SavedBestDistance = GameManager.sharedInstance.bestDistance;
        saveObject.SavedBestPoints = GameManager.sharedInstance.bestPoints;

        for (int i = 0; i < upgrades.Length; i++)
        {
            SaveObject.SavedUpgrade savedUpgrade = new SaveObject.SavedUpgrade();

            savedUpgrade.upgradePrice = upgrades[i].upgradePrice;
            savedUpgrade.upgradeLevel = upgrades[i].upgradeLevel;

            saveObject.SavedUpgrades.list.Add(savedUpgrade);
        }

        saveObject.isFirstRun = false;

        string json = string.Empty;

        json = JsonUtility.ToJson(saveObject);
        string path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "save.json";
        File.WriteAllText(path, json);

        Debug.Log("Saved!");

    }

    public void Load()
    {
        string path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "save.json";
        if (!File.Exists(path))
        {
            Debug.Log("No savefile found!");
            return;
        }
        string json = File.ReadAllText(path);
        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(json);

        PlayerController.sharedInstance.playerStats.health = saveObject.SavedPlayerHealth;
        PlayerController.sharedInstance.playerStats.damage = saveObject.SavedPlayerDamage;
        GameManager.sharedInstance.LoadPointsFromSave(saveObject.SavedPoints);
        GameManager.sharedInstance.bestDistance = saveObject.SavedBestDistance;
        GameManager.sharedInstance.bestPoints = saveObject.SavedBestPoints;
        GameManager.sharedInstance.isFirstRun = saveObject.isFirstRun;
        for (int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i].upgradePrice = saveObject.SavedUpgrades.list[i].upgradePrice;
            upgrades[i].upgradeLevel = saveObject.SavedUpgrades.list[i].upgradeLevel;
        }

    }

}

[System.Serializable]
public class SaveObject
{
    /*public SaveObject(float savedPlayerHealth, float savedPlayerDamage, int savedPoints, float savedBestDistance, float savedBestPoints)
    {
        SavedPlayerHealth = savedPlayerHealth;
        SavedPlayerDamage = savedPlayerDamage;
        SavedPoints = savedPoints;
        SavedBestDistance = savedBestDistance;
        SavedBestPoints = savedBestPoints;
    }*/

    public float SavedPlayerHealth;
    public float SavedPlayerDamage;
    public int SavedPoints;
    public float SavedBestDistance;
    public float SavedBestPoints;

    public bool isFirstRun;

    [System.Serializable]
    public struct SavedUpgrade
    {
        public int upgradeLevel;
        public int upgradePrice;
    }

    public SerializableList<SavedUpgrade> SavedUpgrades = new SerializableList<SavedUpgrade>();
}

