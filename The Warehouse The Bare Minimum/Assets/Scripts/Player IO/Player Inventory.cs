using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool HasItem => helditem != null;
    public Item HeldItem => helditem;

    private Item helditem;

    public bool TryTakeItem(Item item)
	{
        if (HasItem) return false;

        helditem = item;
        return true;
	}

    public Item RemoveItem()
	{
        Item item = helditem;
        helditem = null;

        return item;
	}
}
