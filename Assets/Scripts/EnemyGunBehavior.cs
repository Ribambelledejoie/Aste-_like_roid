using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunBehavior : EnemyBehaviour
{
	[SerializeField] float detectedRadius;
	[SerializeField] float rotationSpeed;

	private Vector2 enemyLookDir;


	private void Update()
	{
		enemyLookDir = distanceEP.normalized;

		//transform.LookAt(rbPlayer.position, Vector3.forward);

	}

	protected override void FixedUpdate()
	{

		base.FixedUpdate();

		Debug.Log(distanceEP);
		if (distanceEP.magnitude >= detectedRadius)
		{
			base.MoveTowardPlayer();
		}

		else
		{
			rb.velocity = Vector2.zero;
			transform.RotateAround(rbPlayer.position, Vector3.forward, rotationSpeed * Time.fixedDeltaTime);
		}

	}

}
