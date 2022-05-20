using UnityEngine;
using UnityEngine.UI;

public class Leverage : MonoBehaviour,ISaveable,IUseble
{

    private PlayerController _player;
    [SerializeField] private OpenableDoor door;
    [SerializeField] private bool _isActivated = false;
    [SerializeField] GameObject _leverage;
    [SerializeField] float _minDistanceToUse = 10f;

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
    }
    private void OnMouseUp()
    {
         Use();
    }

    public void Use(){
        if (_isActivated == false && _player.IsSkeleton && Vector3.Distance(_player.transform.position,this.transform.position)<= _minDistanceToUse)
            {
                _isActivated = true;
                door.OpenDoorByleverage();
                _leverage.transform.rotation = new Quaternion(0, 0, 65, 0);
            }
    }
    public bool isActivated
    {
        get
        {
            return _isActivated;
        }
    }

    public object CaptureState(){
             return new SaveData{
          activated = _isActivated
        };
    }
    
    public void RestoreState(object state){
            var saveData = (SaveData)state;
            _isActivated = saveData.activated;
    }

    [System.Serializable]
    private struct SaveData
    {
       public bool activated;
    }
}
