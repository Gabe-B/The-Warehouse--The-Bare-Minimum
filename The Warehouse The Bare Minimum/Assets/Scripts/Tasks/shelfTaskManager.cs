using UnityEngine;

public class shelfTaskManager : IndividualTaskManager
{
    public GameObject chosenShelf;

    public Task pairedShelf;

	public override void StartTask()
	{
		base.StartTask();

		GameObject[] shelves = GameObject.FindGameObjectsWithTag("Shelf");
		chosenShelf = shelves[Random.Range(0, shelves.Length)];

		pairedShelf.StartingTask();
	}

	public override void EndTask()
	{
		base.EndTask();

		pairedShelf.EndingTask();
	}
}
