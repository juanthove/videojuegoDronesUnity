using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class ManejadorVictoria : MonoBehaviourPun
{
    [SerializeField] TextMeshProUGUI txtNombre;
    // Start is called before the first frame update
    void Start()
    {
        photonView.RPC("cambiarNombre", RpcTarget.All);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    private void cambiarNombre(){
        txtNombre.text=PlayerPrefs.GetString("NombreJugador");
    }
}
