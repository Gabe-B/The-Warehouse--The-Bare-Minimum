using UnityEngine;

public class curbside : Task
{
    private Rigidbody rb;
    private InteractionUI interUI;

    // public override void StartingTask()
    // {
    //     Debug.Log("The curbside has begun");
    // }

    // public override void EndingTask()
    // {
    //     Debug.Log("The curbside has ended");
    // }

    //If product is in zone then its collected and the customer can take off
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            interUI = other.GetComponent<InteractionUI>();
        }
        //finds the rigid body of the product
        GameObject interactionZone = other.transform.GetChild(0).gameObject;
        rb = other.gameObject.GetComponent<Rigidbody>();
        Debug.Log("The item going into the collection zone: " + interactionZone.name);
        // jrObject.gameObject.GetComponent<MeshRenderer>().enabled = true;
        if (other.gameObject.CompareTag("curbside"))
        {
            interUI.inHand = false;
            rb.isKinematic = true;
            other.transform.SetParent(transform, worldPositionStays: true);
            other.transform.localPosition = new Vector3(0, 0, 0);
            interactionZone.gameObject.GetComponent<MeshRenderer>().enabled = false;
            interactionZone.gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
    //Add a random chance of recieving a tip from a customer
}