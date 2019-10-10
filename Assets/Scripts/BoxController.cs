using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxController : MonoBehaviour{

    public Rigidbody rb;

    public float velX;
    public float velY;

    private Vector2 startPos;
    private Vector2 endPos;

    public float force = 10f;
    public float torque = 10f;

    public bool thrown = false;

    public float highScore = 0;

    public GameObject staticBox;

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

        if (Input.GetKeyDown("r"))  SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (velX == 0 && velY == 0 && thrown) {
            landed();
        }
    }

    void Swipe() {
        if (!thrown) {
            Vector2 swipe = endPos - startPos;

            Debug.Log(swipe);

            rb.AddForce(swipe * force, ForceMode.Impulse);
            rb.AddTorque(0f, 0f, torque, ForceMode.Impulse);
            thrown = true;
            // add so force cant be negative
        }
    }

    void landed() {
        //creates static box and resets player box
        highScore = transform.position.y;
        Debug.Log(highScore);
        Instantiate(staticBox);
        thrown = false;
    }
}
