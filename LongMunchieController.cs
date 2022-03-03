using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongMunchieController : MonoBehaviour
{


    [SerializeField]
    float rayOffsetX = 0.4f;
    [SerializeField]
    float rayOffsetY = 0.4f;
    [SerializeField]
    float rayOffsetZ = 0.4f;

    public bool incorrectMunchieTutorial = false;

    RaycastHit hit;
    float rayLengthZ = 1;

    public string wallPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.right * rayOffsetX, Vector3.forward, out hit, rayLengthZ) && hit.transform.tag == "wall")
        {
            wallPosition = "forward";
        }
        else if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY - Vector3.right * rayOffsetX, Vector3.back, out hit, rayLengthZ) && hit.transform.tag == "wall")
        {
            wallPosition = "back";
        }
        else
        {
            wallPosition = "none";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
