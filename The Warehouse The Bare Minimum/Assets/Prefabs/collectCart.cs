using UnityEngine;

public class collectCart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    public cartCollisions collect;
    void Start()
    {
        collect = player.GetComponent<cartCollisions>();
    }
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("collect"))
        {
            Debug.Log("Youve touched the collection zone");
            collect.heldCarts.Remove(collect.heldCarts[collect.heldCarts.Count-1].gameObject);
            Destroy(collect.heldCarts[collect.heldCarts.Count-1].gameObject);
        }
    }
}
