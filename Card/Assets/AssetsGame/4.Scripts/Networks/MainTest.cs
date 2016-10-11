using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Threading;

public class MainTest : MonoBehaviour {
	SocketGame sock;
	Socket ss;

	// Use this for initialization
	void Start () {
		string strIP = "127.0.0.1";
		sock = new SocketGame (strIP, 12345);
		sock.Connect ();
		Invoke ("SendMess", 1);
	}

	void SendMess(){
		Package pac = new Package ();
		string strSend = "send data12";
		Debug.Log ("start Sending..");
		pac.CreatePackage (strSend.Length, 10, strSend.ToCharArray ());
		sock.Send (pac);
	}

	void FixedUpdate(){
		
	}

	IEnumerator WaitingClient(SocketGame socket){
		socket.Accept ();
		yield return new WaitForSeconds (1);

	}
}

public class ServerThread{
	public ServerThread(Socket clientSocket) {
		System.Threading.Thread myThread;
		myThread = new System.Threading.Thread(new 
			System.Threading.ThreadStart(ThreadHandler));
		myThread.Start ();
	}

	void ThreadHandler(){
		Debug.Log ("starting Thread socket...");
	}
}
