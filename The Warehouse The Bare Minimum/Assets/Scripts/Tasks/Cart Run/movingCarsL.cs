using UnityEngine;

public class movingCarsL: MonoBehaviour
{
    public GameObject car;
    private int baseSpeed = 3;
    private float modSpeed;
    private Renderer colorRandomizer;

    //Creating the teleport function
    void Start()
    {
        colorRandomizer = GetComponent<Renderer>();
    }
    void carTeleport()
    {
        colorRandomizer.material.SetColor("_Color",new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f)));
        car.transform.position = new Vector3 (-415,10,265);
    }
    void FixedUpdate()
    {
        //Moving car to original side & setting a modified speed
        if(car.transform.position.z <= -275)
        {
            carTeleport();
            modSpeed = baseSpeed + Random.Range(0,6);
        }

        //Creating the variable to move the car
        Vector3 carMove = new Vector3(0,0,-modSpeed);
        
        //Moving the car
        car.transform.position += carMove;  
    }
}