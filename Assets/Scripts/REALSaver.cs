using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class REALSaver : MonoBehaviour
{
    public SaveFile save;
    
    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

   public void SaveGame()
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/player_save.txt");
        var json = JsonUtility.ToJson(save);
        bf.Serialize(file, json);
        file.Close();
    }

    public void LoadGame()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(Application.persistentDataPath + "/game_save/player_save.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/player_save.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), save);
            file.Close();
        }
    }
}
