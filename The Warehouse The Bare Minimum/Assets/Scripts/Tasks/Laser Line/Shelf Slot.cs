using UnityEngine;

public class ShelfSlot : MonoBehaviour, IInteractable
{
    [Header("Item Placement")]
    [SerializeField] private Transform itemAnchor;

    private Item currentItem;

    public bool IsOccupied => currentItem != null;
    public Item CurrentItem => currentItem;

    public bool CanInteract(ItemInteraction player)
    {
        // Player is holding something.
        // They can interact if this slot is empty.
        if (player.Inventory.HasItem)
        {
            return !IsOccupied;
        }

        // Player isn't holding anything.
        // They can interact if this slot contains an item.
        return IsOccupied;
    }

    public void Interact(ItemInteraction player)
    {
        if (player.Inventory.HasItem)
        {
            player.PlaceIntoSlot(this);
        }
        else if (IsOccupied)
        {
            player.PickUpFromSlot(this);
        }
    }

    public string GetInteractionPrompt(ItemInteraction player)
    {
        if (player.Inventory.HasItem)
        {
            return IsOccupied ? "" : "Place";
        }

        return IsOccupied ? "Pick Up" : "";
    }

    public bool TryPlaceItem(Item item)
    {
        if (IsOccupied)
            return false;

        currentItem = item;

        item.transform.SetParent(itemAnchor);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        return true;
    }

    public Item TakeItem()
    {
        if (!IsOccupied)
            return null;

        Item item = currentItem;
        currentItem = null;

        item.transform.SetParent(null);

        return item;
    }
}
