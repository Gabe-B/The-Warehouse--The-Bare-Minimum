using UnityEngine;

public class hangTV : MonoBehaviour
{
    public GameObject twoArms;
    public float mouseX;
    public float mouseY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        // Vector3 mouseScreenPos = Input.mousePosition;
        twoArms.transform.position = new Vector3 (mouseX,mouseY,0);
    }
}
