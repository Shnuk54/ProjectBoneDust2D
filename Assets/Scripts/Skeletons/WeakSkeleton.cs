using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakSkeleton : SkeletonBodyClass
{
private PlayerController _player;
private AnimatorState _animator;
    private void Start()
    {   
       Configurate();
       _animator = this.GetComponent<AnimatorState>();
       skeletonID = 1;

       AnimationState[] anims = new AnimationState[]{
           new AnimationState(AnimationPart.Body,"Body_Idle"),
           new AnimationState(AnimationPart.Arms,"Arms_Idle"),
           new AnimationState(AnimationPart.Legs,"Legs_Idle"),
           new AnimationState(AnimationPart.Arms,"Arms_Walk"),
           new AnimationState(AnimationPart.Legs,"Legs_Walk"),
           new AnimationState(AnimationPart.Body,"Body_Walk")
       };
       _animator.AddAnimations(anims);
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
  
    private void FixedUpdate()
    {    
        if(IsDestroyed)return;
        Grounded();
        if(_player.transform.position != _headPos.position)_player.ChangePosition(_headPos.position);
        SkeletonMovement(_speed);
        if(UIInputHandler.horJoy.Horizontal > 0 || UIInputHandler.horJoy.Horizontal < 0){
            _animator.PlayAnimation("Legs_Walk");
            _animator.PlayAnimation("Arms_Walk");
            _animator.PlayAnimation("Body_Walk");
        }else{
            _animator.PlayAnimation("Legs_Idle");
            _animator.PlayAnimation("Arms_Idle");
            _animator.PlayAnimation("Body_Idle");
        }
    }
        
}
