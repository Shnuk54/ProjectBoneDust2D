using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerSpawnController : MonoBehaviour,ISaveable
{
   
   [SerializeField] private  int currentScene; 
    public static PlayerSpawnController singleton {get;private set;}
    private PlayerController _player;
    private void Awake()
    {
        if(!singleton){
        singleton = this;
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }
        else{
        Destroy(gameObject);
        }
    }
    void Start()

    {   SaveLoad.singleton.Load();
        if( currentScene != 0 && MenuManager.GetCurrentScene() != 0){
        MenuManager.LoadSceneByIndex(currentScene);
        }
        _player = FindObjectOfType<PlayerController>();
       
    }
    public  void ChangeCurrentSceneIndex(int index){
        currentScene = index;
    }

    void  OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(PlayerController.usedCheckpoint == false){
            PlayerController.spawnpoint = GameObject.FindGameObjectWithTag("StartPos").transform.position;
            _player.ChangePosition(PlayerController.spawnpoint);
        }else{
            _player.ChangePosition(PlayerController.spawnpoint);
        }
       FindObjectOfType<PlayerController>().SetupPlayer();
    }
    
    void OnActiveSceneChanged(Scene scene, Scene scene1){
        PlayerController.usedCheckpoint = false;
    }

    public object CaptureState(){
             return new SaveData{
                currentScene = MenuManager.GetCurrentScene()
        };
    }
    
    public void RestoreState(object state){
            var saveData = (SaveData)state;
           currentScene = saveData.currentScene;
    }

    [System.Serializable]
    private struct SaveData
    {
      public int currentScene;
    }
}
