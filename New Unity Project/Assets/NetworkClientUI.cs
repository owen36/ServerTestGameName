using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine;

public class NetworkClientUI : MonoBehaviour {

	static NetworkClient client;

	void OnGUI()
    {
        string ipaddress = Network.player.ipAddress;
        GUI.Box(new Rect(10,Screen.height - 50, 100,50),ipaddress);
        GUI.Label(new Rect(20,Screen.height - 30, 100,20), "Status:" + client.isConnected);

        if(!client.isConnected)
        {
        	if(GUI.Button(new Rect(10,10,60,50),"Connect"))
        	{
        		Connect();
        	}
        }
    }

	// Use this for initialization
	void Start () {
		client = new NetworkClient();
    }

	void Connect()
	{
		client.Connect("192.168.1.3", 25000);
	}

	static public void SendJoystickInfo(float hDelta, float vDelta)
	{
		if(client.isConnected)
		{
			StringMessage msg = new StringMessage();
			msg.value = hDelta + "|" + vDelta;
			client.Send(888, msg);
		}
	}

	// Update is called once per frame
	void Update ()
    {

	}
}

