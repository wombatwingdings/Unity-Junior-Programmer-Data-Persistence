using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;
    public TextMeshProUGUI HighScoreText;
    public string playerName;
    public string highScoreName;
    public int highScore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
            playerName = "Anon";
            Debug.Log("Player name set");
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerNameChanged(string n)
    {
        playerName = n;
        Debug.Log("Player name changed: " + n);
    }  

    [System.Serializable]
    public class WriteData
    {
        public string highScoreName;
        public int highScore = 0;
    }

    public void SaveData()
    {
        WriteData data = new WriteData();
        data.highScoreName = highScoreName;
        data.highScore = highScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            WriteData data = JsonUtility.FromJson<WriteData>(json);
            highScoreName = data.highScoreName;
            highScore = data.highScore;
            HighScoreText.text = "High Score: " + highScoreName + " - " + highScore;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        SaveData();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
