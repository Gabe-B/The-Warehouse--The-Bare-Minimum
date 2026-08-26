using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class playerIndicator : MonoBehaviour
{
    public GameObject p1Icon,p2Icon,p3Icon,p4Icon;
    public List<GameObject> playerIcons => new List<GameObject> {p1Icon, p2Icon, p3Icon,p4Icon};
    private int playerCount;

    void Start()
    {
        playerCount = GameObject.FindGameObjectsWithTag("player").Length;
        Debug.Log(playerCount);
        for (int i = 0; i < playerCount;i++)
        {
            playerIcons[i].SetActive(true);
        }
    }
}
