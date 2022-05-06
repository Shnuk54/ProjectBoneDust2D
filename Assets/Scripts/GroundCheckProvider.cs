using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckProvider : MonoBehaviour,ISphereCheckProvider
{  
     public bool CheckLayer(Transform pos,float Radius,LayerMask checkLayer){
        bool grounded = Physics2D.OverlapCircle(pos.position, Radius, checkLayer);
        return grounded;
     }
}
