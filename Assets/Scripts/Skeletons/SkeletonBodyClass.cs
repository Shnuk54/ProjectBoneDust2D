using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  System;
using UnityEngine.Events;


public class SkeletonBodyClass : MonoBehaviour,IAlive,ISkeletonBody
{
[SerializeField] public  int skeletonID{get;set;}
[SerializeField] protected Transform _headPos;
[SerializeField] protected Rigidbody2D _rb;
[SerializeField] protected Vector3 _pos;
[SerializeField] protected float _skeletonEndurence;
[SerializeField] protected float _speed;
[SerializeField] protected bool _isDestroyed = false;
[SerializeField] protected float _enduranceLostByMoving;
[SerializeField] protected float _jumpForce;
[SerializeField] Transform bottom;
[SerializeField] protected bool _withTorch;
[SerializeField] protected GameObject _torchPosition;
private ISphereCheckProvider _groundCheck;
[SerializeField] protected bool _grounded;
[SerializeField] private LayerMask _groundLayer;
[SerializeField] private float _groundCheckRaius;

    private void Awake()
    {   _groundCheck = GetComponent<ISphereCheckProvider>();
         UIInputHandler.skeletonJumpButton.onClick.AddListener(delegate{
        if (_grounded)
            {
               SkeletonJump(); 
            }
          
       });
       DontDestroyOnLoad(this.gameObject);
    }
   
    public GameObject GetTorchPosition{
        get{
            return _torchPosition;
        }
    }

    public bool IsDestroyed{
        get{
            return _isDestroyed;
        }
    }

   protected void Skeleton(Transform headPos,Rigidbody2D rb, float skeletonEndurence,float enduranceLostByMoving)
   {
       _headPos = headPos;
       _rb = rb;
       _skeletonEndurence = skeletonEndurence;
       _enduranceLostByMoving = enduranceLostByMoving;
   }
    void FixedUpdate() {
        Grounded();
    }
    protected void Grounded(){
        _grounded = _groundCheck.CheckLayer(bottom,_groundCheckRaius,_groundLayer);
    }
    protected virtual void SkeletonMovement(float _speed)
    {
        
        if(!_isDestroyed && _grounded){

         _pos = _rb.transform.position;
         var hor = UIInputHandler.horJoy.Horizontal;
         
        if (hor > 0)
        {
            Vector2 movVector = new Vector2(hor,0);
            _rb.AddForce(movVector*_speed,ForceMode2D.Impulse);
            LostEndurence(_enduranceLostByMoving*(hor/10));
        }
        if (hor < 0)
        {
            Vector2 movVector = new Vector2(hor,0);
            _rb.AddForce(movVector*_speed,ForceMode2D.Impulse);
            LostEndurence(_enduranceLostByMoving*(-hor/10));
        }
        
            }
    }

    protected virtual void SkeletonJump(){
        if( !_isDestroyed){
        _rb.AddForce(new Vector2(0,_jumpForce),ForceMode2D.Impulse);
        LostEndurence(25f);
        }
    }

   public void Death(){
        _isDestroyed = true;
    }
    public void Heal(float heal){

    }
    public  void TakeDamage(float damage){
         LostEndurence(damage);
    }
    protected void LostEndurence(float endurenceLost){
        if(_skeletonEndurence > 0){
                _skeletonEndurence-= endurenceLost;
        Events.instance.OnLostEndurance(_skeletonEndurence,PlayerStats.endurenceSkeleton,UISlidersHandler.enduranceSlider);
        }else{
            PlayerStateController.singleton.ChangePlayerState(PlayerState.Skull);
            _isDestroyed = true;
        }
    }
      private void OnEnable()
    {
        Events.onPlayerChangeState += OnDestroyed;
    }
    private void OnDisable()
    {
        Events.onPlayerChangeState -= OnDestroyed;
    }

    private void OnDestroyed(PlayerState state){
       if(state == PlayerState.Skull) Death();
    }

}