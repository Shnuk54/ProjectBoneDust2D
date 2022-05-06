using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PhonePositionController
{
    
    public static Vector3 PhonePosition(){
        return Input.acceleration;
    }

}
