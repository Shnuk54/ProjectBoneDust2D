using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private UIController _uiController;
    void Awake() {
        _uiController = GetComponent<UIController>();
    }
    public  void Pause(){
        UnityEngine.Time.timeScale = 0f;
    }
     public  void Continue(){
        UnityEngine.Time.timeScale = 1f;
    }
    public bool isOnPause(){
        bool isPaused;
        if(UnityEngine.Time.timeScale == 0f)isPaused = true; else isPaused = false;
        return isPaused;
    }
    public  void Restart(){
      var scene =  SceneManager.GetActiveScene();
      UnityEngine.Time.timeScale = 1f;
      FindObjectOfType<PlayerController>().IsSkeleton = false;
      PlayerStateController.singleton.ChangePlayerState(PlayerState.Skull);
      SceneManager.LoadScene(scene.buildIndex);
    }
    
    public  void Exit(){
        Application.Quit();
    }
    
    public  void LoadNextScene(){
        var scene =  SceneManager.GetActiveScene();
         SceneManager.LoadScene(scene.buildIndex+1);  
    }
    public void EnablePauseMenu() => _uiController.EnableMenu(MenuType.PauseMenu,true,true);
    public void DisablePauseMenu() => _uiController.EnableMenu(MenuType.PauseMenu,false,true);
    public void DisableSkeletonMenu() => _uiController.EnableMenu(MenuType.SkeletonUI,false,true);
    public void EnableSkeletonMenu() => _uiController.EnableMenu(MenuType.SkeletonUI,true,true);
    public void EnableOptionsMenu() => _uiController.EnableMenu(MenuType.OptionsMenu,true,true);
    public void DisableOptionsMenu() => _uiController.EnableMenu(MenuType.OptionsMenu,false,true);
    public void EnableUI()=>_uiController.EnableMenu(MenuType.UI,true,true);
    
    public static int GetCurrentScene(){
        return SceneManager.GetActiveScene().buildIndex;
    }
    public void LoadSceneByName(string name){
        SceneManager.LoadScene(name);
    }

    public static void LoadSceneByIndex(int index){
        SceneManager.LoadScene(index);
    }
}
