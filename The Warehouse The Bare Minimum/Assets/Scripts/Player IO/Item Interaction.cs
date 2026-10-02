using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInteraction : MonoBehaviour
{
    public PlayerInput pi;

    public GameObject playerCamera;
    public LayerMask mask;
    public float interactCheckDistance = 10.0f;

    public bool isAtMaxCarryCapacity = false;
    int maxCarryAmount = 5;
    int currentCarryAmount = 0;

    public Transform ItemHeldPosition;
    public List<CarryableItem> HeldItems;

    ShelfSlot s_targetShelfSlot;
    CarryableItem c_targetCarryableItem;
    RaycastHit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pi = gameObject.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCarryAmount == maxCarryAmount)
		{
            isAtMaxCarryCapacity = true;
		}
        
        Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactCheckDistance, mask);

        s_targetShelfSlot = hit.transform.gameObject.GetComponent<ShelfSlot>();
        c_targetCarryableItem = hit.transform.gameObject.GetComponent<CarryableItem>();

        if (s_targetShelfSlot != null)
		{
            if (pi.actions.FindAction("Interact").WasPressedThisFrame() && !isAtMaxCarryCapacity)
            {
                HeldItems.Add(s_targetShelfSlot.TryPickupItemFromShelf(s_targetShelfSlot.Item));
                currentCarryAmount++;
            }
            else if (pi.actions.FindAction("Interact").WasPressedThisFrame() && isAtMaxCarryCapacity)
			{
                s_targetShelfSlot.TryPlaceItemOntoShelf(HeldItems[HeldItems.Count-1]); //Always placing the top most item in the list
            }
        }
        else if (c_targetCarryableItem != null)
		{
            if(pi.actions.FindAction("Interact").WasPressedThisFrame())
			{
				try
				{
                    HeldItems.Add(TryPickupItem(c_targetCarryableItem));
                }
				catch (Exception e)
				{
                    Debug.Log("Already carrying too much!!");
				}
			}
		}
    }

	public CarryableItem TryPickupItem (CarryableItem item)
	{
        if (isAtMaxCarryCapacity) return null;

        item.transform.parent = ItemHeldPosition;
        currentCarryAmount++;
        return item;
	}
}
