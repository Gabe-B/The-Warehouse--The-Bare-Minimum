using UnityEngine;

public class boxManager : MonoBehaviour
{
    public BoxCollider productContainer;
    public GameObject boxZone;

    public bool b_isDirty = false;
    public bool b_hasBeenDirtied = false;
    public bool b_hasBeenCleaned = false;

    private Vector3 v_dirtyPosition, v_startPosition;

    private Quaternion q_startRot;

	private void Start()
	{
        productContainer = GetComponent<BoxCollider>();

        if(productContainer == null)
		{
            Debug.Log($"THIS {gameObject.name} PRODUCT HAS NOT HITBOX");
		}

        q_startRot = transform.localRotation;

        v_startPosition = transform.localPosition;
	}

	void Update()
    {
        if (b_isDirty)
        {
            if(!b_hasBeenDirtied)
			{
                dState();

                b_hasBeenDirtied = true;
            }
        }
        else
        {
            cState();

            b_hasBeenCleaned = true;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("player") && b_isDirty && !collision.GetComponent<NewPlayerControls>().isSprinting)
        {
            //Changing box to clean state
            b_isDirty = false;
        }
    }

    void cState()
    {
        //Changing the local position of the box to the clean state
        transform.localPosition = v_startPosition;
        transform.rotation = q_startRot;
        //Disabling the product boxes collision box and hiding the product zone
        productContainer.enabled = false;
        boxZone.GetComponent<MeshRenderer>().enabled = false;
        b_hasBeenDirtied = false;
    }

    void dState()
    {
        v_dirtyPosition = new Vector3(Random.Range(transform.localPosition.x - 0.5f, transform.localPosition.x + 0.5f), transform.localPosition.y, Random.Range(transform.localPosition.z - 0.5f, transform.localPosition.z + 0.5f));

        //Changing the local position of the box to the clean state
        transform.localPosition = v_dirtyPosition;

        transform.rotation = Quaternion.Euler(q_startRot.eulerAngles.x, Random.Range(q_startRot.eulerAngles.y - 25, q_startRot.eulerAngles.y + 25), q_startRot.eulerAngles.z);

        //Enabling the product boxes collision box and displaying the product zone
        productContainer.enabled = true;
        boxZone.GetComponent<MeshRenderer>().enabled = true;
    }
}