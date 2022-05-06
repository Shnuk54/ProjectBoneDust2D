using System.Collections;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField] GameObject[] path;

    [SerializeField] float speed;
    [SerializeField] float delay;

    private PlayerController player;
    private GameObject platform;
    private int pointID = 0;

    private void Start()
    {
        platform = gameObject;
        player = FindObjectOfType<PlayerController>();
        StartCoroutine(MovePlatform());
    }



    IEnumerator MovePlatform()
    {
        while (true)
        {
            if (platform.transform.position == path[pointID].transform.position && pointID != path.Length)
            {
                pointID += 1;
            }
            if (pointID == path.Length)
            {
                pointID = 0;
            }
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, path[pointID].transform.position, speed);
            yield return new WaitForSeconds(delay);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "PlayerBody" && other.tag == "Player" && !player.IsSkeleton)
        {
            player.PlayerRB.transform.SetParent(platform.transform, true);
        }
        if (other.tag == "SkeletonBody")
        {
            other.GetComponent<Rigidbody>().transform.SetParent(platform.transform, true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "PlayerBody" && other.tag == "Player" && !player.IsSkeleton)
        {
            player.PlayerRB.transform.SetParent(null, true);
        }
        if (other.tag == "SkeletonBody")
        {
            other.transform.SetParent(null, true);
        }

    }

}
