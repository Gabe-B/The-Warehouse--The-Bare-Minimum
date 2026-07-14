using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using Unity.Mathematics;
using NUnit.Framework;


public class InteractionUI : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    //Bool for interaction zone and holding check
    public bool inZone, inHand;
    private int placeHolder;
    public GameObject srObject, jrObject, player;
    public Rigidbody rb;
    public NewPlayerControls npc;
    public float timeStart, timeNow, timeEnd, timeDiff, strengthMulti, throwStrength, holdThreshold;
    public Transform holdParent;

    void Start()
    {
        inZone = inHand = false;
        srObject = jrObject = null;
        textMesh.text = null;
    }


    void OnTriggerStay(Collider other) //The player enters an interaction zone
    {
        if (other.gameObject.CompareTag("interUI")) //If the player is touching something and the object has the interUI tag
        {
            inZone = true;
            if (!inHand)
            {
                //Addressing the parent object of the interaction zone
                srObject = other.gameObject.transform.parent.gameObject;
                jrObject = other.gameObject;
                rb = srObject.gameObject.GetComponent<Rigidbody>();
            }
        }
    }
    void OnTriggerExit(Collider other) //The player leaves an interaction zone
    {
        if (other.gameObject.CompareTag("interUI"))
        {
            inZone = false;
        }
    }

    void Update()
    {
        // if (srObject.transform.parent != null && srObject.transform.parent.CompareTag("player")) //Keeps Object in same rotation as the player
        // {
        //     srObject.transform.rotation = Quaternion.Euler(player.transform.localEulerAngles);
        //     Debug.Log("You're currently holding " + (srObject.name) + " put it down to interact again.");
        // }
        if (inZone && npc.pi.actions.FindAction("Interact").WasPressedThisFrame()) //if you're in zone and you press e
        {
            pickUp();
        }

        //Throw mechanics
        if (inHand && npc.pi.actions.FindAction("Throw").WasPressedThisFrame()) //Set Time start
        {
            timeStart = Time.time;
            // putDown();
        }
        if (inHand && npc.pi.actions.FindAction("Throw").IsPressed()) //Gets active time
        {
            timeNow = Time.time - timeStart;
            textMesh.text = "Power: " + timeNow.ToString("F2");
        }
        if (inHand && npc.pi.actions.FindAction("Throw").WasReleasedThisFrame()) //Set time end
        {
            inHand = false;
            timeEnd = Time.time;
            strengthMulti = timeEnd - timeStart;
            textMesh.text = null;
            putDown();
            // timeDiff = timeEnd - timeStart;
            // if (timeDiff <= holdThreshold)
            // {
            //     strengthMulti = 1;
            // }
            // else
            // {
            //     strengthMulti = timeDiff;
            // }
        }

    }

    //Carrying actions
    void pickUp()
    {
        if (!inHand)
        {
            inHand = true;
            //Put whatever interaction is needed after this comment
            //Prevent being able to interect again while inHand the interactable
            jrObject.gameObject.GetComponent<MeshRenderer>().enabled = false;
            jrObject.gameObject.GetComponent<SphereCollider>().enabled = false;
            //turning objects physics off
            rb.isKinematic = true;
            inZone = false;
            //Pick up the object
            srObject.transform.SetParent(holdParent, worldPositionStays: true);
            srObject.transform.localPosition = new Vector3(0, 0, 0);
            srObject.transform.localRotation = new Quaternion (0,0,0,0);
        }
    }
    void putDown()
    {
        //Turning object physics on
        rb.isKinematic = false;
        rb.AddForce(player.transform.forward * (throwStrength * strengthMulti), ForceMode.Impulse); //get angle of player & add force 


        //Disassociate object from the player 
        rb = null;
        srObject.transform.parent = null;
        //Enable interection again after detaching
        jrObject.gameObject.GetComponent<MeshRenderer>().enabled = true;
        jrObject.gameObject.GetComponent<SphereCollider>().enabled = true;
    }
}
//Current Bugs needed to be fixed

//Bug: A player needs to walk into a each zone once per game in order to throw an item into it
//Why: trying to access the inhand variable on this script from the Carry out and recyclingTime and curbside scripts | Commenting out "interUI.inHand = false;" fixes part of the issue but the main issue still remains

//Bug: If a player walks into a recycle zone you cant pick anything up after   
//Why: The game might still thinks the player is holding the object (inHand = true) that was destroyed 