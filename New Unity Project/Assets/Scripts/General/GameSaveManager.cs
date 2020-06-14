using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager instance;
    
    [System.Serializable]
    public class KeybindForPlatforms
    {
        public Keybindings keybindings;
        public string path;
    }

    public KeybindForPlatforms[] arrayOfBindings;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
        DontDestroyOnLoad(this);
    }

    public void SaveGame()
    {
        if(!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/keybindings"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/keybindings");
        }
        foreach (KeybindForPlatforms binding in arrayOfBindings)
        {
            BinaryFormatter binaryF = new BinaryFormatter();
            FileStream stream = File.Create(Application.persistentDataPath + binding.path);
            var json = JsonUtility.ToJson(binding.keybindings);
            binaryF.Serialize(stream, json);
            stream.Close();
        }
    }

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    public void LoadGame()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/keybindings"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/keybindings");
        }
        BinaryFormatter bf = new BinaryFormatter();
        foreach (KeybindForPlatforms binding in arrayOfBindings)
        {
            if(File.Exists(Application.persistentDataPath + binding.path))
            {
                FileStream file = File.Open(Application.persistentDataPath + binding.path, FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), binding.keybindings);
                file.Close();
            }
        }
    }
}
