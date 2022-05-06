using UnityEngine;
using UnityEngine.UI;

public class Leverage : MonoBehaviour,ISaveable
{

    [SerializeField] private PlayerController player;
    [SerializeField] private OpenableDoor door;
    [SerializeField] private bool _isActivated = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    private void OnMouseUp()
    {
         if (_isActivated == false && player.IsSkeleton)
            {
                _isActivated = true;
                door.OpenDoorByleverage();
                gameObject.transform.rotation = new Quaternion(0, 90, 0, 0);
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
