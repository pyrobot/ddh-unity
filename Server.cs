using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class Server : MonoBehaviour {
	
	static Server _this;
	
	void Awake () {
		_this = this;	
	}
	
	void Start () {
		BeginConnect("guildbros.com", 1337);
	}
	
	Socket thisSocket;
	
	public static void Send(Byte[] bytes) {
		_this.thisSocket.BeginSend(bytes, 0, bytes.Length, 0, SendCallback, 0);
	}
	
	public static void SendCallback(IAsyncResult ar) {
		// Debug.Log ("sent");
	}
	
	public static void ConnectCallback(IAsyncResult ar)
	{		
	    _this.thisSocket = (Socket) ar.AsyncState;
	    _this.thisSocket.EndConnect(ar);
	}
	
	public static void BeginConnect(string host, int port)
	{
	    IPAddress[] IPs = Dns.GetHostAddresses(host);
	
		
	    Socket s = new Socket(AddressFamily.InterNetwork,
	        SocketType.Stream,
	        ProtocolType.Tcp);
	    s.BeginConnect(IPs[0], port, 
	        new AsyncCallback(ConnectCallback), s);

	}		
}