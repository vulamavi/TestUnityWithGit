using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using UnityEngine;
public class SocketGame{
	string ipAddress;
	int port;
	Socket socket;

	private static ManualResetEvent sendDone = new ManualResetEvent(false);
	public SocketGame(string ipAddress, int port) {
		this.ipAddress = ipAddress;
		this.port = port;
		socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
	}

	public Socket GetSocket(){
		return this.socket;
	}

	public void Connect(){
		socket.Connect (ipAddress, port);
	}
	//socket listening
	public void Listen(){
		try{
			
			socket.Bind (new IPEndPoint(IPAddress.Any, 12345));
			socket.Listen (100);
		}
		catch(Exception e){
			Debug.Log (e.Message);
		}
	}

	public Socket Accept(){
		Socket s = socket.Accept ();
		return s;
	}

	public void Send(Package package) {
		byte[] serializedMessage;
		var formatter = new BinaryFormatter();
		using (var stream = new MemoryStream())
		{
			formatter.Serialize(stream, (object)package);
			serializedMessage = stream.ToArray();
		}
		for (int i = 0; i < serializedMessage.Length; i++) {
			Debug.Log (" " + serializedMessage[i]);
		}
		Debug.Log ("serializedMessage : " + serializedMessage.Length);
//		socket.BeginSend(serializedMessage, 0, serializedMessage.Length, 0,
//			new AsyncCallback(SendCallback), socket);
		socket.Send (serializedMessage);
	}

	private static void SendCallback(IAsyncResult ar) {
		Debug.Log ("start callback..");

		try {
			// Retrieve the socket from the state object.
			Socket client = (Socket) ar.AsyncState;

			// Complete sending the data to the remote device.
			int bytesSent = client.EndSend(ar);
			Console.WriteLine("Sent {0} bytes to server.", bytesSent);

			// Signal that all bytes have been sent.
			sendDone.Set();
		} catch (Exception e) {
			Console.WriteLine(e.ToString());
		}
	}

	byte[] serializedMessages;
	public void Recv(Package package) {
		socket.Receive (serializedMessages);
		using (var stream = new MemoryStream())
		{
			var formatter = new BinaryFormatter();
			stream.Write(serializedMessages, 0, serializedMessages.Length);
			stream.Seek(0, SeekOrigin.Begin);
			package = (Package)formatter.Deserialize(stream);
		}
		Debug.Log ("Dkkm , nhan dc roi");
	}

	void Close() {
		socket.Close ();
	}
}
