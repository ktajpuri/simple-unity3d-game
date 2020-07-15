using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Vector3 offset = movementVector * 0.2f;
            print(Input.mousePosition);
            print(transform.position.x);
            print(transform.position.y);
            print(transform.position.z);
            //transform.position = transform.position + offset;
        }

        if (Input.GetMouseButtonDown(1))
        {
            print(Input.mousePosition);
            float rotationThisFrame = 1000 * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");
    }
}
