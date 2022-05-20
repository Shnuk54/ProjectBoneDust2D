
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    public static Events instance;
    public static event UnityAction<PlayerState> onPlayerChangeState;
    public static event UnityAction<float> onPlayerTakeDamage;
    public static event UnityAction<float,float,Slider> onPlayerChangeHealth;
    public static event UnityAction<float,float,Slider> onLostEndurance;
    public static event UnityAction<float,float,int> onUseCheckpoint;
    private void Awake()
    {  
        if(instance != null && instance != this)
        {Destroy(this.gameObject);}
        else{instance = this;DontDestroyOnLoad(this);}
    }
    public void OnPlayerChangeState(PlayerState state){
        if(onPlayerChangeState != null){
            onPlayerChangeState(state);
        }
    }
    public void OnPlayerUseCheckpoint(float posX,float posY,int sceneIndex){
        if(onUseCheckpoint != null){
            onUseCheckpoint(posX,posY,sceneIndex);
        }
    }
     public void OnPlayerTakeDamage(float damage){
        if(onPlayerTakeDamage != null){
            onPlayerTakeDamage(damage);
        }
    }

    public void OnPlayerChangeHealth(float health,float maxHealth,Slider slider){
        if(onPlayerChangeHealth != null){
            onPlayerChangeHealth(health,maxHealth,slider);
        }
    }

    public void OnLostEndurance(float endurance,float maxEndurance,Slider slider){
        if(onLostEndurance != null){
            onLostEndurance(endurance,maxEndurance,slider);
        }
    }
}
