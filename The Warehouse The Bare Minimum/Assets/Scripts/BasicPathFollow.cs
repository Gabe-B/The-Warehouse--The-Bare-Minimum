using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPathFollow : MonoBehaviour
{
	[HideInInspector]
	public bool hasBeenTriggered = false;

	public float timeToLerp;
	public Transform objectToLerp;
	public List<Transform> lerpPoints;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			StartCoroutine(MovementRoutine());
		}
	}

	IEnumerator MovementRoutine()
	{
		if (lerpPoints.Count <= 1)
		{
			yield break;
		}

		Transform t_startPoint = lerpPoints[0];

		for (int i = 1; i < lerpPoints.Count; i++)
		{
			Transform t_endPoint = lerpPoints[i];

			yield return MoveWithinSeconds(objectToLerp, t_startPoint, t_endPoint, timeToLerp);

			t_startPoint = t_endPoint;
		}
	}

	IEnumerator MoveWithinSeconds(Transform obj, Transform start, Transform end, float duration)
	{
		float t_timeElapsed = 0;

		while(t_timeElapsed < duration)
		{
			float percentComplete = t_timeElapsed / duration;

			obj.position = Vector3.Lerp(start.position, end.position, percentComplete);

			t_timeElapsed += Time.deltaTime;

			yield return null;
		}

		obj.position = end.position;
	}
}
