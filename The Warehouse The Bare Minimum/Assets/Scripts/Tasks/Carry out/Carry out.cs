using UnityEngine;

public class carryOut : MonoBehaviour
{
    public Rigidbody rb;
    //If product is in zone then its collected and the customer can take off
    void OnTriggerEnter(Collider other)
    {
        //finds the rigid body of the product
        rb = other.gameObject.GetComponent<Rigidbody>();
        if (other.gameObject.CompareTag("product"))
        {
            rb.isKinematic = true;
            other.transform.SetParent(transform, worldPositionStays: true);
            other.transform.localPosition = new Vector3 (0,0,0);
        }
    }
    //Add a random chance of recieving a tip from a customer
}
