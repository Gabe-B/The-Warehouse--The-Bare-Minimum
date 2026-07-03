using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class recyclingTime : MonoBehaviour
{
    public Rigidbody rb;
    public InteractionUI interUI;
    public TextMeshProUGUI textMesh;
    public int recycleTotal,recycleLeft; 
    //If product is in zone then its collected and the customer can take off
    void OnTriggerEnter(Collider other)
    {
        //finds the rigid body of the product
        GameObject interactionZone = other.transform.GetChild(0).gameObject;
        rb = other.gameObject.GetComponent<Rigidbody>();

        if (other.gameObject.CompareTag("trash"))
        {
            rb.isKinematic = true;
            other.transform.SetParent(transform, worldPositionStays: true);
            interactionZone.gameObject.GetComponent<MeshRenderer>().enabled = false;
            interactionZone.gameObject.GetComponent<SphereCollider>().enabled = false;
            
            Destroy(other.gameObject);
            //Has no reference but somehow works 
            interUI.inHand = false;
        }
    }
}

//Trash isnt being destroyed when thrown into the recycle zone (reason: unknown Solution: remove it temporarily)
//When walking an object into zone, instead of throwing, the objects inhand bool isnt turned off. This gives the player the ability to still "throw" the item when it is in the collection one