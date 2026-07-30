using UnityEngine;

public class settingsPage : MonoBehaviour
{
    public GameObject graphicSet, audioSet, controlSet;
    public void graphicEnabled()
    {
        graphicSet.SetActive(true);
        audioSet.SetActive(false);
        controlSet.SetActive(false);
    }
    public void audioEnabled()
    {
        graphicSet.SetActive(false);
        audioSet.SetActive(true);
        controlSet.SetActive(false);
    }
    public void controlEnabled()
    {
        graphicSet.SetActive(false);
        audioSet.SetActive(false);
        controlSet.SetActive(true);
    }
}
