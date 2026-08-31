using UnityEngine;

public class GameSaveController : MonoBehaviour
{
    public GameSaveController instance {get; private set;}
    GameSave gameSave;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There is already an instance of GameSaveController in the scene. Destroying this one.");
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Save()
    {
        GameSave gameSave = new GameSave();
        string json = JsonUtility.ToJson(gameSave);
        PlayerPrefs.SetString("GameSave", json);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        if(PlayerPrefs.HasKey("GameSave"))
        {
            string json = PlayerPrefs.GetString("GameSave");
            gameSave = JsonUtility.FromJson<GameSave>(json);
        }
    }
}
