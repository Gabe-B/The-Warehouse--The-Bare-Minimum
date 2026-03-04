using UnityEngine;

public class cartCollisions : MonoBehaviour
{
    public Rigidbody rb;
    public int cartCount;
    private int carHits;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("cart"))
        {
            Destroy(collision.gameObject);
            cartCount += 1;
            Debug.Log("You are carrying " + cartCount + " carts right now!");
        }
        if (collision.gameObject.CompareTag("car"))
        {
            Debug.Log("You've been hit by a car " + carHits);
            carHits += 1;
            if (carHits >= 3)
            {
                Debug.Log("You got your shit rocked and was dragged back inside.");
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cartCheck"))
        {
            Debug.Log("Checking cameras for carts");
        }
    }
    void FixedUpdate()
    {
        rb.linearDamping =  1 + (cartCount*.1f);
    }
}