using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class interactionScript : MonoBehaviour
{
    public GameObject interItem, interZone, player, tempItem;
    public Rigidbody rb;
    public NewPlayerControls npc;
    public Transform holdParent;
    public bool inZone, inHand;
    public List<GameObject> heldItems = new List<GameObject>();
    public int spaceMulti, throwStrength;
    void Start()
    {
        interZone = null;
    }

    //Getting references to gameobjects and setting bools
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("interUI"))
        {
            interItem = other.gameObject.transform.parent.gameObject;
            interZone = other.gameObject;
            rb = interItem.gameObject.GetComponent<Rigidbody>();
            inZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("interUI"))
        {
            inZone = false;
        }
    }

    //Checking for inputs and resetting object references
    void Update()
    {
        if (npc.pi.actions.FindAction("Interact").WasPressedThisFrame())
        {
            pickUp();
        }
        if (npc.pi.actions.FindAction("Throw").WasPressedThisFrame())
        {
            putDown();
        }
        if (!inHand && !inZone)
        {
            interItem = interZone = null;
            rb = null;
        }
        if (heldItems.Count > 0)
        {
            inHand = true;
        }
    }

    //Interaction logic
    void pickUp()//On pickup inzone isnt zero to inactive because if you are in two zones you would have to leave the second zone and go back into it in order to pick it up
    {
        if (inZone)
        {
            heldItems.Add(interItem);
            interItem.transform.SetParent(holdParent, worldPositionStays: true);
            //interItem.transform.localPosition = new Vector3(0, 0, 0);
            interItem.transform.localPosition = new Vector3(0, (spaceMulti * heldItems.Count) - spaceMulti, 0);
            interItem.transform.localRotation = new Quaternion(0, 0, 0, 0);
            rb.isKinematic = true;

            interZone.gameObject.GetComponent<MeshRenderer>().enabled = false;
            interZone.gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
    void putDown()
    {
        int lastItem = heldItems.Count - 1;
        tempItem = heldItems[lastItem];
        rb.isKinematic = false;
        rb = tempItem.gameObject.GetComponent<Rigidbody>();
        heldItems.Remove(heldItems[lastItem]);
        tempItem.transform.parent = null;

        rb.AddForce(player.transform.forward * (throwStrength), ForceMode.Impulse);

        if (lastItem < 0)
        {
            inHand = false;
            Debug.Log("You aren't holding anything");
        }
    }
}