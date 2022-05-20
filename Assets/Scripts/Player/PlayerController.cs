using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour,IAlive,ISkull
{
    [SerializeField] private  Rigidbody2D _rb;
    public static bool usedCheckpoint = false;
    private bool _dead;
    private  bool _isSkeleton = false;
    public static bool canJumpFromBody = false;
    // PlayerStats

    [SerializeField] private float _hp;
    [SerializeField] private float _afterDamageDelay = 2f;

    private bool _canTakeDamage = true;
    private static bool _playerExists;
    private void Start()
    {
     if(!_playerExists){
            _playerExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _hp = PlayerStats.healthPoint;
        UISlidersHandler.SetSliderValue(_hp,PlayerStats.healthPoint,UISlidersHandler.healthSlider);
        _isSkeleton = false;
        PlayerStateController.singleton.ChangePlayerState(PlayerState.Skull);
    }
    public void SetupPlayer(){
        _hp = PlayerStats.healthPoint;
        _isSkeleton = false;
        UISlidersHandler.SetSliderValue(_hp,PlayerStats.healthPoint,UISlidersHandler.healthSlider);
        ReturnToSkull();
    }
    public Rigidbody2D PlayerRB
    {
        get
        {
            return _rb;
        }
    }

    public  void TakeDamage(float damage){
        if(!_canTakeDamage)return;
        if(_hp-damage <= 0){
            Death();
            return;}
        if(_hp-damage > 0 ){
                _hp -= damage;
                StartCoroutine(AfterDamageDelay());
                GetComponent<ParticleController>().Play("Death");
                Events.instance.OnPlayerChangeHealth(_hp,PlayerStats.healthPoint,UISlidersHandler.healthSlider);
        }
    }
    public void Heal(float heal){

    }
    private IEnumerator AfterDamageDelay(){
            _canTakeDamage = false;
            yield return new WaitForSeconds(_afterDamageDelay);
            _canTakeDamage = true;
            StopCoroutine(AfterDamageDelay());
    }

   
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "Checkpoint"){
                usedCheckpoint = true;
               FindObjectOfType<PlayerSpawnController>().ChangeCurrentSceneIndex(MenuManager.GetCurrentScene());
               UISlidersHandler.SetSliderValue(PlayerStats.healthPoint,PlayerStats.healthPoint,UISlidersHandler.healthSlider);
               _hp = PlayerStats.healthPoint;
               Events.instance.OnPlayerUseCheckpoint(other.transform.position.x,other.transform.position.y,MenuManager.GetCurrentScene());
               SaveLoad.singleton.Save();
            }
        }

    void OnEnable() {
        Events.onPlayerTakeDamage += TakeDamage;
        Events.onPlayerChangeState += ChangeState;
    }

    void OnDisable() {
        Events.onPlayerTakeDamage -= TakeDamage;
        Events.onPlayerChangeState -= ChangeState;
    }

    private void ChangeState(PlayerState state){
        if(state == PlayerState.Skeleton){
            SetPlayerRigBodyKinematic(true);
            _isSkeleton = true;
        }
        if(state == PlayerState.Skull){
            ReturnToSkull();
        }
    }
    public void ReturnToSkull()
    {
        _isSkeleton = false;
        gameObject.transform.SetParent(null);
        _rb.isKinematic = false;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeParent(GameObject newParent)
    {
        gameObject.transform.parent = newParent.transform;
    }


    public void ChangePosition(Vector2 newPos)
    {
        gameObject.transform.position = new Vector3(newPos.x,newPos.y,0); 
    }


    public bool IsSkeleton
    {
        get
        {
            return _isSkeleton;
        }
        set{
            _isSkeleton = value;
        }
    }

    public void SetPlayerRigBodyKinematic(bool isEnabled){
        if(isEnabled)_rb.isKinematic = true;
        if(!isEnabled)_rb.isKinematic = false;
    }
  
 
     public void Death()
    {   StartCoroutine(AfterDamageDelay());
        Respawn();
        ReturnToSkull();
    }

    private void Respawn()
    {   
        _hp = PlayerStats.healthPoint;
        Events.instance.OnPlayerChangeHealth(_hp,PlayerStats.healthPoint,UISlidersHandler.healthSlider);
    }

    
}
