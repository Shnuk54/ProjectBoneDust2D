using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonSpawner : MonoBehaviour 
{
 [SerializeField] protected List<GameObject> _SkeletonBody;
 [SerializeField] protected Rigidbody2D _rb;  
 [SerializeField] protected float distanceToUse = 5f;
 [SerializeField] private GameObject _bonePillUI;
 protected PlayerController _player;
private bool _used = false;
    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }
    
     public void UseBonePill(string skeletonName){
        
         _rb = _player.GetComponent<Rigidbody2D>();
        PlayerStateController.singleton.ChangePlayerState(PlayerState.Skeleton);
        GameObject skeletonBody = Instantiate(Resources.Load($"SkeletonsPrefabs/{skeletonName}") as GameObject);
        skeletonBody.transform.position = _rb.transform.position;
     }

     public void ShowSkeletonsMenu(){
            if(PlayerItemsSystem.singleton.GetSkeletonsCount() != 0){
            if(_player == null) _player = FindObjectOfType<PlayerController>();
            if(_player.IsSkeleton == false && Vector3.Distance(_player.transform.position,transform.position) <= distanceToUse){
            if(FindObjectOfType<MenuManager>().isOnPause() == false)Instantiate(_bonePillUI);
             }
            }
        }
  
}
