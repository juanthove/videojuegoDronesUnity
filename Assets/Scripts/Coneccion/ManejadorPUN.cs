using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ManejadorPUN : MonoBehaviourPunCallbacks
{
    public static ManejadorPUN instancia { get; private set; }
    public DatosPartida datosP;

    private void Awake(){
        instancia = this;
        DontDestroyOnLoad(instancia);
        datosP = GameObject.FindObjectOfType<DatosPartida>();
        //FindObjectOfType<DatosPartida>();
    }

    private void Start(){
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster(){
        PhotonNetwork.JoinLobby();
	}

    internal void CrearRoom(string nombre){
        RoomOptions opciones = new RoomOptions(){
            MaxPlayers = 2
        };

        PhotonNetwork.CreateRoom(nombre, opciones);
    }

    internal void UnirseRoom(string nombre){
        PhotonNetwork.JoinRoom(nombre);
    }

    public override void OnDisconnected(DisconnectCause causa){
        PhotonNetwork.LoadLevel("Menu");
    }

    public void cargarD(string nombre, string partida){
        datosP.cargarDatos(nombre, partida);
        
        
    }



    [PunRPC]
    public void LanzarEscena(string escena){
        PhotonNetwork.LoadLevel(escena);
    }
}
