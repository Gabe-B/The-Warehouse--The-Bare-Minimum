using UnityEngine;

public class ShelfSlot : MonoBehaviour
{
	public bool currentlyOccupied = false;
	public CarryableItem Item;

    public void TryPlaceItemOntoShelf (CarryableItem item)
	{
		if (currentlyOccupied) return;

		item.transform.parent = null;
		item.transform.position = transform.position;

		currentlyOccupied = true;
		Item = item;
	}

	public CarryableItem TryPickupItemFromShelf (CarryableItem item)
	{
		item.transform.parent = null;
		currentlyOccupied = false;

		CarryableItem i = Item;
		Item = null;
		return i;
	}
}
