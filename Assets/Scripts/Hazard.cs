using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Hazard : MonoBehaviour
{
    public ParticleSystem breakingEffect;
    private CinemachineImpulseSource cinemachineImpulseSource;
    private Player player;
    Vector3 rotation;
    private void Start()
    {
        var xRotation = Random.Range(90f, 180f);
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        rotation = new Vector3(-xRotation, 0);
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.CompareTag("hazard"))
        {
            Destroy(gameObject,0.1f);
            Instantiate(breakingEffect, transform.position, Quaternion.identity);
            if (player !=null)
            {
                var distance = Vector3.Distance(transform.position, player.transform.position);
                var force = 1f / distance; // 1/10 = 0.1    1/1 = 1
                //Debug.Log(force);
                cinemachineImpulseSource.GenerateImpulse(force);
            }

        }
    }

}
