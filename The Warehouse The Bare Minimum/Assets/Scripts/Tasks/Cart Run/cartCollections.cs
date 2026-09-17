using System;
using UnityEngine;

public class cartCollections : MonoBehaviour
{
    [SerializeField] private cartCollisions collect;
    void Update()
    {
        Debug.Log(collect.heldCarts.Count);
    }
}