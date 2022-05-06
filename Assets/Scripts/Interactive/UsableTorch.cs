using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsableTorch : MonoBehaviour
{
    [SerializeField] protected float _lifeTime;
    [SerializeField] WeakSkeleton skeleton;
    [SerializeField] float distanceToUse;
    private bool _used;

    private void ChangeParent(GameObject newParent)=>this.transform.parent = newParent.transform;
    

    
        
  private void OnMouseUp()
  {
       if(FindObjectOfType<PlayerController>().IsSkeleton && !_used && FindObjectOfType<WeakSkeleton>()!= null){
           
            skeleton = FindObjectOfType<WeakSkeleton>();
            if(Vector3.Distance(skeleton.transform.position,this.transform.position) <= distanceToUse )
            {
             _used = true;
            ChangeParent(skeleton.GetTorchPosition);
            }
           
        }
  }


   
    private void OnDestroyed(){
        if(_used){
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Collider>().isTrigger = false;
        }
        
    }
    
}
