using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public  class UIController : MonoBehaviour
{
    [SerializeField] private Menu[] _menuCanvas;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

     void  OnSceneLoaded(Scene scene, LoadSceneMode mode){
         if(MenuManager.GetCurrentScene() != 0){
           EnableSkeletonUI(false);
           UIInputHandler.pauseButton.transform.localScale = new Vector3(1,1,1);
         }else{
         UIInputHandler.pauseButton.transform.localScale = new Vector3(1,1,1);
         EnableSkeletonUI(false);
         }
    }
    
    void OnActiveSceneChanged(Scene scene, Scene scene1){
       
    }
    private void OnEnable()
    {
       Events.onPlayerChangeState += ChangePlayerState;
       Events.onPlayerChangeHealth += UISlidersHandler.SetSliderValue;
    }
    private void OnDisable()
    {   
       Events.onPlayerChangeState -= ChangePlayerState;
       Events.onPlayerChangeHealth -= UISlidersHandler.SetSliderValue;
    }
    public void EnableMenu(MenuType type,bool enable,bool disableAllAnother){
        Menu menu = Array.Find(_menuCanvas, menu => menu.type == type);
        if(enable) menu.canvas.SetActive(true);
        if(enable == false) menu.canvas.SetActive(false);
        if(disableAllAnother == true){
            foreach(Menu _menu in _menuCanvas){
                if(_menu.type == type){return;}else{
                    _menu.canvas.SetActive(false);
                }
            }
        }
    }
    private void ChangePlayerState(PlayerState state){
        if(state == PlayerState.Skeleton){
        Events.onLostEndurance += UISlidersHandler.SetSliderValue;
        EnableSkeletonUI(true);}
        if(state == PlayerState.Skull){
        Events.onLostEndurance -= UISlidersHandler.SetSliderValue;
        EnableSkeletonUI(false);
       }
     }
  
   
    public static void EnableLoadMenu(bool isEnabled){
        if(!isEnabled){
            GameObject.FindGameObjectWithTag("LoadMenu").gameObject.SetActive(false);}
        if(isEnabled){
            GameObject.FindGameObjectWithTag("LoadMenu").gameObject.SetActive(true);}
    }
    
    private  void EnableSkeletonUI(bool isEnabled){
        if(isEnabled){
          EnableMenu(MenuType.SkeletonUI,true,false);
        }
        if(!isEnabled){
           EnableMenu(MenuType.SkeletonUI,false,false);
        }
    }

    
       
}
