using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonParts : MonoBehaviour,ISaveable
{
   [SerializeField] private bool _isUsed;
   public Skeleton  skeleton;

private void Start()

{   SaveLoad.singleton.Load();
    if(_isUsed)Destroy(this.gameObject);
}

    private void Collect(){
     PlayerItemsSystem.singleton.CollectSkeletonPart(this);
    Destroy(this.gameObject);
  }
    private void OnTriggerEnter2D(Collider2D other)
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
   public class Skeleton{
       public int skeletonID;
       public string skeletonName;
   }
}
