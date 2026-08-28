using UnityEngine;

public class movingCarsR: MonoBehaviour
{
    public GameObject car;
    private int baseSpeed = 3;
    private float modSpeed;

    private Renderer colorRandomizer;

    void Start()
    {
        colorRandomizer = GetComponent<Renderer>();
    }
    //Creating the teleport function
    void carTeleport()
    {
        colorRandomizer.material.SetColor("_Color",new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f)));
        car.transform.position = new Vector3 (-338,10,-275);
    }
    void FixedUpdate()
    {
        //Moving car to original side & setting a modified speed
        if(car.transform.position.z >= 265)
        {
            carTeleport();
            modSpeed = baseSpeed + Random.Range(0,6);
        }

        //Creating the variable to move the car
        Vector3 carMove = new Vector3(0,0,modSpeed);
        
        //Moving the car
        car.transform.position += carMove;  
    }
}