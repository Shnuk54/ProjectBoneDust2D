using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongSkeleton : SkeletonBodyClass
{
    private PlayerController _player;
    private void Start()
    {   
       Configurate();
       skeletonID = 2;
    }
    private void Configurate(){

        _skeletonEndurence = PlayerStats.endurenceSkeleton;
        UISlidersHandler.SetSliderValue(_skeletonEndurence,_skeletonEndurence,UISlidersHandler.enduranceSlider);
        _rb = gameObject.GetComponent<Rigidbody2D>();
        if(_headPos.transform == null)_headPos = _rb.transform;
       Skeleton(_headPos,_rb,_skeletonEndurence,_enduranceLostByMoving);
        gameObject.transform.SetParent(null);
         _player = FindObjectOfType<PlayerController>();
        _player.ChangeParent(gameObject);
        _player.ChangePosition(_headPos.position);
       _speed = PlayerStats.speedSkeleton;
    }
    
    private void DragItem(){

    }
    private void Attack(){
        
    }
    private void FixedUpdate()
    {
        Grounded();
       if(!IsDestroyed){
        if(_player.transform.position != _headPos.position)_player.ChangePosition(_headPos.position);
        SkeletonMovement(_speed);
        }
    }
}
