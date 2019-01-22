using FRC_Holo.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using Urho.Gui;
using Urho.Physics;
using System.IO;
using Urho.Resources;
using MjpegProcessor;
using System.Runtime.InteropServices.WindowsRuntime;
using Urho.Urho2D;

namespace FRC_HoloClient
{
	class CameraWidget : Component
	{
		public Urho.Shapes.Plane plane;

		public string URL;

		private MjpegDecoder mjpegDecoder;

		public CameraWidget(string URL, Urho.Shapes.Plane plane)
		{
			ReceiveSceneUpdates = false;
			this.URL = URL;
			this.plane = plane;
		}

		//called when the component is attached to some node
		public override void OnAttachedToNode(Node _node)
		{
			/*mjpegDecoder = new MjpegDecoder();
			mjpegDecoder.FrameReady += Mjpeg_FrameReady;
			//start parsing
			mjpegDecoder.ParseStream(new Uri(URL));*/
		}

		private async void Mjpeg_FrameReady(object sender, FrameReadyEventArgs e)
		{
			/*MemoryStream memoryStream = new MemoryStream();
			await memoryStream.WriteAsync(e.FrameBuffer.ToArray(), 0, Convert.ToInt32(e.FrameBuffer.Length));

			MemoryBuffer mb = new MemoryBuffer(memoryStream);

			Texture2D texture = new Texture2D();
			texture.Load(mb);

			var material = new Material();
			material.SetTexture(TextureUnit.Diffuse, texture);
			material.SetTechnique(0, CoreAssets.Techniques.Diff);
			plane.SetMaterial(material);*/
		}

	}
}
