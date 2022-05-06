using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
[System.Serializable]
public class SaveLoad : MonoBehaviour
{
    public static SaveLoad singleton {get;private set;}
    [SerializeField] string saveName;
    [SerializeField] List<string> playerSaves;
     private string SavePath => $"{Application.persistentDataPath}/{saveName}.save";
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
        FindSaves();
    }
   

    [ContextMenu("Save")]
    public void Save(){
        var state = LoadFile();
        CaptureState(state);
        SaveFile(state);
    }

    [ContextMenu("Load")]
    public void Load(){
        var state = LoadFile();
        RestoreState(state);
    }

     public void SaveNew(){
        var state = LoadFile();
        CaptureState(state);
        SaveFile(state);
    }

    public List<string> GetSaves(){
         FindSaves();
         return playerSaves;
    }

     private void FindSaves(){
        playerSaves.Clear();
          try{
        string[] saves = Directory.GetFiles(Application.persistentDataPath,"*.save",SearchOption.TopDirectoryOnly);
             foreach(string save in saves){
        playerSaves.Add(Path.GetFileName(save));
         }
        }catch{
            Debug.Log("Cannot find file");
        }
    }

    public void LoadByName(string _saveName){
            saveName = _saveName;
            Load();
        }

    private Dictionary<string,object> LoadFile(){

        if(!File.Exists(SavePath)){
            return new Dictionary<string, object>();
        }
        using(FileStream stream = File.Open(SavePath,FileMode.Open)){
             var formatter = new BinaryFormatter();
            return (Dictionary<string,object>)formatter.Deserialize(stream);
        }
    }

    private void SaveFile(object state){
        using(var stream = File.Open(SavePath,FileMode.Create)){
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream,state);
            stream.Close();
        }
    }

    private void CaptureState(Dictionary<string,object> state){

        foreach(var saveable in FindObjectsOfType<SaveableEntity>()){
            state[saveable.Id] = saveable.CaptureState();
        }
    }

    private void RestoreState(Dictionary<string,object> state){

        foreach(var saveable in FindObjectsOfType<SaveableEntity>()){
           
           if(state.TryGetValue(saveable.Id, out object value)){
               saveable.RestoreState(value);
           }

        }
    }
}
