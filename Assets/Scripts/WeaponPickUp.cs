using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{

    public Weapon weaponToPick;

    [SerializeField] private List<Weapon> weaponList;

    private void Start()
    {
        var randomWeaponID = Random.Range(0, weaponList.Count);
        weaponToPick = weaponList[randomWeaponID];
    }
}
