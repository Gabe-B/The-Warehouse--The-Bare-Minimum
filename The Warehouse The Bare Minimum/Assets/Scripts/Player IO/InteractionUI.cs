using UnityEngine;
using TMPro;
using UnityEngine.Rendering;


public class InteractionUI : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public bool inZone, taskStart;
    private int placeHolder;

    void Start()
    {
        inZone = taskStart = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("interUI"))
        {
            inZone = true;   
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag("interUI"))
        {
            inZone = false;
        }
        
    }

    void Update()
    {
        if(inZone && Input.GetKeyDown("e"))
        {
            //Put whatever interaction is needed after this comment
            placeHolder += 1;
            textMesh.text = placeHolder.ToString();
            //Pick up object
            taskStart = true;
        }
        //Throw Object
    
    }
}
