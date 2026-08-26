using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameTips : MonoBehaviour
{
    public List<string> tipsList = new List<string>
    {
        //Add new game tips here
        "Take recycling to the bins.",
        "Dont poop with the door open.",
        "Avoid management always.",
        "All characters have unique abilities.",
        "Do the bare minimum.",
        "Always put shopping carts back, lazy bones.",

    };
    public int fart;
    public TextMeshProUGUI textMesh;
    public GameObject pauseUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void OnEnable()
    {
        getTip();
    }
    public void getTip()
    {
        if (pauseUI.activeSelf)
        {
            fart = Random.Range(0, tipsList.Count);
            textMesh.text = tipsList[fart];
        }
        if (!pauseUI.activeSelf)
        {
            textMesh.text = null;
        }
    }

}
