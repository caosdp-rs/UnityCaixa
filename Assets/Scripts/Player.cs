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
    public GameObject mainVcam;
    public GameObject zoomVcam;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        if (rb.velocity.magnitude <= maxVelocity)
        {
            rb.AddForce(new Vector3(horizontalInput * forceMultiple, 0, 0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("hazard"))
        {
            GameManager.GamerOver();
            Destroy(gameObject);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            cinemachineImpulseSource.GenerateImpulse();
            mainVcam.SetActive(false);
            zoomVcam.SetActive(true);
            

        }
    }
}
