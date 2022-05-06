using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    [SerializeField] private float _forceMultyply;

   private void OnTriggerEnter2D(Collider2D other)
   {
       AddForce(other.gameObject.GetComponent<Rigidbody2D>());
   }
    private void AddForce(Rigidbody2D rb)
    {
            rb.AddForce (new Vector2(0,rb.velocity.y * -_forceMultyply),ForceMode2D.Impulse);
    }

}
