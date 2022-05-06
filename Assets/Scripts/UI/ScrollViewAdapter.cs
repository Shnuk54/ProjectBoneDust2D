using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using  TMPro;
using System.Collections.Generic;


public class ScrollViewAdapter : MonoBehaviour {

    [SerializeField] RectTransform contentPref;
    [SerializeField] RectTransform content;
    private List<string> saves;

[ContextMenu("Update")]
    public void UpdateItems ()
    {   
        saves = SaveLoad.singleton.GetSaves();
        int modelsCount = 0;
        int.TryParse(saves.Count.ToString(), out modelsCount);
        StartCoroutine(GetItems(modelsCount, results => OnReceivedModels(results)));
    }
    private void Start()
    {
        UpdateItems();
    }

    void OnReceivedModels (SaveModel[] models)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var model in models)
        {
            var instance = GameObject.Instantiate(contentPref.gameObject) as GameObject;
            instance.transform.SetParent(content, false);
            InitializeItemView(instance, model);
        }
    }

    void InitializeItemView (GameObject viewGameObject, SaveModel model)
    {
        TestItemView view = new TestItemView(viewGameObject.transform);
        view.textName.text = model.textName;

         view.btnSave.onClick.AddListener(
            ()=> 
            {
                SaveLoad.singleton.LoadByName(view.textName.text);
                UIController.EnableLoadMenu(false);
            }
        );
       
    }

    IEnumerator GetItems (int count, System.Action<SaveModel[]> callback)
    {
        yield return new WaitForSecondsRealtime(1f);
        var results = new SaveModel[count];
        for (int i = 0; i < count; i++)
        {
            results[i] = new SaveModel();
            string saveName = saves[i];
            int pos = saveName.LastIndexOf('.');
            results[i].textName = saveName.Substring(0,pos);
           
        }

        callback(results);
    }

    public class TestItemView
    {
        public TextMeshProUGUI textName;
        public Button btnSave;
     

        public TestItemView (Transform rootView)
        {
            textName = rootView.Find("textName").GetComponent<TextMeshProUGUI>();
            btnSave = rootView.Find("btnLoad").GetComponent<Button>();
        }
    }

    public class SaveModel
    {
        public string textName;
        
    }
}

