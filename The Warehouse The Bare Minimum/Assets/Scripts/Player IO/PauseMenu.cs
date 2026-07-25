using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu _instance { get; private set; }

    public GameObject pausePanel;

	private void Awake()
	{
        if (_instance != null && _instance != this)
        {
            Debug.Log("Duplicate pause menu found. Deleting instance.");
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
