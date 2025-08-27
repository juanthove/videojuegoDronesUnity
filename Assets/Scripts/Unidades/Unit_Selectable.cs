using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Photon.Pun;
using Photon.Realtime;

public class Unit_Selectable : MonoBehaviour
{
    private Material contornoMaterial; // Referencia al material de contorno
    private Material originalMaterial;
    private Renderer rend;
    private bool objetoSeleccionado {get ; set;}
    [SerializeField]
    private string outlinedMaterial;

    private Unit_Settings unitConfig;

    //[SerializeField] PhotonView pv;


    // [PunRPC]
    // public void Inicializar(){
    //     pv = photonView;
    // }

    void Start(){
        objetoSeleccionado = false;
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;

        // Cargar el material de contorno desde Resources
        contornoMaterial = Resources.Load<Material>(outlinedMaterial);

        //Cargar datos de UnitSettings para ver si el jugador puede seleccionar la unidad
        unitConfig = GetComponent<Unit_Settings>();
    }

    void Update(){
        if (ClickNoEsUnidad() && objetoSeleccionado && !Input.GetKey(KeyCode.LeftControl))
        {       
                DeseleccionarObjeto(); 
        }

    }

    void OnMouseDown(){
        // if(PhotonNetwork.IsMasterClient  && unitConfig.GetEquipo() == Unit_Settings.enumEquipo.ROJO){
        //     SeleccionarObjeto();        
        // }else if(!PhotonNetwork.IsMasterClient  && unitConfig.GetEquipo() == Unit_Settings.enumEquipo.AZUL){
        //     SeleccionarObjeto();
        // }
        if(enabled){
            SeleccionarObjeto();
        }
    }

    void SeleccionarObjeto() {
        // Cambiar al material de contorno cuando se selecciona el objeto
        rend.material = contornoMaterial;
        objetoSeleccionado = true;
        TransformarHijos();
    }

    void DeseleccionarObjeto(){
        // Restaurar el material original cuando se deselecciona el objeto
        rend.material = originalMaterial;
        objetoSeleccionado = false;
        TransformarHijos();
    }

    public bool GetObjetoSeleccionado(){
        return objetoSeleccionado;
    }

    //Ver si estoy tocando algo que no sea la unidad
    public bool ClickNoEsUnidad(){
        if(Input.GetMouseButtonDown(0)){
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(rayo, out hit)){
                if(hit.collider.gameObject != gameObject){
                    return true;
                }
            }
        }
        return false;  
    }

    public void TransformarHijos(){
        foreach(Transform child in transform){
            Renderer renderer = child.GetComponent<Renderer>();
            if (renderer != null) {
                if(objetoSeleccionado) renderer.material = contornoMaterial;
                else renderer.material = originalMaterial;
            }else{
            }
        }
    }

}