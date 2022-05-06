using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemsSystem : MonoBehaviour,ISaveable
{
    [SerializeField] List<CollactableItem.ItemParametrs> items;
    [SerializeField] List<SkeletonParts.Skeleton> _skeletons;
    [SerializeField] int itemsCount;
    public static PlayerItemsSystem singleton;
    

    private void Awake()
    {
         if(!singleton){
            singleton = this;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SaveLoad.singleton.Load();
        SetupItemsStats();
    }

    private void SetupItemsStats(){
        foreach(CollactableItem.ItemParametrs item in items){
            PlayerStats.IncreaseHealth(item.healthIncrease);
        }
    }

    public List<SkeletonParts.Skeleton> GetSkeletons(){
       return _skeletons;
    }
    public int GetSkeletonsCount(){
        if(_skeletons != null){
            return _skeletons.ToArray().Length;
        }else
        {
            return 0;
        }
        
    }
    public void CollectItem(CollactableItem item){
        items.Add(item.parametrs);
        SaveLoad.singleton.Save();
        PlayerStats.IncreaseHealth(item.parametrs.healthIncrease);
        FindObjectOfType<PlayerController>().SetupPlayer();
    }
    public void CollectSkeletonPart(SkeletonParts part){
        List<SkeletonParts.Skeleton> skeletonsParts = new List<SkeletonParts.Skeleton>();
        skeletonsParts = _skeletons;
        skeletonsParts.Add(part.skeleton);
        _skeletons = skeletonsParts;
        SaveLoad.singleton.Save();
    }
      public  object CaptureState(){
             return new SaveData{
                 items = items ,
                 skeletons = _skeletons
        };
    }
    public  void RestoreState(object state){
        var saveData = (SaveData)state;
        items = saveData.items;
        _skeletons = saveData.skeletons;
    }

    [System.Serializable]
    private struct SaveData
    {
        public List<CollactableItem.ItemParametrs> items;
        public List<SkeletonParts.Skeleton> skeletons;
    }
}
