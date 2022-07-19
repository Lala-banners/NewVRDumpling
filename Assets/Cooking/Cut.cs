using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut : MonoBehaviour
{
	public Rigidbody rb;
	//public int sliceIndex;
	
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.constraints = RigidbodyConstraints.FreezeAll;
		rb.useGravity = false;
	}

	private void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.name == "Butcher Knife")
		{
			rb.constraints = RigidbodyConstraints.None;
			rb.useGravity = true;
			transform.parent = null;
		}
		
		//ResetRigidbody();
	}

	public void ResetRigidbody()
	{
		//sliceIndex = 0;
		rb.constraints = RigidbodyConstraints.None;
		rb.useGravity = true;
	}
}
