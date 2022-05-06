using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISphereCheckProvider 
{
   public bool CheckLayer(Transform pos,float Radius,LayerMask checkLayer);
    
}
