using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIInputHandler : MonoBehaviour
{
   
    public static Joystick horJoy{get;private set;}
    public static Button jumpButton{get;private set;}
    public static Button skeletonJumpButton{get;private set;}
    public static Button skeletonsButton{get;private set;}
    public static Button pauseButton{get;private set;}
    void Awake() {
        jumpButton = GameObject.FindGameObjectWithTag("ButtonJump").GetComponent<Button>();
        skeletonsButton = GameObject.FindGameObjectWithTag("ButtonSkeletons").GetComponent<Button>();
        skeletonJumpButton = GameObject.FindGameObjectWithTag("ButtonSkeletonJump").GetComponent<Button>();
        horJoy = GameObject.FindGameObjectWithTag("HorJoy").GetComponent<Joystick>();
        pauseButton = GameObject.FindGameObjectWithTag("ButtonPause").GetComponent<Button>();
        UIInputHandler.jumpButton.onClick.RemoveAllListeners();
        UIInputHandler.jumpButton.onClick.AddListener(delegate{   
        if (!PlayerController.canJumpFromBody)
            {
                PlayerController.canJumpFromBody = true;
                EnableHorJoy(false);
            }
            else
            {
                PlayerController.canJumpFromBody = false;
                EnableHorJoy(true);
            }
       }); 
       
        skeletonsButton.onClick.AddListener(delegate{
        FindObjectOfType<SkeletonSpawner>().ShowSkeletonsMenu();
       });
    }
   public static void EnableHorJoy(bool isEnabled){
        if(isEnabled && UIInputHandler.horJoy!= null)UIInputHandler.horJoy.transform.localScale = new Vector3(1,1,1);
        if(!isEnabled)UIInputHandler.horJoy.transform.localScale = new Vector3(1,0,1);
    }
    public static void EnableJumpButton(bool isEnabled){
        if(isEnabled && UIInputHandler.jumpButton != null)UIInputHandler.jumpButton.transform.localScale = new Vector3(1,1,1);
        if(!isEnabled)UIInputHandler.jumpButton.transform.localScale = new Vector3(1,0,1);
    }
    public static void EnableSkeletonJumpButton(bool isEnabled){
        if(isEnabled && UIInputHandler.skeletonJumpButton != null)UIInputHandler.skeletonJumpButton.transform.localScale = new Vector3(1,1,1);
        if(!isEnabled)UIInputHandler.skeletonJumpButton.transform.localScale = new Vector3(1,0,1);
    }
}
