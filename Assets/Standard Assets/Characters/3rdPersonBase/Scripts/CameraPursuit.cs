using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPursuit : MonoBehaviour {

    public GameObject target;
    Transform targetTrans;
    Vector3 offset;
    float targetTY;
    float tilt = 15.0f;

	// Use this for initialization
	void Start () {
		if (target != null)
        {
            targetTrans = target.transform;
            targetTY = targetTrans.rotation.y;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (target != null)
        {
            transform.position = targetTrans.position;

        }
	}

    void adjustRotation()
    {

    }
}
