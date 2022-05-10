using UnityEngine;
using UnityEngine.UI;


public static class PlayerStats 
{
    // Player 
   [SerializeField] public static int healthPoint{get;private set;} = 100;
   [SerializeField] public static int armorPoint{get;private set;} = 10;
   [SerializeField] public static int damagePlayer{get;private set;} = 1;
   [SerializeField] public static float gravityPlayer{get;private set;} = 1f;
   [SerializeField] public static float maxForce{get;private set;} = 185f;
   [SerializeField] public static float forceMultiplier{get;private set;} = 3;
   [SerializeField] public static float doubleJumpForce{get;private set;} = 100f;

    // Skeleton
   [SerializeField] public static float speedSkeleton{get;private set;} = 4f;
   [SerializeField] public static float endurenceSkeleton{get;private set;} = 100f;

    [SerializeField] public static int itemsFound{get;private set;}

    public static void IncreaseHealth(int value){
        healthPoint += value;
        Debug.Log("Health add:"+ value);
    }
   
}