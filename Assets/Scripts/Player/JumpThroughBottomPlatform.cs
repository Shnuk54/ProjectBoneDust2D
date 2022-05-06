using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpThroughBottomPlatform : MonoBehaviour
{
    private Collider col;
    PlayerController player;
    SkullMovement movement;
    private bool onPlatform = false;
    private bool canJumpToBottom = false;

    private void Start()
    {
        col = GetComponent<Collider>();
        player = FindObjectOfType<PlayerController>();
        movement = FindObjectOfType<SkullMovement>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && player.PlayerRB.velocity.y > 0 && !onPlatform){
            StartCoroutine(JumpThroughPlatform());
            StartCoroutine(CheckIfOnPlatform());
            onPlatform = true;
        }
        if(other.tag == "Player"  && player.PlayerRB.velocity.y < 0 && !onPlatform){
            onPlatform = true;
            StartCoroutine(CheckIfOnPlatform());
        }
    }
private void OnTriggerExit(Collider other)
{
    if(other.tag == "Player"){
        onPlatform = false;
        canJumpToBottom = false;
    }

}
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" &&  onPlatform && canJumpToBottom &&  movement.GetrForce.y > 75){
            StartCoroutine(JumpThroughPlatform());
        }
    }

    IEnumerator JumpThroughPlatform(){
        col.isTrigger = true;
        yield return new WaitForSeconds(0.5f);
        col.isTrigger = false;
        StopCoroutine(JumpThroughPlatform());
    }

    IEnumerator CheckIfOnPlatform(){
        canJumpToBottom = false;
        yield return new WaitForSeconds(1f);
        canJumpToBottom = true;
        StopCoroutine(CheckIfOnPlatform());

    }
}
