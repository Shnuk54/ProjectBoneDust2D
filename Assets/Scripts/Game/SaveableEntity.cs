using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveableEntity : MonoBehaviour
{
   [SerializeField] private string id = string.Empty;
   public string Id => id;

   [ContextMenu("GenerateID")]
    private void GenerateID() => id = Guid.NewGuid().ToString();

    private void Awake()
    {
        if(id.Length == 0){
            GenerateID();
        }
    }
    public object CaptureState(){

        var state = new Dictionary<string,object>();

        foreach(var saveable in GetComponents<ISaveable>()){

            state[saveable.GetType().ToString()] = saveable.CaptureState();
        }
        return state;
    }

    public void RestoreState(object state){

        var stateDictionary =  (Dictionary<string,object>)state;

        foreach(var saveable in GetComponents<ISaveable>()){

            string typeName = saveable.GetType().ToString();

            if(stateDictionary.TryGetValue(typeName, out object value)){

                saveable.RestoreState(value);

            }
        }
    }
}
