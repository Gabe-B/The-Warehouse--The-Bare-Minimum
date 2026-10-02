using UnityEngine;
using UnityEngine.InputSystem;

public class ItemInteraction : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 3f;

    [Header("Inventory")]
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private Transform itemHand;

    public PlayerInventory Inventory => inventory;
    public Transform ItemHand => itemHand;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactionDistance))
        {
            IInteractable interactable =
                hit.collider.GetComponentInParent<IInteractable>();

            if (interactable == null)
                return;

            if (!interactable.CanInteract(this))
                return;

            interactable.Interact(this);
        }
    }

    public void PickUpFromSlot(ShelfSlot slot)
    {
        if (inventory.HasItem)
            return;

        Item item = slot.TakeItem();

        if (item == null)
            return;

        if (!inventory.TryTakeItem(item))
        {
            slot.TryPlaceItem(item);
            return;
        }

        item.transform.SetParent(itemHand);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

    public void PlaceIntoSlot(ShelfSlot slot)
    {
        if (!inventory.HasItem)
            return;

        if (slot.IsOccupied)
            return;

        Item item = inventory.RemoveItem();

        if (item == null)
            return;

        if (!slot.TryPlaceItem(item))
        {
            inventory.TryTakeItem(item);
        }
    }
}
