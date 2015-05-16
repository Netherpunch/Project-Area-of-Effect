using UnityEngine;
using System.Collections;

public class AnimationFSM : MonoBehaviour {

	public float range = 1.5f;	//Range that the character will stop moving towards the destination

	public AnimationClip idleClip;
	public AnimationClip walkClip;
	public AnimationClip runClip;
	public AnimationClip attackClip;
	public AnimationClip dieClip;
	
	public enum AnimationState
	{
		idle,
		run,
		attack,
		die
	}

	public AnimationState aniamtionState;


	// Use this for initialization
	void Start ()
	{
		aniamtionState = AnimationState.idle;
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (aniamtionState)
		{
		case AnimationState.idle:
				IdleState();
			break;
		case AnimationState.run:
				RunState();
			break;
		case AnimationState.die:
				Die();
			break;
		}
	}

	private void IdleState()
	{
		GetComponent<Animation>().CrossFade(idleClip.name);
	}

	private void RunState()
	{
		if(Vector3.Distance(transform.position, GetComponent<NavMeshAgent>().destination) < range)
		{
			aniamtionState = AnimationState.idle;
		}

		GetComponent<Animation>().CrossFade(walkClip.name);
	}

	private void Die()
	{
		GetComponent<Animation>().CrossFade(dieClip.name);
	}
}
