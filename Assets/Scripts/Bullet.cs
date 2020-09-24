using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Cinemachine.CinemachineImpulseSource screenShake;

    // Start is called before the first frame update
    void Start()
    {
        screenShake = GetComponent<Cinemachine.CinemachineImpulseSource>();
        screenShake.GenerateImpulse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
