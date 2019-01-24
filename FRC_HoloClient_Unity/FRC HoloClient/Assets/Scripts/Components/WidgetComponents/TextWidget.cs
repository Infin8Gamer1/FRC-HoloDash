using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FRC_Holo.API;

public class TextWidget : MonoBehaviour {

	public TextMeshPro Text;

	public string Key;
	public string Label;

	// Use this for initialization
	void Start () {
		//subscribe to network updates
		NetworkUtil.GetInstance().networkUpdatedHandler += OnNetworkUpdate;

		Text.text = Label + "\nLoading...";
	}

	//update method
	private void OnNetworkUpdate(object sender, NetworkUpdatedEvent e)
	{
		string value = NetworkUtil.GetInstance().GetKey(Key).ToString();

		Text.text = Label + "\n" + value;
	}
}
