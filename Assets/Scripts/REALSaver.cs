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
        FileStream file = File.Create(Application.persistentDataPath + "/game-save");
        var json = JsonUtility.ToJson(save);
        bf.Serialize(file, json);
        file.Close();
    }
}
