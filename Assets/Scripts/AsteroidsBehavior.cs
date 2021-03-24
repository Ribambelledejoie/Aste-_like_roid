using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 1 - récupérer la position du joueur : récupérer joueur + position
/// </summary>
public class AsteroidsBehavior : EnemyBehaviour
{

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        base.MoveTowardPlayer();
    }

}
