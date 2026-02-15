using UnityEngine;
using TMPro;


public class InteractionUI : MonoBehaviour
{
  
   public TextMeshProUGUI textMesh;
   private bool inside;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("player")){
            inside = true;
            textMesh.text = "Press E to interact";
        }
    }

    void OnTriggerExit(Collider other)
    {
        inside = false;
        textMesh.text = "";
    }

    void Update()
    {
        if (inside = true && Input.GetKeyDown("e"))
            {
                Debug.Log("You were in zone and pressed E");
            }
    }
}
