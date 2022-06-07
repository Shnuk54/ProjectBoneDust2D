using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform targetTransform;
    public Vector3 targetPos = Vector3.zero;
    public bool lookForward = false;
    [SerializeField] float smoothSpeed = 0.125f;
    [SerializeField] public Vector3 offset;
    private Vector3 _standartOffset;
    private Transform _standartTarget;

    private void Start()
    {
        _standartOffset = offset;
        targetTransform = FindObjectOfType<PlayerController>().GetComponent<Transform>();
        _standartTarget = targetTransform;
        DontDestroyOnLoad(this);
    }
    void FixedUpdate()
    {  
        FollowTarget();
    }
    private void FollowTarget(){
        Vector3 desiredPosition = Vector3.zero;
        Vector3 smoothedPosition = Vector3.zero;
    if(lookForward){
         desiredPosition = targetPos + offset;
         smoothedPosition = Vector3.Lerp(targetPos, desiredPosition, smoothSpeed/2);
                    }
    else{
       desiredPosition = targetTransform.position + offset;
       smoothedPosition  = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
        transform.position = smoothedPosition;
    }
    public void Reset(){
        offset = _standartOffset;
        targetTransform = _standartTarget;
        lookForward = false;
    }

}