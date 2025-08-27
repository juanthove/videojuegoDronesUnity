using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ManejadorJuego : MonoBehaviourPun
{
    public static ManejadorJuego instancia;

    [SerializeField] JugadorControlador jugadorA;
    [SerializeField] JugadorControlador jugadorB;

    public JugadorControlador JugadorA { get { return jugadorA; }}
    public JugadorControlador JugadorB { get { return jugadorB; }}


    private void Awake()
    {
        instancia = this;
    }


    // Start is called before the first frame update
    void Start()
    {

        string txt = PlayerPrefs.GetString("Cargada");
        if(txt == "true"){
            if(PhotonNetwork.IsMasterClient){
                ConfigurarJugadoresCargada();
            }
        }else{
            Debug.Log("Cargada=false");
            if(PhotonNetwork.IsMasterClient){
                ConfigurarJugadores();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ConfigurarJugadores(){
        jugadorA.photonView.TransferOwnership(1);
        jugadorB.photonView.TransferOwnership(2);

        jugadorA.photonView.RPC("Inicializar", RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(1), 6);
        jugadorB.photonView.RPC("Inicializar", RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(2), 6);
    }

    public JugadorControlador ObtenerOtroJugador(JugadorControlador jugadorActual){
        return jugadorActual == jugadorA ? jugadorB : jugadorA;
    }

    private void ConfigurarJugadoresCargada(){
        Debug.Log("Se cargó la partida");
        jugadorA.photonView.TransferOwnership(1);
        jugadorB.photonView.TransferOwnership(2);

        jugadorA.photonView.RPC("CargarUnidades", RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(1));
        jugadorB.photonView.RPC("CargarUnidades", RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(2));
    }


}
