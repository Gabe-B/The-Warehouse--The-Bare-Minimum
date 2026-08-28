using UnityEngine;

public class cartCollisions : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject cartLocation;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("cart"))
        {
            collision.transform.SetParent(cartLocation.transform);
            collision.transform.localPosition = new Vector3 (0,0,5);
        }
    }
}