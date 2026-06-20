using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;

public class hammerTime : MonoBehaviour
{
    //Setting variables and references
    public int fuckUps;
    public TextMeshProUGUI fUps;
    public Material material;
    public GameObject tlNail, trNail, blNail, brNail;
    private bool tlBool, trBool, blBool, brBool;
    public GameObject playerCam, htvCanvas;
    public Camera htvCam;
    public CanvasGroup screenCrack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fuckUps = 3;
        fUps.text = fuckUps.ToString();
        playerCam.SetActive(false);
        htvCam.enabled = htvCam.enabled;

    }

    // Update is called once per frame
    void Update()
    {
        if (tlBool && trBool && blBool && brBool)
        {
            Debug.Log("Nice Job the TV is hung!");
            resetState();
        }

        if (fuckUps <= 0)
        {
            Debug.Log("Way to go you bungled the whole thing.");
            resetState();
        }
    }
    public void onButtonClick()
    {
        fuckUps -= 1;
        screenCrack.alpha += .2f;
        fUps.text = fuckUps.ToString();
    }

    public void TLN()
    {
        tlNail.GetComponent<MeshRenderer>().material = material;
        tlBool = true;
    }
    public void TRN()
    {
        trNail.GetComponent<MeshRenderer>().material = material;
        trBool = true;
    }
    public void BLN()
    {
        blNail.GetComponent<MeshRenderer>().material = material;
        blBool = true;
    }
    public void BRN()
    {
        brNail.GetComponent<MeshRenderer>().material = material;
        brBool = true;
    }

    public void resetState()
    {
        fuckUps = 3;
        tlBool = trBool = blBool = brBool = false;
        brNail.GetComponent<MeshRenderer>().material = material;
        brNail.GetComponent<MeshRenderer>().material = material;
        brNail.GetComponent<MeshRenderer>().material = material;
        brNail.GetComponent<MeshRenderer>().material = material;
        playerCam.SetActive(true);
        htvCanvas.SetActive(false);
        htvCam.enabled = !htvCam.enabled;
    }
}
