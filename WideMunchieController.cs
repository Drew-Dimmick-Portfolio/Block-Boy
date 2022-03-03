using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WideMunchieController : MonoBehaviour
{
    [SerializeField]
    float rayOffsetX = 0.4f;
    [SerializeField]
    float rayOffsetY = 0.4f;
    [SerializeField]
    float rayOffsetZ = 0.4f;

    public bool incorrectMunchieTutorial = false;

    RaycastHit hit;
    float rayLengthX = 1;

    public string wallPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.forward * rayOffsetZ, Vector3.left, out hit, rayLengthX) && hit.transform.tag == "wall")
        {
            wallPosition = "left";
        }
        else if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY - Vector3.forward * rayOffsetZ, Vector3.right, out hit, rayLengthX) && hit.transform.tag == "wall")
        {
            wallPosition = "right";
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
