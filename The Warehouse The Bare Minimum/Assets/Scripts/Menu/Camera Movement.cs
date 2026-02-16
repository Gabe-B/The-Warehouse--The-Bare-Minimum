using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform camStartPosition, camSinglePlayerPosition;
    public float transitionTime;

    private bool _hasBeenPressed = false;
    private float elapsedTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.transform.position = camStartPosition.position;
        gameObject.transform.rotation = camStartPosition.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(_hasBeenPressed)
		{
            elapsedTime += Time.deltaTime;
            float percentLerpComplete = elapsedTime / transitionTime;

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, camSinglePlayerPosition.position, percentLerpComplete);
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, camSinglePlayerPosition.rotation, percentLerpComplete);
        }
    }

    public void OnStartPressed()
	{
        _hasBeenPressed = true;
	}
}
