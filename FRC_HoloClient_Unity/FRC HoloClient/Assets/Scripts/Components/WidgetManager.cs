using FRC_HoloClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetManager : MonoBehaviour {

	public WidgetGrid Grid;

	public GameObject Text;

	// Use this for initialization
	void Start () {
		Grid = new WidgetGrid(true);

		//add nodes for each widget in the grid as children from this node
		foreach (Widget widget in Grid.widgets)
		{
			AddWidget(widget);
		}
	}
	
	private void AddWidget(Widget widget)
	{
		GameObject widgetGO = new GameObject(widget.type.ToString() + ": " + widget.NetworkKey);
		widgetGO.transform.SetParent(gameObject.transform);
		widgetGO.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		widgetGO.transform.localPosition = new Vector3(widget.Position[0] * 0.5f, widget.Position[1] * 0.5f, widget.Position[2] * 0.5f);

		//Background Plane
		GameObject backgroundPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
		backgroundPlane.transform.SetParent(widgetGO.transform);

		backgroundPlane.transform.localScale = new Vector3(0.97f, 0.48f, 0.48f);
		backgroundPlane.transform.localRotation = Quaternion.Euler(90, 0, 180);
		backgroundPlane.transform.localPosition = new Vector3(0, 0, 0.05f);

		MeshRenderer planeRenderer = backgroundPlane.GetComponent<MeshRenderer>();
		planeRenderer.material.color = new Color(0.4f, 0.4f, 0.4f, 0.75f);

		//Text Component
		GameObject TextGO = Instantiate(Text, widgetGO.transform, false);
		TextGO.transform.localPosition = new Vector3(0, 0, -0.05f);
		TextGO.transform.localRotation = Quaternion.identity;


		switch (widget.type)
		{
			case WidgetType.Text:
				//Text Widget Display
				TextWidget textWidget = widgetGO.AddComponent<TextWidget>();
				textWidget.Text = TextGO.GetComponent<TMPro.TextMeshPro>();
				textWidget.Key = widget.NetworkKey;
				textWidget.Label = widget.Label;
				break;
			case WidgetType.Camera:
				//remove text
				Destroy(TextGO);

				//Camera Widget
				WebStream webStream = widgetGO.AddComponent<WebStream>();
				webStream.sourceURL = widget.NetworkKey;
				webStream.frame = planeRenderer;
				break;
			case WidgetType.Status:
				//adjust the size of the background plane
				backgroundPlane.transform.localScale = new Vector3(0.5f, 0.25f, 0.25f);

				//Status Widget
				StatusWidget statusWidget = widgetGO.AddComponent<StatusWidget>();
				statusWidget.Text = TextGO.GetComponent<TMPro.TextMeshPro>();
				statusWidget.PlaneRenderer = planeRenderer;
				statusWidget.Key = widget.NetworkKey;
				statusWidget.Label = widget.Label;
				break;
		}

		
	}
}
