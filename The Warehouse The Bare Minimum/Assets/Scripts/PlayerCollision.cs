using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name == "Warehouse"){
        Debug.Log ("We are in the warehouse");
        }
    }
}
