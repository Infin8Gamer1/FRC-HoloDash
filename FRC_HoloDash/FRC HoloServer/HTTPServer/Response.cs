using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FRC_HoloServer.Server
{
	public class Response
	{

		private byte[] data = null;
		private string status;
		private string mime;

		private Response(string status, string mime, byte[] data)
		{
			this.data = data;
			this.status = status;
			this.mime = mime;
		}

		public static Response From(Request request)
		{
			if (request == null)
			{
				return MakeMsgRequest("400.html", "400 Bad Request");

			} else if (request.Type == "GET") {

				string filePath = Environment.CurrentDirectory + HTTPServer.WEB_DIR + request.URL;
				FileInfo fileInfo = new FileInfo(filePath);

				//if the file doesn't exist then try to get the default page
				if (!fileInfo.Exists) {
					DirectoryInfo directoryInfo = new DirectoryInfo(filePath + "/");
					FileInfo[] files = directoryInfo.GetFiles();
					foreach (FileInfo file in files)
					{
						string name = file.Name;
						if (name.Contains("default.html") || name.Contains("index.html"))
						{
							fileInfo = file;
						}
					}
				}

				//make sure it got something otherwise just return page not found
				if (fileInfo.Exists)
				{
					return MakeHtmlRequest(fileInfo);
				} else {
					return MakeMsgRequest("404.html", "404 Page Not Found");
				}

			} else if (request.Type == "POST")
			{
				return null;
			} else {
				return MakeMsgRequest("405.html", "405 Method Not Allowed");
			}
		}

		private static Response MakeHtmlRequest(FileInfo fileInfo)
		{
			//convert the file to a byte array
			FileStream fileStream = fileInfo.OpenRead();
			BinaryReader reader = new BinaryReader(fileStream);
			byte[] outData = new byte[fileStream.Length];
			reader.Read(outData, 0, outData.Length);

			reader.Close();
			fileStream.Close();

			return new Response("200 OK", "text/html", outData);
		}

		private static Response MakeMsgRequest(string file, string status)
		{
			//load html file and convert it to a byte array to be sent with the response
			string fileName = Environment.CurrentDirectory + HTTPServer.MSG_DIR + file;
			FileInfo fileInfo = new FileInfo(fileName);
			FileStream fileStream = fileInfo.OpenRead();
			BinaryReader reader = new BinaryReader(fileStream);
			byte[] outData = new byte[fileStream.Length];
			reader.Read(outData, 0, outData.Length);

			reader.Close();
			fileStream.Close();

			return new Response(status, "text/html", outData);
		}

		public void Post(NetworkStream stream)
		{
			StreamWriter writer = new StreamWriter(stream);
			writer.Flush();

			writer.WriteLine(string.Format("{0} {1}\r\nContent-Length: {2}\r\nContent-Type: {3}\r\nServer: {4}\r\nAccept-Ranges: bytes\r\n",
				HTTPServer.VERSION,
				status,
				data.Length,
				mime,
				HTTPServer.NAME));

			stream.Write(data, 0, data.Length);
		}
	}
}
