using FRC_Holo.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

	public float timeBetweenUpdatesMS = 100f;
	private float timeBetween;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeBetween -= Time.deltaTime;

		if (timeBetween < 0)
		{
			NetworkUtil.GetInstance().UpdateNtTable();
			timeBetween = timeBetweenUpdatesMS;
		}
	}
}
