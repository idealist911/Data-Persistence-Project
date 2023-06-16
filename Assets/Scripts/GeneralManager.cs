using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// For data persistence
public class GeneralManager : MonoBehaviour
{
    public static GeneralManager instance;

    public static Dictionary<string, int> scenes = new Dictionary<string, int>()
    {
        { "menu" , 0 },
        { "main" , 1 }
    };
    public string playername;
    public string currentPlayername;
    public int hiscore;
    string savefilename = "savefile.json";

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Ensures only one instance exists
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Keep it when switching scenes
        //Debug.Log(Application.persistentDataPath); // "C:/Users/tksd/AppData/LocalLow/DefaultCompany/SimpleBreakout"
    }

    [System.Serializable]
    class SaveData
    {
        public string playername; // player name
        public int score;   // high score
    }

    public void SavePlayerDetails()
    {
        SaveData data = new SaveData();
        data.playername = playername;
        data.score = hiscore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/" + savefilename, json);
    }

    public void LoadPlayerDetails()
    {
        string path = Application.persistentDataPath + "/" + savefilename;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            playername = data.playername;
            hiscore = data.score;
        }
        else
        {
            playername = currentPlayername;
            hiscore = 0;
        }
    }
}
