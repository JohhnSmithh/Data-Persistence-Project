using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;


public class BestScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;

    public static BestScoreManager instance;

    public string currentName;
    public string bestScoreName;
    public int bestScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // set instances name input to the new/current name input
        instance.nameInput = nameInput;

        // load current best score
        LoadBestScore();
    }

    private void Update()
    {
        currentName = nameInput.text;
    }

    [System.Serializable] class BestScoreSaveData
    {
        public string bestScoreName;
        public int bestScore;
    }


    public void LoadBestScore()
    {
        // create and check path
        string path = Application.persistentDataPath + "/savefile2.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            BestScoreSaveData data = JsonUtility.FromJson<BestScoreSaveData>(json);

            bestScoreName = data.bestScoreName;
            bestScore = data.bestScore;
        }
    }

    public void SaveBestScore()
    {
        // create BestScoreSaveData class
        BestScoreSaveData data = new BestScoreSaveData();
        data.bestScoreName = bestScoreName;
        data.bestScore = bestScore;

        // convert data to json string
        string json = JsonUtility.ToJson(data);
            
        // write to json file
        File.WriteAllText(Application.persistentDataPath + "/savefile2.json", json);
    }
}
