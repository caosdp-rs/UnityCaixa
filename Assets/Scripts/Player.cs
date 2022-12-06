using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public float forceMultiple = 3f;
    public float maxVelocity = 3f;
    private Rigidbody rb;
    private CinemachineImpulseSource cinemachineImpulseSource;
    public ParticleSystem deathParticles;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnEnable()
    {
        transform.position = new Vector3(0, 1.97f, 0);
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
        
    {
        if (GameManager.Instance == null)
        {
            return;
        }
        float horizontalInput = 0;

        if (Input.GetMouseButton(0))
        {
            var center = Screen.width / 2;
            var mousePosition = Input.mousePosition;
            if (mousePosition.x > center)
            {
                horizontalInput = 1;
            }
            else
            {
                horizontalInput = -1;
            }
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }
        if (rb.velocity.magnitude <= maxVelocity)
        {
            rb.AddForce(new Vector3(horizontalInput * forceMultiple, 0, 0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("hazard"))
        {
            //Destroy(gameObject);
            GameManager.Instance.GameOver();
            gameObject.SetActive(false);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            cinemachineImpulseSource.GenerateImpulse();



        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("FallDown"))
        {
            GameManager.Instance.GameOver();
            gameObject.SetActive(false);
        }

    }
}
