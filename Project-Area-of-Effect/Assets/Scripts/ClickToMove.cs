using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour 
{

	public LayerMask TerrainLayerMask;

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton(0))
		{
			GenerateLocation();
		}
	}
	
	void GenerateLocation ()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if(Physics.Raycast(ray, out hit, 100, TerrainLayerMask))
		{
			GetComponent<NavMeshAgent>().SetDestination(hit.point);
			GetComponent<AnimationFSM>().aniamtionState = AnimationFSM.AnimationState.run;
		}
	}
}
