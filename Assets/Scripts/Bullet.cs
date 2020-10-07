using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{



    Cinemachine.CinemachineImpulseSource screenShake;
    [SerializeField] private GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        screenShake = GetComponent<Cinemachine.CinemachineImpulseSource>();
        screenShake.GenerateImpulse();
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity); //Quaternion.identity = no rotation
        Destroy(effect, 0.85f);
        Destroy(gameObject);
    }
    */

    // Update is called once per frame
    void Update()
    {
        
    }
}
