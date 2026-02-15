using UnityEngine;
using TMPro;


public class PlayerRegion : MonoBehaviour
{
  
   public TextMeshProUGUI textMesh;
   public string regionName;

   void OnTriggerStay(Collider other)
   {
       if (other.gameObject.CompareTag("Region"))
       {
           textMesh.text = (string)other.gameObject.name;
       }
      
   }
  
   void OnTriggerExit(Collider other)
   {
       if (other.gameObject.CompareTag("Region"))
       {
           textMesh.text = "Sales Floor";
       }
   }
   
  
}
