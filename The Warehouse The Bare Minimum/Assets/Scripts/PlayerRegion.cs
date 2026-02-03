using UnityEngine;
using TMPro;


public class PlayerRegion : MonoBehaviour
{
  
   public TextMeshProUGUI textMesh;
   public string regionName;
  
   void OnTriggerExit(Collider other)
   {
       if (other.gameObject.CompareTag("Region"))
       {
           textMesh.text = "Sales Floor";
       }
   }
   void OnTriggerEnter(Collider other)
   {
       if (other.gameObject.CompareTag("Region"))
       {
           textMesh.text = (string)other.gameObject.name;
       }
      
   }
  
}
