using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleOrbit : MonoBehaviour {

    [SerializeField]Transform target;
    [SerializeField]float movSpd = 5;

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 t = target.position;
        t.y = transform.position.y;

        transform.LookAt(t);
        transform.Translate((Vector3.right*h+Vector3.forward*v) *movSpd* Time.deltaTime);
    }
}
