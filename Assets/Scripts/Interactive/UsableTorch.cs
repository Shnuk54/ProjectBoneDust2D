using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsableTorch : MonoBehaviour,IUseble
{
    [SerializeField] private WeakSkeleton _skeleton;
    [SerializeField] private float _distanceToUse;
    private bool _used;
  


    private void ChangeParent(GameObject newParent){
        this.transform.parent = newParent.transform;
        this.transform.position = newParent.transform.position;}
    

  private void OnMouseUp()
  {
       Use();
  }
   
    void FixedUpdate() {
        if(!_used)return;
        if(this.transform.position != _skeleton.GetTorchPosition.transform.position)this.transform.position = _skeleton.GetTorchPosition.transform.position;
    }

    public void Use() {
    if(FindObjectOfType<PlayerController>().IsSkeleton && !_used && FindObjectOfType<WeakSkeleton>()!= null){
            _skeleton = FindObjectOfType<WeakSkeleton>();
            if(Vector3.Distance(_skeleton.transform.position,this.transform.position) <= _distanceToUse )
            {
             _used = true;
            ChangeParent(_skeleton.GetTorchPosition);
            }
       }
    }

    void OnEnable() {
        Events.onPlayerChangeState += Destroy;
    }
    void OnDisable() {
        Events.onPlayerChangeState -= Destroy;
    }
    private void Destroy(PlayerState state){
        if(state == PlayerState.Skull && _used){
            Destroy(this.gameObject);
        }
    }
}
