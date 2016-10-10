using UnityEngine;
using System.Collections;
using System.Net.Sockets;
public class Config{

	public static string ipAddressServer = "127.0.0.1";
	public static int port = 12000;
	public static SocketType type = SocketType.Stream; //TCP
	public static ProtocolType protocolType = ProtocolType.Tcp;
	public static bool isBlocking = true;
}
