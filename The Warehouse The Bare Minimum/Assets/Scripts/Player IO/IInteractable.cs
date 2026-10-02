public interface IInteractable
{
    bool CanInteract(ItemInteraction player);

    void Interact(ItemInteraction player);

    string GetInteractionPrompt(ItemInteraction player);
}
