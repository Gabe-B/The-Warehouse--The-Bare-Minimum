using UnityEngine;

public class carryOut : MonoBehaviour
{
    public Rigidbody rb;
    public InteractionUI interUI;
    //If product is in zone then its collected and the customer can take off
    void OnTriggerEnter(Collider other)
    {
        //finds the rigid body of the product
        GameObject interactionZone = other.transform.GetChild(0).gameObject;
        rb = other.gameObject.GetComponent<Rigidbody>();
        Debug.Log("The item going into the collection zone: "+interactionZone.name);
        // jrObject.gameObject.GetComponent<MeshRenderer>().enabled = true;
        if (other.gameObject.CompareTag("product"))
        {
            rb.isKinematic = true;
            other.transform.SetParent(transform, worldPositionStays: true);
            other.transform.localPosition = new Vector3 (0,0,0);
            interactionZone.gameObject.GetComponent<MeshRenderer>().enabled = false;
            interactionZone.gameObject.GetComponent<SphereCollider>().enabled = false;
            interUI.inHand = false;
        }
    }
    //Add a random chance of recieving a tip from a customer
}
//When walking an object into zone, instead of throwing, the objects inhand bool isnt turned off. This gives the player the ability to still "throw" the item when it is in the collection one