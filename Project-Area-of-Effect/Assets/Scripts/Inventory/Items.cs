using UnityEngine;
using System.Collections.Generic;

public class Items : MonoBehaviour 
{
	public List<Armor> armorInspector;
	private static List<Armor> armor;

	void Start()
	{
		armor = armorInspector;
	}

	public static Armor GetArmor(int id)
	{

		Armor armor = new Armor ();

		armor.image = Items.armor [id].image;
		armor.width = Items.armor [id].width;
		armor.height = Items.armor [id].height;

		return armor;
	}
}
