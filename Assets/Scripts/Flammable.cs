using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Flammable : MonoBehaviour,ISaveable
{
    [SerializeField] protected float _flameTime;
    [SerializeField] protected Material _fireShader;
    protected Vector3 _position;
    protected MeshRenderer _renderer;
    [SerializeField] protected bool _inFlame = false; 
    protected Material _curMat;
   [SerializeField] protected bool _destroyed;

    private void Awake()
    {
        _position = this.transform.position;
        _renderer = GetComponent<MeshRenderer>();
        _curMat = _renderer.sharedMaterial;

        
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if(_destroyed == true){
            Destroy(this.gameObject,0.5f);
            };
    }
    protected void ChangeMaterial(){
        _renderer.sharedMaterials = new Material[2];
        var newMat = _renderer.sharedMaterials;
        newMat[0] = _curMat;
        newMat[1] = _fireShader;
        _renderer.sharedMaterials = newMat;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Torch" && !_inFlame){
            _inFlame = true;
            StartCoroutine(Burn());
        }
    }

   protected IEnumerator Burn (){
        ChangeMaterial();
        yield return new WaitForSeconds(_flameTime);
        _destroyed = true;
        SaveLoad.singleton.Save();
        Destroy(this.gameObject);
    }

    public object CaptureState(){
             return new SaveData{
            destroyed = _destroyed
        };
    }
    public void RestoreState(object state){
            var saveData = (SaveData)state;
            _destroyed = saveData.destroyed;
    }

    [System.Serializable]
    private struct SaveData
    {
       public bool destroyed;
    }
}
