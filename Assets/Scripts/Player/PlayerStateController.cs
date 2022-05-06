using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
   [SerializeField] private PlayerState _playerState;
    public static PlayerStateController singleton{get;private set;}
    void Awake() {
        if(!singleton){
        singleton = this;
        DontDestroyOnLoad(this);
        }
        else{
        Destroy(gameObject);
        }
    }
   public PlayerState PlayerCurrentState{
       get{
           return _playerState;
       }
   }
   public void ChangePlayerState(PlayerState state){
    _playerState = state;
    Debug.Log("State:"+ state);
    Events.instance.OnPlayerChangeState(state);
   }
}
