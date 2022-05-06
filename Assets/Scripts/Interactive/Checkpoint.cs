using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] string _checkpointState = "notActive";
    private Material material;
    [SerializeField] Color activeColor;
    [SerializeField] Color notActiveColor;
    [SerializeField] Color blockedActiveColor;
    [SerializeField] GameObject[] checkPointsList;
    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        checkPointsList = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    public void DisableCheckpoint()
    {
        material.color = notActiveColor;
    }
    public void EnableCheckpoint()
    {
        material.color = activeColor; ;
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
