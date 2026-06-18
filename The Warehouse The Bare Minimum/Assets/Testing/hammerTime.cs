using UnityEngine;
using TMPro;

public class hammerTime : MonoBehaviour
{
    public int fuckUps;
    public TextMeshProUGUI fUps;
    public Material material;
    public GameObject nail1,nail2,nail3,nail4;
    private bool nBool1, nBool2, nBool3, nBool4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fuckUps = 3;
        fUps.text = fuckUps.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (nBool1&&nBool2&&nBool3&&nBool4)
        {
            Debug.Log("Nice Job the TV is hung!");
        }

        if (fuckUps<= 0)
        {
            Debug.Log("Way to go you bungled the whole thing.");
        }
    }
    public void onButtonClick()
    {
        fuckUps -= 1;
        fUps.text = fuckUps.ToString();
    }

    public void TLN()
    {
        nail1.GetComponent<MeshRenderer> ().material = material;
        nBool1 = true;
    }
    public void TRN()
    {
        nail2.GetComponent<MeshRenderer> ().material = material;
        nBool2 = true;
    }
    public void BLN()
    {
        nail3.GetComponent<MeshRenderer> ().material = material;
        nBool3 = true;
    }
    public void BRN()
    {
        nail4.GetComponent<MeshRenderer> ().material = material;
        nBool4 = true;
    }
}
