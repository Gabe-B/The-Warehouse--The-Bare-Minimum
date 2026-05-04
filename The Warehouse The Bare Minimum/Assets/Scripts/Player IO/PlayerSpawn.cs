using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawn : MonoBehaviour
{
    public Transform[] spawnPoints;

    private int playerCount;

    public void OnPlayerJoined(PlayerInput playerInput)
	{
        //Debug.Log(playerInput.transform.position);
        playerInput.GetComponent<Rigidbody>().position = spawnPoints[playerCount].transform.position;

        playerCount++;           
	}
}
