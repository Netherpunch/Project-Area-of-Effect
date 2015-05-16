using UnityEngine;
using System.Collections;

public class Base_Enemy : MonoBehaviour
{

	private int _curHealth;
	public int maxHealth;

	public float threatRange;
	public float combatRange;

	public float speed;

	private void Start()
	{
		_curHealth = maxHealth;
	}

	public void TakeDamage(int damageAmount)
	{
		_curHealth = _curHealth - damageAmount;
	}
}
