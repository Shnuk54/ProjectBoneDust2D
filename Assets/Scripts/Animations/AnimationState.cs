using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AnimationPart{
    Body,Legs,Arms
}
public class AnimationState
{
   public AnimationPart animPart{get;private set;}
   public string animName;
   public bool isActive;

   public AnimationState(AnimationPart part, string name){
       this.animName = name;
       this.animPart = part;
   }
}
