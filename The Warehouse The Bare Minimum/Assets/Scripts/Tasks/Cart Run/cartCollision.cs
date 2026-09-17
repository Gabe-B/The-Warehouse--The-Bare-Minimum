using System.Collections.Generic;
using UnityEngine;

public class cartCollisions : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject cartLocation;
    private int cartCount,newCartPos;
    public List<GameObject> heldCarts = new List<GameObject>();
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("cart") & !heldCarts.Contains(collision.gameObject))
        {
            collision.transform.SetParent(cartLocation.transform);
            heldCarts.Add(collision.gameObject);
            cartCount = heldCarts.Count;
            newCartPos = 5 * cartCount;
            collision.transform.localPosition = new Vector3(0, 0, newCartPos);
            collision.transform.localRotation = new Quaternion(0, 0, 0, 0);
            Debug.Log("cart added to inventory");
        }
        // if(collision.gameObject.CompareTag("collect"))
        // {
        //     Debug.Log("Youve touched the collection zone");
        //     Destroy(heldCarts[heldCarts.Count].gameObject);
        //     heldCarts.Remove(heldCarts[heldCarts.Count].gameObject);
        // }
    }
}