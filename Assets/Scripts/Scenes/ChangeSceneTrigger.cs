using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneTrigger : MonoBehaviour
{
    [SerializeField] bool loadNext;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
           
        var scene =  SceneManager.GetActiveScene();
        

         if(loadNext){
 SceneManager.LoadScene(scene.buildIndex+1); 
         }else{
 SceneManager.LoadScene(scene.buildIndex-1); 
         }
        }
    }

}
