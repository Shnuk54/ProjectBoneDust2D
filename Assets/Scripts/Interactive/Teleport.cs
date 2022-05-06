using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] float teleportDelay = 1f;
    [SerializeField]  bool canTeleport = true;
    [SerializeField] GameObject telExit;
    private Teleport _telExit;
     WaitForSeconds seconds;
    void Awake() {
        _telExit = telExit.GetComponent<Teleport>();
    }

    void Start() {
        seconds = new WaitForSeconds(teleportDelay);
    }

    public bool CanTeleport{
        set{
            canTeleport = value;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(canTeleport == false) return;
        if(other.GetComponent<Rigidbody2D>() == null)return;
        var rb = other.GetComponent<Rigidbody2D>();
        StartCoroutine(Teleportation(rb));
    }


    public IEnumerator Teleportation(Rigidbody2D rb)
    {   rb.velocity = new Vector2(rb.velocity.x,-rb.velocity.y);
        rb.transform.position = telExit.transform.position;
        _telExit.canTeleport = false;
        yield return seconds;
        _telExit.canTeleport = true;
    }

}