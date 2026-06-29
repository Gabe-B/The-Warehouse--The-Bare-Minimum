using UnityEngine;
using TMPro;
using UnityEngine.Rendering;
using Unity.Mathematics;


public class InteractionUI : MonoBehaviour
{
    public TextMeshProUGUI textMesh, xtext, ytext, ztext;

    //Bool for interaction zone and holding check
    public bool inZone, inHand;
    private int placeHolder;
    public int throwStrength;
    public GameObject srObject, jrObject, player;
    public Rigidbody rb;

    void Start()
    {
        inZone = inHand = false;
        srObject = jrObject = null;
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
    //The player leaves an interaction zone
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("interUI"))
        {
            inZone = false;
        }
    }

    void Update()
    {

        if (inHand) //Keeps Object in same rotation as the player
        {
            srObject.transform.rotation = Quaternion.Euler(player.transform.localEulerAngles);
        }
        if (inZone && Input.GetKeyDown("e")) //if you're in zone and you press e
        {
            pickUp();
        }
        if (inHand && Input.GetKeyDown("q")) //Throw Object
        {
            putDown();
        }
        if (srObject.transform.parent != null)//Checking to see if an item is being held 
        {
            Debug.Log("You're currently inHand something, put it down to interact again.");
        }
    }

    //Carrying actions
    void pickUp()
    {
        if (!inHand)
        {
            inHand = true;
            //Put whatever interaction is needed after this comment
            placeHolder += 1;
            textMesh.text = placeHolder.ToString();

            //Prevent being able to interect again while inHand the interactable
            jrObject.gameObject.GetComponent<MeshRenderer>().enabled = false;
            jrObject.gameObject.GetComponent<SphereCollider>().enabled = false;
            rb.isKinematic = true;
            inZone = false;

            //Pick up the object
            srObject.transform.SetParent(transform, worldPositionStays: true);
            srObject.transform.localPosition = new Vector3(0, 10, 0);
        }
    }
    void putDown()
    {
        rb.isKinematic = false;
        rb.AddForce(player.transform.forward * throwStrength, ForceMode.Impulse); //get angle of player & add force 
        //Disassociate object from the player 
        inHand = false;
        rb = null;
        srObject.transform.parent = null;
        //Enable interection again after detaching
        jrObject.gameObject.GetComponent<MeshRenderer>().enabled = true;
        jrObject.gameObject.GetComponent<SphereCollider>().enabled = true;
    }
}
// check compare tag of srObject to get the type of interaction