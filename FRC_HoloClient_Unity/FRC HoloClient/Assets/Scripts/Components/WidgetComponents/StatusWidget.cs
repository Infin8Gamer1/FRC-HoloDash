using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FRC_Holo.API;

public class StatusWidget : MonoBehaviour {

	public TextMeshPro Text;
	public Renderer PlaneRenderer;

	public string Key;
	public string Label;

	// Use this for initialization
	void Start () {
		PlaneRenderer.material.color = Color.red;
		Text.text = Label;

		//subscribe to the network updated ring of trust
		NetworkUtil.GetInstance().networkUpdatedHandler += OnNetworkUpdate;
	}

	//update method
	private void OnNetworkUpdate(object sender, NetworkUpdatedEvent e)
	{
		string value = NetworkUtil.GetInstance().GetKey(Key).ToString();

		if (value == "BAD" || value == "" || value == null)
		{
			PlaneRenderer.material.color = Color.red;
		}
		else if (value == "OK")
		{
			PlaneRenderer.material.color = Color.yellow;
		}
		else if (value == "GOOD")
		{
			PlaneRenderer.material.color = Color.green;
		}
	}
}
