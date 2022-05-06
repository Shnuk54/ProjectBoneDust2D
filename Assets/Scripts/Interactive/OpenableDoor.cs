using UnityEngine;

public class OpenableDoor : MonoBehaviour
{

    [SerializeField] GameObject[] keys;
    private int _activeKeys = 0;
    private int _notActiveKeys = 0;
    private bool _isOpen;
    
    public void OpenDoorByPlate()
    {
        _activeKeys = 0;
        _notActiveKeys = 0;

        foreach (GameObject plate in keys)
        {
            if (plate.GetComponent<PressurePlate>().isPressed == false)
            {
                _notActiveKeys += 1;
            }
            if (plate.GetComponent<PressurePlate>().isPressed == true)
            {
                _activeKeys += 1;
            }
        }
        if (_activeKeys == keys.Length)
        {
            _isOpen = true;
            gameObject.transform.position = new Vector3(0, 0, 0);
        }
    }

    public void OpenDoorByleverage()
    {
        _activeKeys = 0;
        _notActiveKeys = 0;

        foreach (GameObject leverage in keys)
        {
            if (leverage.GetComponent<Leverage>().isActivated == false)
            {
                _notActiveKeys += 1;
            }
            if (leverage.GetComponent<Leverage>().isActivated == true)
            {
                _activeKeys += 1;
            }
        }
        if (_activeKeys == keys.Length)
        {
            _isOpen = true;
            gameObject.transform.position = new Vector3(0, 0, 0);
        }
    }

}
