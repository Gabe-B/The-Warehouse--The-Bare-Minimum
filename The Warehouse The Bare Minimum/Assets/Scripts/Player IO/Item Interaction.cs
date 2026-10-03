using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInteraction : MonoBehaviour
{
    public PlayerInput pi;

    public LayerMask mask;
    public float interactCheckDistance = 10.0f;

    public bool isAtMaxCarryCapacity = false;
    int maxCarryAmount = 5;
    int currentCarryAmount = 0;

    public Transform ItemHeldPosition;
    public Transform InteractCheckPosition;
    public Transform playerCameraPivot;
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
        
        bool isLookingAtInteractable = Physics.BoxCast(InteractCheckPosition.position, new Vector3(6, 10, 1), InteractCheckPosition.forward, out hit, Quaternion.identity, interactCheckDistance, mask);

        if(isLookingAtInteractable)
		{
            if (hit.collider.gameObject.GetComponent<ShelfSlot>())
            {
                s_targetShelfSlot = hit.collider.gameObject.GetComponent<ShelfSlot>();
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.yellow;

                if (pi.actions.FindAction("Interact").WasPressedThisFrame() && !isAtMaxCarryCapacity)
                {
                    HeldItems.Add(s_targetShelfSlot.TryPickupItemFromShelf(s_targetShelfSlot.Item));
                    currentCarryAmount++;
                }
                else if (pi.actions.FindAction("Interact").WasPressedThisFrame() && isAtMaxCarryCapacity)
                {
                    s_targetShelfSlot.TryPlaceItemOntoShelf(HeldItems[HeldItems.Count - 1]); //Always placing the top most item in the list
                }
            }
            else if (hit.collider.gameObject.GetComponent<CarryableItem>())
            {
                c_targetCarryableItem = hit.collider.gameObject.GetComponent<CarryableItem>();
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.yellow;

                if (pi.actions.FindAction("Interact").WasPressedThisFrame() && !isAtMaxCarryCapacity)
                {
                    HeldItems.Add(TryPickupItem(c_targetCarryableItem));
                }
            }
        }
        else
		{
            c_targetCarryableItem.gameObject.GetComponent<Renderer>().material.color = Color.white;
            s_targetShelfSlot.gameObject.GetComponent<Renderer>().material.color = Color.white;
            c_targetCarryableItem = null;
            s_targetShelfSlot = null;
        }
    }

	public CarryableItem TryPickupItem (CarryableItem item)
	{
        item.gameObject.GetComponent<Rigidbody>().useGravity = false;
        item.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        item.gameObject.GetComponent<BoxCollider>().enabled = false;

        item.transform.parent = ItemHeldPosition;
        item.transform.position = ItemHeldPosition.position;

        currentCarryAmount++;
        return item;
	}
}