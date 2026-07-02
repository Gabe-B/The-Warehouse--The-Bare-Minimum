using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    //How close the camera will get to the player
    public float minDistance = 1.0f;

    //How far the camera will get from the player
    public float maxDistance = 4.0f;

    //Smooths the cameras movements
    public float smooth = 10.0f;

    //Current distance
    public float distance;

    //These two handle the values needed to adjust the camera
    public Vector3 dollyDirectionAdjusted;
    Vector3 dollyDirection;

    LayerMask zones;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        dollyDirection = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;

        zones = ~LayerMask.GetMask("Zone");
    }

    // Update is called once per frame
    void Update()
    {
        //The desired position of the camera based on the current position of the camera pivot point times the maximum allowed distance. Basically the maximum distance the camera can be from the player based on current position
        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDirection * maxDistance);
        RaycastHit hit;

        //Checks if there is an object directly behind the camera pivot point. If there is, it sets the cameras distance equal to the distance from the center to the hit location. If not, sets it to the max distance
        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit, zones))
        {
            distance = Mathf.Clamp((hit.distance * 0.4f), minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }

        //Sets the cameras position on a lerp between the current position and the desired position over time
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDirection * distance, Time.deltaTime * smooth);
    }
}