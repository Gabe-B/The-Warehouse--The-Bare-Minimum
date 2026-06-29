using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasicPathFollow : MonoBehaviour
{
	[HideInInspector]
	public bool hasBeenTriggered = false;

	[SerializeField]
	private bool lerpOnStart = false;

	//public float timeToLerp;
	public Transform objectToLerp;
	public List<Transform> lerpPoints;
	public List<float> lerpTime;

	public bool isDoneLerping = false;

	private int previousLerpPointLength = 0;

	private void OnValidate()
	{
		if (EditorApplication.isPlayingOrWillChangePlaymode) return;

		if (lerpPoints.Count != previousLerpPointLength)
		{
			if (lerpPoints.Count > lerpTime.Count)
			{
				for (int i = lerpTime.Count; i < lerpPoints.Count; i++)
				{
					lerpTime.Add(0);
				}
			}
			else if (lerpPoints.Count < lerpTime.Count)
			{
				for (int i = lerpTime.Count; i > lerpPoints.Count; i--)
				{
					lerpTime.RemoveAt(i - 1);
				}
			}

			previousLerpPointLength = lerpPoints.Count;
		}


	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		if (lerpTime.Count != lerpPoints.Count)
		{
			throw new System.Exception("Timing list isn't the same length as points list");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (lerpOnStart)
		{
			StartCoroutine(MovementRoutine());
		}
	}

	IEnumerator MovementRoutine()
	{
		lerpOnStart = false;

		if (lerpPoints.Count <= 1)
		{
			yield break;
		}

		Transform t_startPoint = lerpPoints[0];

		for (int i = 1; i <= lerpPoints.Count - 1; i++)
		{
			Transform t_endPoint = lerpPoints[i];

			yield return MoveWithinSeconds(objectToLerp, t_startPoint, t_endPoint, lerpTime[i]);

			t_startPoint = t_endPoint;
		}

		isDoneLerping = true;
	}

	IEnumerator MoveWithinSeconds(Transform obj, Transform start, Transform end, float duration)
	{
		float t_timeElapsed = 0;

		while (t_timeElapsed < duration)
		{
			float percentComplete = t_timeElapsed / duration;

			obj.position = Vector3.Lerp(start.position, end.position, percentComplete);
			obj.rotation = Quaternion.Lerp(start.rotation, end.rotation, percentComplete);

			t_timeElapsed += Time.deltaTime;

			yield return null;
		}

		obj.position = end.position;
	}
}
