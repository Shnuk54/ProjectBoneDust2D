using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float _delay;
    private Vector3 _startPos;
    private Rigidbody _rb;
    
    private void Start()
    {
        _startPos = gameObject.transform.position;
        _rb = gameObject.GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            _rb.isKinematic = false;
            StartCoroutine(ReturnDelay());
        }
    }
    private IEnumerator ReturnDelay(){
        yield return new WaitForSeconds(_delay);
        _rb.isKinematic = true;
        gameObject.transform.position = _startPos;
    }
}
