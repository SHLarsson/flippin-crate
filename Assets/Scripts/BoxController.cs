using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour{

    public Rigidbody rb;

    public float velX;
    public float velY;

    private Vector2 startPos;
    private Vector2 endPos;

    public float force = 10f;
    public float torque = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        velX = rb.velocity.x;
        velY = rb.velocity.y;

    }

    // Update is called once per frame
    void FixedUpdate(){

        if (Input.GetMouseButtonDown(0)) {
            startPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Swipe();
        }
    }

    void Swipe() {

        Vector2 swipe = endPos - startPos;
        rb.AddForce(swipe * force,ForceMode.Impulse);
        rb.AddTorque(0f, 0f, torque, ForceMode.Impulse);
    }
}
