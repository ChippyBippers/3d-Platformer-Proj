using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterControl))]
public class BasicInputSender : MonoBehaviour {

    private CharacterControl characterOutput;
    private Transform camTran;
    private Vector3 camForward;
    private Vector3 camSide;
    [SerializeField]
    float camDist = 0f;

    [SerializeField]
    float relativeAngleToCamera;

    
    void Start () {
        characterOutput = GetComponent<CharacterControl>();

        if (Camera.main != null)
        {
            camTran = Camera.main.transform;
        }
	}
	
	
	void FixedUpdate () {
        float xx = Input.GetAxis("Horizontal");
        float zz = Input.GetAxis("Vertical");

        Vector3 moveVect;

        bool jump = Input.GetButtonDown("Jump");

        if (Camera.main != null)
        {

            relativeAngleToCamera = transform.eulerAngles.y - camTran.eulerAngles.y;

            Vector3 forwards = camTran.forward * zz;
            Vector3 sideways = camTran.right * xx;

            camDist = Vector3.Distance(transform.position, camTran.position);

            moveVect = forwards + sideways;
            moveVect.y = 0;

            //Debug.Log(moveVect);

            
        } else
        {
            moveVect = zz * Vector3.forward + xx * Vector3.right;
        }

        Debug.DrawRay(Vector3.zero, moveVect);

        characterOutput.Move(moveVect, jump, camDist);
	}
}
