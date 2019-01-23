using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.IO;

public class WebStream : MonoBehaviour
{

	public MeshRenderer frame;    //Mesh for displaying video
	public string sourceURL = "http://infinitepc:1181/video.cgi";

	private Texture2D texture;
	private Stream stream;
	private WebStreamState streamState;

	// Use this for initialization
	void Start()
	{
		GetVideo();
	}

	private void FixedUpdate()
	{
		//if the stream stops then try to connect again
		if(streamState == WebStreamState.EOS)
		{
			GetVideo();
		}
	}

	public void GetVideo()
	{
		streamState = WebStreamState.Streaming;

		texture = new Texture2D(2, 2);
		// create HTTP request
		HttpWebRequest req = (HttpWebRequest)WebRequest.Create(sourceURL);
		//Optional (if authorization is Digest)
		//req.Credentials = new NetworkCredential("username", "password");
		// get response
		WebResponse resp = req.GetResponse();
		// get response stream
		stream = resp.GetResponseStream();
		StartCoroutine(GetFrame());
	}

	IEnumerator GetFrame()
	{
		Byte[] JpegData = new Byte[100000];

		while (true)
		{
			int bytesToRead = FindLength(stream);
			//print(bytesToRead);
			if (bytesToRead == -1)
			{
				print("End of stream");
				streamState = WebStreamState.EOS;
				yield break;
			}

			int leftToRead = bytesToRead;

			while (leftToRead > 0)
			{
				//print(leftToRead);

				try
				{
					leftToRead -= stream.Read(JpegData, bytesToRead - leftToRead, leftToRead);
				} catch (ObjectDisposedException e)
				{
					print(e.Message);
					leftToRead = 0;
					streamState = WebStreamState.EOS;
					yield break;
				}
				
				yield return null;
			}

			MemoryStream ms = new MemoryStream(JpegData, 0, bytesToRead, false, true);

			texture.LoadImage(ms.GetBuffer());
			frame.material.mainTexture = texture;
			stream.ReadByte(); // CR after bytes
			stream.ReadByte(); // LF after bytes
		}
	}

	int FindLength(Stream stream)
	{
		int b;
		string line = "";
		int result = -1;
		bool atEOL = false;

		while ((b = stream.ReadByte()) != -1)
		{
			if (b == 10) continue; // ignore LF char
			if (b == 13)
			{ // CR
				if (atEOL)
				{  // two blank lines means end of header
					stream.ReadByte(); // eat last LF
					return result;
				}
				if (line.StartsWith("Content-Length:"))
				{
					result = Convert.ToInt32(line.Substring("Content-Length:".Length).Trim());
				}
				else
				{
					line = "";
				}
				atEOL = true;
			}
			else
			{
				atEOL = false;
				line += (char)b;
			}
		}
		return -1;
	}
}

public enum WebStreamState
{
	Streaming,
	EOS
}