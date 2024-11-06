using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIRAAAAAAAA : MonoBehaviour
{
    public Transform pivotPoint;
    public float rotationSpeed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivotPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
