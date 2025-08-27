using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConectMaster : MonoBehaviourPunCallbacks
{
	
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

	public override void OnConnectedToMaster(){
        PhotonNetwork.AutomaticallySyncScene = true;
		Debug.Log("Connected to Master");
	}
}
