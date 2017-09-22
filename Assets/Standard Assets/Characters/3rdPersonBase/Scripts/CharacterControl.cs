using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {

    [SerializeField]float movSpd = 4f;
    [SerializeField]float movingTurnSpd = 720;
    [SerializeField]float standingTurnSpd = 360;
    [SerializeField]float maxGroundCheck = 0.1f;
    [SerializeField]float jumpPow = 8;

    Vector3 groundVect;

    bool grounded = true;

    float forwardPow;
    float sidePow;
    float camDistPrevious = 0;

    Rigidbody myRB;

    void Start ()
    {
        myRB = GetComponent<Rigidbody>();
	}

    public void Move(Vector3 mov, bool jump, float camDist)
    {
        if (mov.magnitude > 1f) { mov.Normalize(); }

        checkForGround();

        Vector3 relativeMov = transform.InverseTransformDirection(mov);
        relativeMov = Vector3.ProjectOnPlane(relativeMov, groundVect);

        float forwardPow = relativeMov.z, 
            sidePow = relativeMov.x, 
            turnPow = Mathf.Atan2(sidePow, forwardPow);

        applyRotations(turnPow);
        applyGroundMotion(new Vector3(0,0,forwardPow)*movSpd*Time.deltaTime, jump);

        camDistPrevious = camDist;
    }

    void checkForGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position+Vector3.up*0.1f,Vector3.down,out hit, maxGroundCheck))
        {
            groundVect = hit.normal;
            grounded = true;
        } else
        {
            groundVect = Vector3.up;
            grounded = false;
        }
    }

    void applyRotations(float turnPow)
    {
        float turnSpd = Mathf.Lerp(standingTurnSpd,movingTurnSpd,forwardPow);
        transform.Rotate(0, turnPow * turnSpd * Time.deltaTime, 0);
        //Debug.Log(turnPow * turnSpd * Time.deltaTime);

        
    }

    void applyGroundMotion(Vector3 mov, bool jump)
    {
        transform.Translate(mov);

        Debug.DrawRay(Vector3.zero, transform.TransformDirection(mov)*10);

        if (jump && grounded) myRB.velocity = new Vector3(myRB.velocity.x, jumpPow, myRB.velocity.z); 
    }

    void applyAirMotion(Vector3 mov)
    {
        transform.Translate(mov);
    }
}
