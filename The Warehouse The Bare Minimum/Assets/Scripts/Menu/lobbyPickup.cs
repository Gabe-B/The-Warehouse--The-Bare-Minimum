using System.Collections.Generic;
using UnityEngine;

public class lobbyPickup : MonoBehaviour
{
    private int playerNumber;
    public List<GameObject> playerPallet;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            Destroy(other.gameObject);
            playerPallet[playerNumber].SetActive(true);
            playerNumber++;
        }
    }
}
