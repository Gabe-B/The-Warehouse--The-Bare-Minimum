using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemName;

	public string ItemName => itemName;
}
