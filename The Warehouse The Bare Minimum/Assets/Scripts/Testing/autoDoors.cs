using UnityEngine;
using System.Collections;

public class autoDoors : MonoBehaviour
{
    public GameObject doorOne, doorTwo;
    public Transform d1Open, d1Closed, d2Open, d2Closed;
    private bool hasOpened;
    public float duration;

    //ALOT OF THIS CAN BE REWRITTEN I HAD TO LOOK UP ALOT AND USE ALOT OF WHAT I SAW
    void OnTriggerEnter(Collider other)
    {
        if (!hasOpened)
        {
            hasOpened = true;
            StartCoroutine(LerpLoop());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (hasOpened)
        {
            hasOpened = false;
            StartCoroutine(LerpLoop());
        }
    }
    private IEnumerator LerpLoop()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = doorOne.transform.position;

        //Opening Animation
        if (hasOpened)
        {
            // This is your frame-by-frame loop
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime; // Track time passed since last frame
                float t = elapsedTime / duration; // Convert time to a 0.0 - 1.0 percentage

                // Smoothly update position based on the loop's progress
                doorOne.transform.position = Vector3.Lerp(d1Closed.transform.position, d1Open.transform.position, t);
                doorTwo.transform.position = Vector3.Lerp(d2Closed.transform.position, d2Open.transform.position, t);

                // Tells Unity to pause here, render the frame, 
                // and resume the loop on the next frame
                yield return null;
            }
            // Snap precisely to the target position when the loop finishes
            doorOne.transform.position = d1Open.transform.position;
            doorTwo.transform.position = d2Open.transform.position;
        }

        //Closing Animation
        if (!hasOpened)
        {
            // This is your frame-by-frame loop
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime; // Track time passed since last frame
                float t = elapsedTime / duration; // Convert time to a 0.0 - 1.0 percentage

                // Smoothly update position based on the loop's progress
                doorOne.transform.position = Vector3.Lerp(d1Open.transform.position, d1Closed.transform.position, t);
                doorTwo.transform.position = Vector3.Lerp(d2Open.transform.position, d2Closed.transform.position, t);

                // Tells Unity to pause here, render the frame, 
                // and resume the loop on the next frame
                yield return null;
            }
            doorOne.transform.position = d1Closed.transform.position;
            doorTwo.transform.position = d2Closed.transform.position;
        }
    }
}