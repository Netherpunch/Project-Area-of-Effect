using UnityEngine;
using System.Collections;

/*
 * @author SoftRare - www.softrare.eu
 * This class is part of a demo scene of the package uMMO.
 * You may only use and/or change this code if you purchased it in a legal way: From the official Unity Asset Store or directly from the author SoftRare.
 */
public class CoinSpawnPoint : MonoBehaviour {
	
	public Coin coin;
	
	public bool hasExistingCoin() {
		
		return (coin == null?false:true);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
