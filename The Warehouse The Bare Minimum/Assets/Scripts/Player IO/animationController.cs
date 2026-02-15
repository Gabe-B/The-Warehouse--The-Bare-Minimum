using UnityEngine;

public class animControls : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody rb;

    public float animationSpeed;
    private float speed;
    
    void Update()
    {
        if (rb.linearVelocity.magnitude>0)
        {
            Player.GetComponent<Animator>().Play("walk");
            Player.GetComponent<Animator>().speed = rb.linearVelocity.magnitude*(1/animationSpeed);
        }
        if (!(rb.linearVelocity.magnitude>0))
        {
            Player.GetComponent<Animator>().Play("stand");
        }
    }
}
