using FRC_Holo.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

	public float timeBetweenUpdatesMS = 100f;
	private float timeBetween = 0.03f;

	// Use this for initialization
	void Start () {
		timeBetween = 0.03f;
		timeBetweenUpdatesMS = timeBetweenUpdatesMS * 0.001f;
	}
	
	// Update is called once per frame
	void Update () {
		timeBetween -= Time.deltaTime;

		if (timeBetween < 0)
		{
			StartCoroutine(NetworkUtil.GetInstance().UpdateNtTable());
			timeBetween = timeBetweenUpdatesMS;
		}
	}
}
