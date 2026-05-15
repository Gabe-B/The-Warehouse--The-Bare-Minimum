using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class difficultyChange : MonoBehaviour
{
    public List<string> dRating = new List<string>{"Easy", "Medium", "Hard", "Holiday"};
    private int currentIndex = 0;
    public TextMeshProUGUI dLabel;
    public Image pBackground;

    void Start()
    {
        dLabel.text = dRating[currentIndex];
    }
    // Had to google the wrapping around the list so it repeats, in the final version I dont want it to wrap, I want it to stop at easy and have the down button be un clickable and vice versa for the up button on hard
    public void UpDifficulty()
    {
        currentIndex = (currentIndex + 1) % dRating.Count;
        dLabel.text= dRating[currentIndex];
        Debug.Log(currentIndex);
    }

    public void DownDifficulty()
    {
        currentIndex = (currentIndex - 1 + dRating.Count) % dRating.Count;
        dLabel.text= dRating[currentIndex];
        Debug.Log(currentIndex);
    }

    //For some reason on start it makes the dLabel "easy" instead of "Easy", this is a temp fix
    void Update()
    {
        if(dLabel.text == "easy")
        {
            dLabel.text = "Easy";
        }

    }
}