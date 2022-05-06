using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using  TMPro;
using System.Collections.Generic;


public class ScrollViewAdapterInventory : MonoBehaviour {

    [SerializeField] RectTransform contentPref;
    [SerializeField] RectTransform content;
    private List<SkeletonParts.Skeleton> skeletons;
    private Object[] skeletonsGO;

[ContextMenu("Update")]
    public void UpdateItems ()
    {   
        skeletons = PlayerItemsSystem.singleton.GetSkeletons();
        skeletonsGO = Resources.LoadAll("SkeletonsPrefabs",typeof(GameObject));
        Debug.Log(skeletonsGO[0].name);
        
        int modelsCount = 0;
        int.TryParse(skeletons.Count.ToString(), out modelsCount);
        StartCoroutine(GetItems(modelsCount, results => OnReceivedModels(results)));
    }
    private void Start()
    {
        UpdateItems();
        FindObjectOfType<MenuManager>().Pause();
    }

    void OnReceivedModels (SkeletonCardModel[] models)
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

    void InitializeItemView (GameObject viewGameObject, SkeletonCardModel model)
    {
        TestItemView view = new TestItemView(viewGameObject.transform);
        view.textInfo.text = model.textInfo;
        view.skeletonID = model.skeletonID;
        view.btnUse.onClick.AddListener(
            ()=> 
            {
                int skeletonID = 0;
                string prefName = "";
                foreach(GameObject skeleton in skeletonsGO){
                    SkeletonBodyClass _skeletonClass = skeleton.GetComponent<SkeletonBodyClass>();
                    if(_skeletonClass != null &&_skeletonClass.skeletonID == view.skeletonID){
                    skeletonID = view.skeletonID;
                     prefName = skeleton.name;
                    }
                }
                FindObjectOfType<SkeletonSpawner>().UseBonePill(prefName);
                FindObjectOfType<MenuManager>().Continue();
               Destroy(GameObject.FindGameObjectWithTag("SkeletonsMenu"));

            }
        );

        
        
    }

    IEnumerator GetItems (int count, System.Action<SkeletonCardModel[]> callback)
    {
        yield return new WaitForSecondsRealtime(1f);
        var results = new SkeletonCardModel[count];
        for (int i = 0; i < count; i++)
        {
            results[i] = new SkeletonCardModel();
            string skeletonName = skeletons[i].skeletonName;

            results[i].textInfo = skeletonName;
            results[i].skeletonID = skeletons[i].skeletonID;
           
        }

        callback(results);
    }

    public class TestItemView
    {
        public TextMeshProUGUI textInfo;
        public Button btnUse;
        public int skeletonID;
        public TestItemView (Transform rootView)
        {
            textInfo = rootView.Find("SkeletonInfo").GetComponent<TextMeshProUGUI>();
            btnUse = rootView.Find("btnUse").GetComponent<Button>();
        }
    }

    public class SkeletonCardModel
    {
        public string textInfo;
        public int skeletonID;
        
    }
}

