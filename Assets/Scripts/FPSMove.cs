using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMove : MonoBehaviour
{
    private float gravity = -9.8f;
    private float speed = 5.0f;
    private CharacterController charController;
    private float horizontal;
    private float vertical;
    // Start is called before the first frame update
    private void Start()
    {
        charController = GetComponent<CharacterController>();
    }
    void FixedUpdate()
    {
        //Vector3 move = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
        //transform.Translate(move);
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        // Clamp magnitude for diagonal movement 
        movement = Vector3.ClampMagnitude(movement, 1.0f);

        movement.y = gravity;

        // determine how far to move on the XZ plane 
        movement *= speed;

        // Movement code Frame Rate Independent 
        movement *= Time.deltaTime;

        // Convert local to global coordinates 
        movement = transform.TransformDirection(movement);

        charController.Move(movement);
    }
}
