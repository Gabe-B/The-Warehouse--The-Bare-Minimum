using UnityEngine;

public class shelfManager : MonoBehaviour
{
    public GameObject prOne;
    public GameObject prTwo;
    public GameObject prThree;
    public GameObject prFour;
    
    void Start()
    {
        prOne.tag = "dirty";
        prTwo.tag = "dirty";
        prThree.tag = "dirty";
        prFour.tag = "dirty";
    }
}
