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
    public GameObject spawnPoint;

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
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "LandingPlatform"){
            StartCoroutine(checkVelocity());
        }
    }

    IEnumerator checkVelocity()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("xVel " + rb.velocity.x);
        Debug.Log("yVel " + rb.velocity.y);
        if (rb.velocity.x < 0.01 && rb.velocity.y < 0.01 && thrown)
        {
            landed();
        }
    }

    void Swipe() {
        if (!thrown) {
            Vector2 swipe = endPos - startPos;

            //Debug.Log(swipe);

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
        
        Instantiate(staticBox,new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        transform.position = spawnPoint.transform.position;
        thrown = false;
    }
}
