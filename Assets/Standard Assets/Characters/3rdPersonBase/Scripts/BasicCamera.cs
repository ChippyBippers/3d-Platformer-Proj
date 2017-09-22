using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCamera : MonoBehaviour {
    public GameObject target;
    Transform targetTrans;
    [SerializeField]float upOffset = 0.1f;
    [SerializeField]float idealDistance = 3;
    [SerializeField]float realDistance;
    [SerializeField]float shiftRate = 0.6f;
    [SerializeField]float strafeSpeed = 4;

    void Start()
    {
        if (target != null)
        targetTrans = target.transform;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            cameraStrafe();

            calcRealDistance();
            backUpJack();

            transform.LookAt(targetTrans.position + Vector3.up * upOffset, Vector3.up);
        }
    }

    void calcRealDistance()
    {
        Vector2 d1 = new Vector2(targetTrans.position.x, targetTrans.position.z);
        Vector2 d2 = new Vector2(transform.position.x, transform.position.z);
        realDistance = Vector2.Distance(d1, d2);
    }

    void backUpJack()
    {
        float difference = idealDistance - realDistance;

        Vector3 backVect = -transform.forward * Mathf.Lerp(0, difference, shiftRate);
        backVect.y = 0;

        transform.position += backVect;
    }

    void cameraStrafe()
    {
        float LR = Input.GetAxis("CameraLR");

        transform.Translate(Vector3.right*(LR * strafeSpeed * Time.deltaTime));
    }
}
