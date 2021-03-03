using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Gun")]
public class DefaultGun : Weapon
{
    public override void Shoot(Transform FirePoint)
    {
        GameObject instantiatedBullet = Instantiate(bullet, FirePoint.position, FirePoint.rotation);

        Rigidbody2D rbBullet = instantiatedBullet.GetComponent<Rigidbody2D>();

        rbBullet.AddForce(FirePoint.up * bulletForce, ForceMode2D.Impulse);

    }
}
