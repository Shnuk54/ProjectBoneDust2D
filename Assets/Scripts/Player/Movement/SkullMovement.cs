using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkullMovement : MonoBehaviour
{
    [SerializeField] Vector2 mousePressDownPos;
    [SerializeField] Vector2 mouseReleasePos;
    [SerializeField] private  Rigidbody2D _rb;
    public static bool _isSkeleton{get;private set;} = false;
    public static bool _canJumpFromBody = false;
    [SerializeField]  private float _maxForce;

    // PlayerStats
    [SerializeField] private float _forceMultiplier;
    [SerializeField] private float maxForce;
    private float _doubleJumpForce;
    [SerializeField] private  Vector3 _force;
    [SerializeField]private ISkull _player;
    [SerializeField] private Transform _transform;
    [SerializeField] private float _playerDrag;

    private bool _screenIsTouched = false;
    [SerializeField] private float _rollingSpeed; 
    public static SkullMovement singleton;
     private ISphereCheckProvider _layerCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private  bool _canDobleJump = true;
    bool _grounded;
    private void Start()
    {
        if(!singleton){
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(gameObject);
        }

    _player = GetComponent<ISkull>();
    _layerCheck = GetComponent<ISphereCheckProvider>();
    _rb = GetComponent<Rigidbody2D>();
    _transform = GetComponent<Transform>();


    }
    private void Awake()
    {
  
    _forceMultiplier = PlayerStats.forceMultiplier;
    maxForce = PlayerStats.maxForce;
    _doubleJumpForce = PlayerStats.doubleJumpForce;
    _maxForce = maxForce;
    }
    private void FixedUpdate()
    { 
        var hor = UIInputHandler.horJoy.Horizontal;

         _grounded = _layerCheck.CheckLayer(this.transform,groundCheckRadius,groundLayer);

        if(_grounded) _canDobleJump = true;

         if(_screenIsTouched == false && _isSkeleton == false && _grounded){
            if(PhonePositionController.PhonePosition().x > 0.2f || PhonePositionController.PhonePosition().x < -0.2f){
                _rb.AddForce(new Vector2(PhonePositionController.PhonePosition().x*_rollingSpeed,0),ForceMode2D.Force);
         }
    }   
            
    }
    void Update() {
        Movement();
    }
    private void Movement(){
        if(Input.touchCount > 0){
             Touch touch =  Input.GetTouch(0);
             if (touch.phase == TouchPhase.Began)
                {
            OnMouseDown();
                }
           
            if(touch.phase == TouchPhase.Moved){
                OnMouseDrag();
            }
            if (touch.phase == TouchPhase.Ended){
                OnMouseUp();
            }
        }
    }
      private void OnMouseDown()
    {
        Touch touch = Input.GetTouch(0);
        mousePressDownPos = touch.position;
        _screenIsTouched = true;
    }


    private void OnMouseDrag()
    {    Touch touch = Input.GetTouch(0);
        Vector3 ForceInit = (touch.position - mousePressDownPos);
        maxForce = _maxForce;

        if (ForceInit.x > maxForce) ForceInit.x = maxForce;
        if (ForceInit.y > maxForce) ForceInit.y = maxForce;
        if (ForceInit.x < -maxForce) ForceInit.x = -maxForce;
        if (ForceInit.y < -maxForce) ForceInit.y = -maxForce;

        Vector2 ForceV = (new Vector2(-ForceInit.x, -ForceInit.y) * _forceMultiplier);

        if (!_player.IsSkeleton)
        {
            if (_grounded) DrawTrajecroty.singleton.UpdateTrajectory(ForceV, _rb, _transform.position);
            else
            {
                if (_canDobleJump)
                {
                    maxForce = _doubleJumpForce;
                    if (ForceInit.x > maxForce) ForceInit.x = maxForce;
                    if (ForceInit.y > maxForce) ForceInit.y = maxForce;
                    if (ForceInit.x < -maxForce) ForceInit.x = -maxForce;
                    if (ForceInit.y < -maxForce) ForceInit.y = -maxForce;
                    ForceV = (new Vector2(-ForceInit.x, -ForceInit.y) * _forceMultiplier);
                    DrawTrajecroty.singleton.UpdateTrajectory(ForceV, _rb, _transform.position);
                }
            }
        }
        else
        {   
            if (PlayerController.canJumpFromBody) DrawTrajecroty.singleton.UpdateTrajectory(ForceV, _rb, _transform.position);
        }
        
    }

    private void OnMouseUp()
    {
        DrawTrajecroty.singleton.HideLine();
        Touch touch = Input.GetTouch(0);
        mouseReleasePos = touch.position;
        _force = mouseReleasePos - mousePressDownPos;

        if (!_player.IsSkeleton)
        {
            if (_grounded)
            {
                maxForce = _maxForce;
                Shoot(mouseReleasePos - mousePressDownPos);
            }
            else
            {
                if (_canDobleJump)
                {
                    maxForce = _doubleJumpForce;
                    _canDobleJump = false;
                    Shoot(mouseReleasePos - mousePressDownPos);
                }
            }
        }
        else
        {
            if (PlayerController.canJumpFromBody && _force.x != 0 && _force.y != 0)
            {
               
                maxForce = _maxForce;
                PlayerStateController.singleton.ChangePlayerState(PlayerState.Skull);
                PlayerController.canJumpFromBody = false;
                Shoot(mouseReleasePos - mousePressDownPos);
            }
        }
        _screenIsTouched = false;
    }
 public Vector2 GetrForce{
        get{ return _force;}
    }
     void Shoot(Vector2 Force)
    {
        if (Force.x > maxForce) Force.x = maxForce;
        if (Force.y > maxForce) Force.y = maxForce;
        if (Force.x < -maxForce) Force.x = -maxForce;
        if (Force.y < -maxForce) Force.y = -maxForce;
        _rb.AddForce(new Vector2(-Force.x, -Force.y) * _forceMultiplier);
    }
  
}
