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

}
