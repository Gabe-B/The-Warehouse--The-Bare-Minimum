using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class recyclingTime : MonoBehaviour
{
    public Rigidbody rb;
    public InteractionUI interUI;
    public TextMeshProUGUI textMesh;
    public int recycleTotal, recycleLeft;

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

        if (other.gameObject.CompareTag("trash"))
        {
            interUI.inHand = false;
            rb.isKinematic = true;
            other.transform.SetParent(transform, worldPositionStays: true);
            interactionZone.gameObject.GetComponent<MeshRenderer>().enabled = false;
            interactionZone.gameObject.GetComponent<SphereCollider>().enabled = false;
            Destroy(other.gameObject);
        }
    }
}