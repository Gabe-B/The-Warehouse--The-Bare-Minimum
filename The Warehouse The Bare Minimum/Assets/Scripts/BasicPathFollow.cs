using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasicPathFollow : MonoBehaviour
{
	//Sets the lerp to begin when marked as 'true'
	[SerializeField]
	private bool lerpOnStart = false;

	//Usually the gameobject this script is attached to. Defines the object being lerped
	public Transform objectToLerp;

	//List of points the obeject will lerp along
	public List<Transform> lerpPoints;

	//List of times used to define how long it should take to lerp between each point
	public List<float> lerpTime;

	//Marks when lerp is finished
	public bool isDoneLerping = false;

	//Used to make sure list of points count changes according to edits made in the editor
	private int previousLerpPointLength = 0;

	//Runs whenever a change is made in the inspector
	private void OnValidate()
	{
		if (EditorApplication.isPlayingOrWillChangePlaymode) return; //Exits function if the editor is changing modes. Prevents changes while the game changes play modes from being undone

		//If the length of 'lerpPoints' has updated
		if (lerpPoints.Count != previousLerpPointLength)
		{
			//If there are more points that times
			if (lerpPoints.Count > lerpTime.Count)
			{
				//Adds to the time list
				for (int i = lerpTime.Count; i < lerpPoints.Count; i++)
				{
					lerpTime.Add(0);
				}
			}
			//If there are more times than points
			else if (lerpPoints.Count < lerpTime.Count)
			{
				//Adds to the points list
				for (int i = lerpTime.Count; i > lerpPoints.Count; i--)
				{
					lerpTime.RemoveAt(i - 1);
				}
			}

			//Sets previous count to current count
			previousLerpPointLength = lerpPoints.Count;
		}
	}

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		//Checks to make sure the two lists have the same length
		if (lerpTime.Count != lerpPoints.Count)
		{
			throw new System.Exception("Timing list isn't the same length as points list");
		}
	}

	// Update is called once per frame
	void Update()
	{
		//Starts the lerp
		if (lerpOnStart)
		{
			//A co-routine is used to run on a separate thread. Keeps this running while the rest of the script is running
			StartCoroutine(MovementRoutine());
		}
	}

	//Co-routine for handling the lerp
	IEnumerator MovementRoutine()
	{
		//Breaks the loop in 'Update'
		lerpOnStart = false;

		//Exits the lerp if there are no/a single point
		if (lerpPoints.Count <= 1)
		{
			yield break;
		}

		//Sets the start point to the first point in the list
		Transform t_startPoint = lerpPoints[0];

		//Runs for each point in the list
		for (int i = 1; i <= lerpPoints.Count - 1; i++)
		{
			//Sets the end point to the next point in the list
			Transform t_endPoint = lerpPoints[i];

			//Starts and waits for the actual lerp top finish. Lerps the defined object from the start point to the end point over a defined period of time
			yield return MoveWithinSeconds(objectToLerp, t_startPoint, t_endPoint, lerpTime[i]);
			
			//Increments the start point to the end point of the lerp
			t_startPoint = t_endPoint;
		}

		//Used to tell other scripts that the lerp is done
		isDoneLerping = true;
	}

	IEnumerator MoveWithinSeconds(Transform obj, Transform start, Transform end, float duration)
	{
		//Set timer to 0
		float t_timeElapsed = 0;

		//Runs until time is up
		while (t_timeElapsed < duration)
		{
			//Percentage used to determine how far along the lerp to move the objecy
			float percentComplete = t_timeElapsed / duration;

			//Moves the object along a straight path from the defined point to the defined end point
			obj.position = Vector3.Lerp(start.position, end.position, percentComplete);

			//Rotates the object linearly between the two roation values of the start and end points
			obj.rotation = Quaternion.Lerp(start.rotation, end.rotation, percentComplete);

			t_timeElapsed += Time.deltaTime;

			yield return null;
		}

		//Makes sure the obejct is set to the right position and rotation
		obj.position = end.position;
		obj.rotation = end.rotation;
	}
}
