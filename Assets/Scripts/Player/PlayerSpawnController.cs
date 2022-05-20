using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerSpawnController : MonoBehaviour,ISaveable
{
   
     [SerializeField] private  int currentScene; 
     [SerializeField] private int savedScene;
     [SerializeField] Vector2 spawnPoint;
    public static PlayerSpawnController singleton {get;private set;}
    private PlayerController _player;
    private void Awake()
    {
        if(!singleton){
        singleton = this;
        DontDestroyOnLoad(this);
        }
        else{
        Destroy(gameObject);
        }
    }
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Events.onUseCheckpoint += PlayerSaved;
    }
    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Events.onUseCheckpoint -= PlayerSaved;
    }
    void Start()
    {  
        _player = FindObjectOfType<PlayerController>();
        SaveLoad.singleton.Load();
        if( MenuManager.GetCurrentScene() != 0){
        MenuManager.LoadSceneByIndex(savedScene);
        }
    }
    public void LoadSavedScene(){
         MenuManager.LoadSceneByIndex(savedScene);
    }
    public  void ChangeCurrentSceneIndex(int index){
        currentScene = index;
    }
    private void PlayerSaved(float posX, float posY,int sceneIndex){
        spawnPoint = new Vector2(posX,posY);
        savedScene = sceneIndex;
    }
    void  OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(MenuManager.GetCurrentScene() != savedScene){
            spawnPoint = GameObject.FindGameObjectWithTag("StartPos").transform.position;
            _player.ChangePosition(spawnPoint);
        }else{
            _player.ChangePosition(spawnPoint);
        }
    }
    
   

    public object CaptureState(){
             return new SaveData{
                savedScene = savedScene,
                posX = spawnPoint.x,
                posY = spawnPoint.y
        };
    }
    
    public void RestoreState(object state){
            var saveData = (SaveData)state;
            savedScene = saveData.savedScene;
            spawnPoint = new Vector2(saveData.posX,saveData.posY);
    }

    [System.Serializable]
    private struct SaveData
    {
     public int savedScene;
     public float posX;
     public float posY;
    }
}
