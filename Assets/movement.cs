using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
    private CharacterController controller;
    private Vector3 moveDir = Vector3.zero;
    public float speed = 6.0f;
    public float sensitivity = 5.0f;
    public float camRayLength = 100.0f;

    int floorMask;
    // Use this for initialization
    void Start () {
        controller = GetComponent<CharacterController>();
        floorMask = LayerMask.GetMask("floor");
	}
	
	// Update is called once per frame
	void Update () {

        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //moveDir = transform.TransformDirection(moveDir);
        moveDir *= speed;

        controller.Move(moveDir * Time.deltaTime);

        Turning();
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;
            Debug.Log(playerToMouse);
            Quaternion rot = Quaternion.LookRotation(playerToMouse);
            transform.rotation = rot;
        }
    }
}
