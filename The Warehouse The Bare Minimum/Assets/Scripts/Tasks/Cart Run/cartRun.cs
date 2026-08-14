using System.Collections.Generic;
using UnityEngine;

public class cartRun : Task
{
    public Transform minSpawnPos,maxSpawnPos;
    public int cartAmount;
    public GameObject shoppingCart;
    public List<GameObject> cartsInHand = new List<GameObject>();
    void Update()
    {
        if(!isComplete && isInProgress)
        {
            
        }
    }
    public override void StartingTask()
    {
        Debug.Log("The cart run has begun");
        //Process of spawning the carts
        for (int c = 0; c < cartAmount; c++)
        {
            //uses the transform components of two objects to set the boundaries of the spawning locations | sets a random angle for the cart to spawn at
            Vector3 randomSpawnPosition = new Vector3(Random.Range(minSpawnPos.transform.position.x,maxSpawnPos.transform.position.x),0,Random.Range(minSpawnPos.transform.position.z,maxSpawnPos.transform.position.z));
            Vector3 randomRotation = new Vector3(0,Random.Range(0,361),0);

            //spawns the cart
            GameObject cart = Instantiate(shoppingCart,randomSpawnPosition,Quaternion.Euler(randomRotation));
            
            //adds cart to list
            cartsInHand.Add(cart);
        } 
        isInProgress = true;
        isComplete = false;
    }

    public override void EndingTask()
    {
        Debug.Log("The cart run has ended");
        //Destroy the carts
        for (int i=0;i < cartsInHand.Count;i++)
        {
            GameObject tempCart = cartsInHand[i];
            cartsInHand.Remove(tempCart);
            Destroy(tempCart);
        }

        isInProgress = false;
        isComplete = true;
    }
}
