using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
	public Transform lookObj = null;
	void OnAnimatorIK()
	{
		Animator animator = GetComponent<Animator>();
		animator.SetLookAtWeight(1.0f, 0.5f, 0.8f, 1.0f, 0.3f);
		animator.SetLookAtPosition(lookObj.position);
	}
}
