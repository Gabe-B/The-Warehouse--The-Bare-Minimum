using UnityEngine;
using TMPro;

public class movingCarsR: MonoBehaviour
{
    public GameObject car;
    public TextMeshProUGUI textMesh;
    private int baseSpeed = 5;
    private float modSpeed;

    //Creating the teleport function
    void carTeleport()
    {
        car.transform.position = new Vector3 (-338,10,-275);
    }
    void FixedUpdate()
    {
        //Moving car to original side & setting a modified speed
        if(car.transform.position.z >= 265)
        {
            carTeleport();
            modSpeed = baseSpeed + (Random.Range(0,6));
            textMesh.text = "" + modSpeed;
        }

        //Creating the variable to move the car
        Vector3 carMove = new Vector3(0,0,modSpeed);
        
        //Moving the car
        car.transform.position += carMove;  
    }
}