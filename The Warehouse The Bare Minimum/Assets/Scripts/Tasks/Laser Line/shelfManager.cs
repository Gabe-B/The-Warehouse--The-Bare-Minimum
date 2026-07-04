using UnityEngine;

public class shelfManager : Task
{
    public GameObject prOne;
    public GameObject prTwo;
    public GameObject prThree;
    public GameObject prFour;

    public override void StartingTask()
    {
        Debug.Log("The carryout has begun");
        prOne.tag = "dirty";
        prTwo.tag = "dirty";
        prThree.tag = "dirty";
        prFour.tag = "dirty";
    }

    public override void EndingTask()
    {
        Debug.Log("The carryout has ended");
    }

    void Start()
    {
        
    }
}
