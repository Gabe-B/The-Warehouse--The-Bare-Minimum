using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using Unity.Mathematics;
using NUnit.Framework;


public class InteractionUI : MonoBehaviour
{
    public TextMeshProUGUI textMesh;// not needed delete this
    public GameObject interItem, interZone, player;
    public Rigidbody rb;
    public NewPlayerControls npc;
    public Transform holdParent;
    public bool inZone, inHand;
    private float timeStart, timeNow, timeEnd, strengthMulti;
    public float throwStrength; // can privatize some and remove some

    void Start()
    {
        inZone = inHand = false;
        interItem = interZone = null;
        textMesh.text = null;
    }


    void OnTriggerStay(Collider other) //The player enters an interaction zone
    {
        if (other.gameObject.CompareTag("interUI")) //If the player is touching something and the object has the interUI tag
        {
            inZone = true;
            if (!inHand)
            {
                //Getting References to objects
                interItem = other.gameObject.transform.parent.gameObject; // This is the actual object
                interZone = other.gameObject; // This is the interaction sphere around the actual object
                rb = interItem.gameObject.GetComponent<Rigidbody>(); // This is the rigidbody of the actual object
            }
        }
    }
    void OnTriggerExit(Collider other) //The player leaves an interaction zone
    {
        if (other.gameObject.CompareTag("interUI"))//if nothings in hand and you leave zone all references to interaction objects should be set to null but if you in two interaction zones at the same time it will cause issues
        {
            inZone = false;
        }
    }

    void Update()
    {
        // if (interItem.transform.parent != null && interItem.transform.parent.CompareTag("player")) //Keeps Object in same rotation as the player
        // {
        //     interItem.transform.rotation = Quaternion.Euler(player.transform.localEulerAngles);
        //     Debug.Log("You're currently holding " + (interItem.name) + " put it down to interact again.");
        // }
        if (inZone && npc.pi.actions.FindAction("Interact").WasPressedThisFrame()) //if you're in zone and you press e
        {
            pickUp();
        }

        //Throw mechanics
        if (inHand && npc.pi.actions.FindAction("Throw").WasPressedThisFrame()) //Set Time start
        {
            timeStart = Time.time; // Gets start of timer
        }
        if (inHand && npc.pi.actions.FindAction("Throw").IsPressed()) //Gets active time
        {
            timeNow = Time.time - timeStart; // Gets current time of the timer
            textMesh.text = "Power: " + timeNow.ToString("F2"); // Displays the strength of the throw
        }
        if (inHand && npc.pi.actions.FindAction("Throw").WasReleasedThisFrame()) //Set time end
        {
            inHand = false;
            timeEnd = Time.time; // Gets the end of the timer
            strengthMulti = timeEnd - timeStart; // Sets the strength multiplier
            textMesh.text = null; // Resets the text display to blank
            putDown();
        }
        if(!inZone && !inHand)
        {
            interItem = interZone = null;
            rb = null;
        }
    }

    //Carrying actions
    void pickUp()
    {
        if (!inHand)
        {
            inHand = true;
            //Prevent being able to interect again while inHand the interactable
            interZone.gameObject.GetComponent<MeshRenderer>().enabled = false;
            interZone.gameObject.GetComponent<SphereCollider>().enabled = false;

            //turning objects physics off
            rb.isKinematic = true;
            inZone = false;
            
            //Pick up the object
            interItem.transform.SetParent(holdParent, worldPositionStays: true);
            interItem.transform.localPosition = new Vector3(0, 0, 0);
            interItem.transform.localRotation = new Quaternion (0,0,0,0);
        }
    }
    void putDown()
    {
        //Turning object physics on
        rb.isKinematic = false;
        rb.AddForce(player.transform.forward * (throwStrength * strengthMulti), ForceMode.Impulse); //get angle of player & add force 


        //Disassociate object from the player 
        rb = null;
        interItem.transform.parent = null;//removes from any parent

        //Enable interection again after detaching
        interZone.gameObject.GetComponent<MeshRenderer>().enabled = true;
        interZone.gameObject.GetComponent<SphereCollider>().enabled = true;
    }
}
//Current Bugs needed to be fixed

//Bug: A player needs to walk into a each zone once per game in order to throw an item into it
//Why: trying to access the inhand variable on this script from the Carry out and recyclingTime and curbside scripts | Commenting out "interUI.inHand = false;" fixes part of the issue but the main issue still remains

//Bug: If a player walks into a recycle zone you cant pick anything up after   
//Why: The game might still thinks the player is holding the object (inHand = true) that was destroyed 