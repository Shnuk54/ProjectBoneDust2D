using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private float _massToOpen;
    [SerializeField] private OpenableDoor _door;
    private bool _isPressed = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            if (other.GetComponent<Rigidbody>().mass >= _massToOpen)
            {
                _isPressed = true;
                _door.OpenDoorByPlate();
            }
            else
            {
                _isPressed = false;
            }  
        }
    }

    public bool isPressed
    {
        get
        {
            return _isPressed;
        }
    }

}
