using UnityEngine;

public class shelfTaskManager : MonoBehaviour
{
    public GameObject chosenShelf;
    void Start()
    {
        GameObject[] shelves = GameObject.FindGameObjectsWithTag("shelf");
        chosenShelf = shelves[Random.Range(0,shelves.Length)];
        chosenShelf.GetComponent<shelfManager>().enabled = true;
    }
}
