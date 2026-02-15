using UnityEngine;

public class lookAtCamera : MonoBehaviour

{

    public Transform cameraSelect;
    
    void Start()
    {
        cameraSelect = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(cameraSelect.position);
    }
}
