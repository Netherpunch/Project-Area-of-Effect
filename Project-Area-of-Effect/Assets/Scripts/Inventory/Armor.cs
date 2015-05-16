using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Armor : Item
{
	
	public List<Item> armorInspector;
	public static List<Item> armor;

	void Start()
	{
		armor = armorInspector;
	}
}
