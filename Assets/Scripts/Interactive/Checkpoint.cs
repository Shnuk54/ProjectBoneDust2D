using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Checkpoint : MonoBehaviour
{
    [SerializeField] string _checkpointState = "notActive";
     
    [SerializeField] private Light2D _light;
    [SerializeField] Color activeColor;
    [SerializeField] Color notActiveColor;
    [SerializeField] Color blockedActiveColor;
    [SerializeField] GameObject[] checkPointsList;

    private AnimatorState _animator;
    private void Start()
    {
       _animator =  this.GetComponent<AnimatorState>();
        checkPointsList = GameObject.FindGameObjectsWithTag("Checkpoint");
        AnimationState[] anims = new AnimationState[]{
           new AnimationState(AnimationPart.Body,"Activate"),
           new AnimationState(AnimationPart.Body,"Idle_Lamp")
        };
        _animator.AddAnimations(anims);
       DisableCheckpoint();
    }

    public void DisableCheckpoint()
    {
        _light.color = notActiveColor;
        _animator.PlayAnimation("Idle_Lamp");
    }
    public void EnableCheckpoint()
    {
        _light.color = activeColor; 
        _animator.PlayAnimation("Activate");
    }
    public string CheckpointState
    {
        set
        {
            _checkpointState = value;
        }
    }
    private void ActivateCheckpoint()
    {

        foreach (GameObject checkpoint in checkPointsList)
        {
            checkpoint.GetComponent<Checkpoint>().CheckpointState = "notActive";
            checkpoint.GetComponent<Checkpoint>().DisableCheckpoint();
        }
        _checkpointState = "isActive";
        EnableCheckpoint();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ActivateCheckpoint();
            SaveLoad.singleton.Save();
        }
    }



}
