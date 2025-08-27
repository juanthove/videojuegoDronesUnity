using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Misil_Trayectoria : MonoBehaviour
{
    public Transform objetivo; // El objetivo hacia el que se dirige el misil
    public float velocidad = 20f; // Velocidad de movimiento del misil
    public float rotacionSuave = 1f; // Suavidad de la rotación del misil
    private GameObject unidadLanzadora; //Referencia a la unidad que lanza el misil

    //[SerializeField] PhotonView pv;

    // [PunRPC]
    // public void Inicializar(){

    //     pv = gameObject.GetComponent<PhotonView>();
    // }

    void start(){
        //lanzadorCollider = GetComponent<Collider>;
    }

    void Update()
    {
        if (objetivo == null)
        {
            if(unidadLanzadora.GetComponent<Unit_Settings>() != null){
                unidadLanzadora.GetComponent<Unit_Settings>().DevolverMisil();
            }
            
            Destroy(gameObject); // Destruye el misil si el objetivo ya no está disponible
            return;
        }

        // Calcula la dirección hacia el objetivo
        Vector3 direccion = (objetivo.position - transform.position).normalized;

        // Calcula la rotación deseada hacia el objetivo
        Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);

        // Suaviza la rotación del misil hacia el objetivo
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, rotacionSuave * Time.deltaTime);

        // Calcula el movimiento hacia adelante
        Vector3 movimiento = transform.forward * velocidad * Time.deltaTime;

        // Aplica el movimiento al misil
        transform.Translate(movimiento, Space.World);
    }

    // Método para establecer el objetivo del misil
    public void EstablecerObjetivo(Transform nuevoObjetivo, GameObject lanzador)
    {
        objetivo = nuevoObjetivo;
        unidadLanzadora = lanzador;
    }

    public bool compararPosiciones(){
        if(Mathf.FloorToInt(objetivo.position.x) == Mathf.FloorToInt(transform.position.x) &&
                Mathf.FloorToInt(objetivo.position.z) == Mathf.FloorToInt(transform.position.z)){
                    return true;
        }
        return false;
    }

    public GameObject GetUnidadLanzadora(){
        return unidadLanzadora;
    }

    public Transform GetObjetivo(){
        return objetivo;
    }
}
