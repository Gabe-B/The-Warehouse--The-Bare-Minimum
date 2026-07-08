using UnityEngine;

public class boxManager : MonoBehaviour
{
    public BoxCollider productContainer;
    public GameObject boxZone;

    [HideInInspector]
    public bool b_isDirty = false;

    private Vector3 v_dirtyPosition, v_startPosition;

    private Quaternion q_startRot;

    private bool b_hasBeenDirtied = false;

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
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("player") && b_isDirty)
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
        v_dirtyPosition = new Vector3(Random.Range(transform.localPosition.x - 2f, transform.localPosition.x + 2f), transform.localPosition.y, Random.Range(transform.localPosition.z - 2f, transform.localPosition.z + 2f));

        //Changing the local position of the box to the clean state
        transform.localPosition = v_dirtyPosition;

        transform.rotation = Quaternion.Euler(q_startRot.x, Random.Range(-25, 25), q_startRot.z);

        //Enabling the product boxes collision box and displaying the product zone
        productContainer.enabled = true;
        boxZone.GetComponent<MeshRenderer>().enabled = true;
    }
}