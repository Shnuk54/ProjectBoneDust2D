using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveblItem : MonoBehaviour
{
  [SerializeField] private bool _canBeMoved;
    private Rigidbody _rb;
  private void Start()
  {     _rb = this.GetComponent<Rigidbody>();
      _rb.constraints = RigidbodyConstraints.FreezeAll;
  }

    private void CanBeMoved(){
        if(_canBeMoved){
                _rb.constraints = RigidbodyConstraints.None;
               _rb.constraints = RigidbodyConstraints.FreezePositionZ;
               _rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
  private void OnTriggerEnter(Collider other)
  {
      if(other.tag == "SkeletonBody"){
          Debug.Log("Enter player");
          if(FindObjectOfType<PlayerController>().IsSkeleton && other.GetComponent<SkeletonBodyClass>() is StrongSkeleton){
                Debug.Log("Enter skeleton");
              _canBeMoved = true;
               CanBeMoved();
          }else{
              _canBeMoved = false;
               CanBeMoved();
          }

      }
  }

  

}
