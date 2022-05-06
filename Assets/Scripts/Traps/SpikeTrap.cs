using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SpikeTrap : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<IAlive>() == null)return;
        other.GetComponent<IAlive>().TakeDamage(_damage);
    }   
   
}
