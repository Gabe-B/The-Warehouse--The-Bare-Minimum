using UnityEngine;
using TMPro;


public class InteractionUI : MonoBehaviour
{
  
   public TextMeshProUGUI textMesh;

   void OnTriggerStay(Collider other)
   {
       if (other.gameObject.CompareTag("interUI"))
       {
           textMesh.text = "Press E to interact";
           if (Input.GetKey("e"))
            {
                Debug.Log("You were in zone and pressed E");
            }
       }
      
   }
  
   void OnTriggerExit(Collider other)
   {
       if (other.gameObject.CompareTag("interUI"))
       {
           textMesh.text = "";
       }
   }
   
}
