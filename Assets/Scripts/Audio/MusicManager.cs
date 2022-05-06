using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour
{
     private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

      void OnSceneLoaded(Scene scene, LoadSceneMode mode){
         if(MenuManager.GetCurrentScene() == 1){
            AudioManager.singleton.Play("Music1");
            AudioManager.singleton.Play("Rain");
        }
    }
    void OnActiveSceneChanged(Scene scene, Scene scene1){
        AudioManager.singleton.StopAll();
    }
}
