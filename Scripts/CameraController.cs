using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject player;
	public Vector3 offset;
	public Vector3 moveVector;

	public float transition = 0f;
	public float animationDuration = 1f;
	public Vector3 animationOffset = new Vector3(0, 5, 5);

	// Use this for initialization
	void Start()
	{
		offset = transform.position - player.transform.position;
	}

	// Update is called once per frame
	void LateUpdate()
	{
		moveVector = player.transform.position + offset;

		// x
		moveVector.x = 4.87f;

		if (transition > 1f)
		{
			transform.position = moveVector;
		}
		else
		{
			//Animation at start of Game
			transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
			transition += Time.deltaTime * 1 / animationDuration;


		}


	}
}
