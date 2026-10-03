using UnityEngine;

//ATTACHED TO CART PREFAB
public class cartSubmit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player; //player prefab
    public cartCollisions collect; // player prefab
    void Start()
    {
        //setting the reference to the script that houses the heldcarts list
        collect = player.GetComponent<cartCollisions>();
    }
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("collect"))
        {
            Debug.Log("Youve touched the collection zone");
            //collect.heldCarts.Remove(collect.heldCarts[collect.heldCarts.Count-1].gameObject);
            Destroy(this.gameObject);
        }
    }
}
// This is part of the cart prefab
//There is no reference to the cart itself 
//the only refernce is to the script for collecting 