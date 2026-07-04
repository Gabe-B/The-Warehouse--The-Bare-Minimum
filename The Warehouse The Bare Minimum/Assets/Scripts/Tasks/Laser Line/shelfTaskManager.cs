using UnityEngine;

public class shelfTaskManager : IndividualTaskManager
{
    public GameObject chosenShelf;

    public Task pairedShelf;

	public override void StartTask()
	{
		base.StartTask();

		GameObject[] shelves = GameObject.FindGameObjectsWithTag("shelf");
		chosenShelf = shelves[Random.Range(0, shelves.Length)];
		chosenShelf.GetComponent<shelfManager>().enabled = true;
		pairedShelf.StartingTask();
	}

	public override void EndTask()
	{
		base.EndTask();

		pairedShelf.EndingTask();
	}
}
