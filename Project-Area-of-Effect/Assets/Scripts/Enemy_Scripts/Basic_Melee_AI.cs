using UnityEngine;
using System.Collections;

public class Basic_Melee_AI : Base_Enemy
{

	private AnimationFSM _animationFSM;
	private NavMeshAgent _navMeshAgent;

	public GameObject targetObject;

	public enum CharacterState
	{
		idle,
		simpleMove,
		pathMove,
		attacking,
		die
	}

	public CharacterState characterState;

	// Use this for initialization
	void Start ()
	{
		targetObject = GameObject.FindGameObjectWithTag ("Player");
		_navMeshAgent = GetComponent<NavMeshAgent> ();
		_animationFSM = GetComponent<AnimationFSM> ();

		if (_navMeshAgent.stoppingDistance != combatRange)
		{
			_navMeshAgent.stoppingDistance = combatRange;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (characterState)
		{
			case CharacterState.idle:
				Idle();
			break;
		case CharacterState.simpleMove:
				SimpleMove();
			break;
		case CharacterState.pathMove:
				PathMove();
			break;
		case CharacterState.attacking:
				Attack();
			break;
		case CharacterState.die:
				Die();
			break;
		}
	}

	private void Idle()
	{
		_animationFSM.aniamtionState = AnimationFSM.AnimationState.idle;

		if (GetComponent<Animation> () [_animationFSM.idleClip.name].time < 0.9f)
		{
			Debug.Log("animation is done");
		}

		if (targetObject != null)
		{
			if(Vector3.Distance (transform.position, targetObject.transform.position) > combatRange)
			{
				characterState = CharacterState.pathMove;
			}
		}
	}

	private void SimpleMove()
	{
		_animationFSM.aniamtionState = AnimationFSM.AnimationState.run;
	}

	private void PathMove()
	{
		_animationFSM.aniamtionState = AnimationFSM.AnimationState.run;

		_navMeshAgent.destination = targetObject.transform.position;

		if (Vector3.Distance (transform.position, targetObject.transform.position) < combatRange)
		{
			characterState = CharacterState.idle;
		}
	}

	private void Attack()
	{
		_animationFSM.aniamtionState = AnimationFSM.AnimationState.attack;
	}

	private void Die()
	{
		//plz add die thank
	}
}
