using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CollactableItem : MonoBehaviour,ISaveable
{
[SerializeField] string itemName;
[SerializeField] Sprite icon;
public ItemParametrs parametrs;
[SerializeField] private bool _isUsed = false;

  private void Start()
  {
    SaveLoad.singleton.Load();
    if(_isUsed)Destroy(this.gameObject);
  }

  private void Collect(){
    PlayerItemsSystem.singleton.CollectItem(this);
    Destroy(this.gameObject);
  }

  private void OnTriggerEnter(Collider other)
  {
    if(other.tag == "Player" && !_isUsed){
      Collect();
      _isUsed = true;
      SaveLoad.singleton.Save();
      }
  }

   public  object CaptureState(){
      return new SaveData{
          isUsed = _isUsed
        };
    }
    public  void RestoreState(object state){
        var saveData = (SaveData)state;
        _isUsed = saveData.isUsed;
    }

     [System.Serializable]
    private struct SaveData
    {
        public bool isUsed;
    }

[System.Serializable]
 public class ItemParametrs
 {
   public string itemName; 
   public bool health;
   public int healthIncrease;
   public bool skeletonEndurance;
   public int skeletonEnduranceIncrease;
   public  bool dJumpHeight;
   public int dJumpHeightIncrease;
   public bool jumpHeight;
   public int jumpHeightIncrease;
 }


   
}

