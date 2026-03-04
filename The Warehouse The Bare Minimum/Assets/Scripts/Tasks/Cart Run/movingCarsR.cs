using UnityEngine;

public class movingCarsR: MonoBehaviour
{
    public GameObject car;
    private int baseSpeed = 5;
    private float modSpeed;

    //Creating the teleport function
    void carTeleport()
    {
        car.transform.position = new Vector3 (-838,14,-270);
    }
    void FixedUpdate()
    {
        //Moving car to original side & setting a modified speed
        if(car.transform.position.x >= 838)
        {
            carTeleport();
            modSpeed = baseSpeed + (Random.Range(0,6));
            Debug.Log("Bottom lane speed: " + modSpeed);
        }

        //Creating the variable to move the car
        Vector3 carMove = new Vector3(modSpeed,0,0);
        
        //Moving the car
        car.transform.position += carMove;  
    }
}
