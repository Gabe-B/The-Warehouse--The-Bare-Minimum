using TMPro;
using UnityEngine;

public class faceAnimations : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator anim;
    private float relIndex;
    private string faceName;

    // Update is called once per frame

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //Playing the animation
        

        //Creating an input to work with
        if (Input.GetKeyDown("q") && relIndex < 12)
        {
            relIndex += 1;
            string myrelIndex = relIndex.ToString();
            string faceName = "face"+myrelIndex;
            anim.Play(faceName,0);
        }

        if (Input.GetKeyDown("1") && relIndex > 0)
        {
            relIndex -= 1;
            string myrelIndex = relIndex.ToString();
            string faceName = "face"+myrelIndex;
            anim.Play(faceName,0);
        }       
    }
}