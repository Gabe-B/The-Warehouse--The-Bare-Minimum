using UnityEngine;

public class boxManager : MonoBehaviour
{
    //Setting object references to variables
    public GameObject productContainer;
    public GameObject productBox;
    public GameObject boxZone;
    public GameObject shelf;
    private
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("player")&&(productBox.gameObject.tag == "dirty"))
        {
            //Changing box to clean state
            productBox.tag = "clean";
        }
    }
    void FixedUpdate()
    {
        if (productBox.tag == "clean")
        {
            cState();
        }
        if (productBox.tag == "dirty")
        {
            dState();
        }
    }
    void cState()
    {
        //Changing the local position of the box to the clean state
        productBox.transform.localPosition = new Vector3 (0,0,-.5f);
        //Disabling the product boxes collision box and hiding the product zone
        productContainer.GetComponent<BoxCollider>().enabled = false;
        boxZone.GetComponent<MeshRenderer>().enabled = false;
    }

    void dState()
    {
        //Changing the local position of the box to the clean state
        productBox.transform.localPosition = new Vector3 (0,0,-6.75f);
        //Enabling the product boxes collision box and displaying the product zone
        productContainer.GetComponent<BoxCollider>().enabled = true;
        boxZone.GetComponent<MeshRenderer>().enabled = true;
    }
}