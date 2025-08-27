using UnityEngine;
using Proyecto26;
using TMPro; 
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using Photon.Pun;
using Photon.Realtime;


public class DatosPartida : MonoBehaviour
{
  
    private string nombreAtacante;
    private string nombreDefensa;
    private string nombrePartida;
    [SerializeField] Button btnGuardar;
   

    //Variables para Atacante
    public List<DatosDrone> listaDatosDrone =new List<DatosDrone>();
    private List<GameObject> dronesEnEscena = new List<GameObject>();


    // Variables para los objetos de defensa
    private GameObject torretaLazer;
    private GameObject cañonBofors;
    private GameObject barteriaMisiles;
    private GameObject radarEstatico;
    private GameObject radarMovil;
    private GameObject central;
    public  DatosTorretaLazer dataTorreta;
    public DatosBofors dataBofors;
    public DatosBateriaMisiles dataBateriaMisiles;
    public DatosRadarFijo dataRadarFijo;
    public DatoRadarMovil1 dataRadarMovil;
    public DatosCentral dataCentral;
    public numeroPartida dataPartida;
    
    public DatosPartida(List<DatosDrone> dd, DatosTorretaLazer dt, DatosBofors db,DatosBateriaMisiles dbm, 
    DatosRadarFijo drf, DatoRadarMovil1 drm, DatosCentral dc)
    {
       if(dd!=null)
            this.listaDatosDrone  =  dd;
       if(dt!=null)
            this.dataTorreta = dt;
       if(db!=null) 
            this.dataBofors = db;
        if(dbm!=null)
            this.dataBateriaMisiles = dbm;
        if(drf!=null)
            this.dataRadarFijo = drf;
        if(drm!=null)
            this.dataRadarMovil = drm;
        if(dc!=null)
            this.dataCentral = dc;
    }

    void Awake(){
        // Asegúrate de que el botón esté asignado
        if (btnGuardar != null)
        {
            //Agrega el método al evento onClick del botón
            btnGuardar.onClick.AddListener(OnClickGuardar);
        }
        else
        {
            Debug.LogWarning("El botón no está asignado en el Inspector.");
        }
    }

    private void OnClickGuardar(){
        if(PhotonNetwork.IsMasterClient){
            actualizarPartida(PhotonNetwork.MasterClient.NickName, PhotonNetwork.CurrentRoom.Name);
        }
    }

    public void actualizarPartida(string nomJ, string nomP) 
    {
        
       
         if (listaDatosDrone == null) {
        listaDatosDrone = new List<DatosDrone>(); 
        }

        listaDatosDrone.Clear();

         if (dronesEnEscena == null) {
        dronesEnEscena = new List<GameObject>();
        }

           Debug.Log("Llego");
        GameObject[] dronesArray = GameObject.FindGameObjectsWithTag("Drone");
        dronesEnEscena.AddRange(dronesArray);
        int i=0;
           Debug.Log("Llego");
         foreach (GameObject drone in dronesEnEscena)
        {
               Debug.Log("Llego");
          int vidaDron = drone.GetComponent<Unit_Settings>().GetSalud();
          int municion  = drone.GetComponent<Unit_Settings>().CantidadMunicion();
          Vector3 pos = drone.GetComponent<Unit_Settings>().getPosicion();
          string nombreDrone  = "Drone" + i;
          //Buscar el getNumero
          DatosDrone data = new DatosDrone(vidaDron,  pos, municion, nombreDrone, drone.GetComponent<Unit_Settings>().GetNumeroDrone());
          listaDatosDrone.Add(data);
            i++;
        }

         dataPartida = new numeroPartida(1, i);
           Debug.Log("Termino drone");

          torretaLazer = GameObject.FindWithTag("torretaLazer");
      
        if(torretaLazer!= null){
            Debug.Log("Entro torreta Lazer");
            int vidaLazer = torretaLazer.GetComponent<Unit_Settings>().GetSalud();
            int municionLazer  = torretaLazer.GetComponent<Unit_Settings>().CantidadMunicion();
            Vector3 posLazer = torretaLazer.GetComponent<Unit_Settings>().getPosicion();
            string numeroTorretaLazer = "CañonLazer" ; 
            dataTorreta = new DatosTorretaLazer(vidaLazer,  posLazer,municionLazer, numeroTorretaLazer);
        }
        
         Debug.Log("Termino Lazer");

         cañonBofors =  GameObject.FindWithTag("cañonBofors");

        if(cañonBofors!=null)
        {
            int vidaBofors = cañonBofors.GetComponent<Unit_Settings>().GetSalud();
            int municionBofors  = cañonBofors.GetComponent<Unit_Settings>().CantidadMunicion();
            Vector3 posBofors = cañonBofors.GetComponent<Unit_Settings>().getPosicion();
            string numeroBofors = "CañonBofors" ; 
            dataBofors = new DatosBofors(vidaBofors,  posBofors,municionBofors, numeroBofors);
        }


        Debug.Log("Termino bofors");

        barteriaMisiles = GameObject.FindWithTag("bateriaMisiles");
        
        if(barteriaMisiles!=null)
        {
            int vidaBateria = barteriaMisiles.GetComponent<Unit_Settings>().GetSalud();
            int municionBateria  = barteriaMisiles.GetComponent<Unit_Settings>().CantidadMunicion();
            Vector3 posBateria = barteriaMisiles.GetComponent<Unit_Settings>().getPosicion();
            string numeroBateria = "BateriaMisiles" ;         
            dataBateriaMisiles = new DatosBateriaMisiles(vidaBateria, posBateria, municionBateria, numeroBateria);
        }
        
        Debug.Log("Termino bateria");

         radarEstatico =  GameObject.FindWithTag("radarFijo");
        if(radarEstatico!=null)
        {
            int vidaRadarFijo = radarEstatico.GetComponent<Unit_Settings>().GetSalud();
            Vector3 posRadarFijo = radarEstatico.GetComponent<Unit_Settings>().getPosicion();
            string nombreRadarFijo = "RadarFijo" ;         
            dataRadarFijo = new DatosRadarFijo(vidaRadarFijo, posRadarFijo, nombreRadarFijo);
        }

        Debug.Log("Termino radarEstatico");

        radarMovil =  GameObject.FindWithTag("radarMovil");
        if(radarMovil!=null)
        {
            int vidaRadarMovil = radarMovil.GetComponent<Unit_Settings>().GetSalud();
            Vector3 posRadarMovil  = radarMovil.GetComponent<Unit_Settings>().getPosicion();
            string nombreRadarMovil = "RadarMovil" ;         
            dataRadarMovil = new DatoRadarMovil1(vidaRadarMovil, posRadarMovil, nombreRadarMovil); 

        }

        Debug.Log("Termino radarMovil");

        central =  GameObject.FindWithTag("central");
                Debug.Log("Holanada");
        if(central!=null)
        {
             Debug.Log("2");
            int vidaCentral = central.GetComponent<Unit_Settings>().GetSalud();
            Vector3 posCentral  = central.GetComponent<Unit_Settings>().getPosicion();
            string nombreCentral = "Central" ;        
            dataCentral = new DatosCentral(vidaCentral, posCentral, nombreCentral); 
        }
        Debug.Log("Termino central");

         salvar(nomJ, nomP,   listaDatosDrone, dataTorreta, dataBofors, dataBateriaMisiles, dataRadarFijo, dataRadarMovil, dataCentral, dataPartida);
    }

    public void salvar(string nombreJugador, string nombrePartidas,List<DatosDrone> drones, DatosTorretaLazer data, DatosBofors dataBofors, 
    DatosBateriaMisiles dataBateriaMisiles, DatosRadarFijo dataRadarFijo, DatoRadarMovil1 dataRadarMovil, DatosCentral dataCentral, numeroPartida dataPartida){
        
        string firebaseURL = $"https://proyectoude-5c466-default-rtdb.firebaseio.com/partidas/{nombreJugador}/{nombrePartidas}.json";
        string firebaseURL2 = $"https://proyectoude-5c466-default-rtdb.firebaseio.com/partidas/{nombreJugador}/{nombrePartidas}";
        string firebaseURL3 = $"https://proyectoude-5c466-default-rtdb.firebaseio.com/partidas/{nombreJugador}.json";

        //int cantDrones = dataPartida.getNumeroDrones();
        // Eliminar todos los datos de drones antiguos asociados al atacante
        RestClient.Delete(firebaseURL).Then(response =>
        {
             
             RestClient.Put(firebaseURL3, dataPartida).Then(response =>
             {
                   Debug.Log("Datos del drone agregados correctamente");
             }).Catch(error =>
             {
                 Debug.LogError("Error al agregar datos del drone: " + error.Message);
             });



            //Drone
            foreach (DatosDrone drone in drones)
            {
                string droneURL = $"{firebaseURL2}/{drone.getNombre()}.json";
                DatosDrone nuevoDrone = new DatosDrone(drone.getVida(), drone.getPos(), drone.getCantMisiles(), drone.getNombre(), drone.getNumero());
                Debug.Log("URL drone : " + droneURL);
                if(nuevoDrone!=null)
                {
                    RestClient.Put(droneURL, nuevoDrone).Then(response =>
                    {
                        Debug.Log("Datos del drone agregados correctamente");
                    }).Catch(error =>
                    {
                        Debug.LogError("Error al agregar datos del drone: " + error.Message);
                    });
                }
            }

            //Lazer
            if(data!=null)
            {
                string LazerURL = $"{firebaseURL2}/{data.getNumero()}.json";
                Debug.Log("URL Lazer aaaa : " + LazerURL);
                RestClient.Put(LazerURL, data).Then(response =>
                {
                    Debug.Log("Datos del lazer agregados correctamente");
                }).Catch(error =>
                {
                    Debug.LogError("Error al agregar datos del drone: " + error.Message);
                });
            }

            //Bofors
            if(dataBofors!=null)
            {
                string BoforsURL = $"{firebaseURL2}/{dataBofors.getNumero()}.json";
                Debug.Log("URL drone : " + BoforsURL);
                RestClient.Put(BoforsURL, dataBofors).Then(response =>
                {
                    Debug.Log("Datos del bofors agregados correctamente");
                }).Catch(error =>
                {
                    Debug.LogError("Error al agregar datos del drone: " + error.Message);
                });
            }
            //BateriaMisiles
            if(dataBateriaMisiles!=null)
            {
                string BateriaURL = $"{firebaseURL2}/{dataBateriaMisiles.getNumero()}.json";
                Debug.Log("URL drone : " + BateriaURL);
                RestClient.Put(BateriaURL, dataBateriaMisiles).Then(response =>
                {
                    Debug.Log("Datos de la bateria agregados correctamente");
                }).Catch(error =>
                {
                    Debug.LogError("Error al agregar datos de la bateria: " + error.Message);
                });
            }

            //RadarFijo
            if(dataRadarFijo!=null)
            {
                string RadarFijoURL = $"{firebaseURL2}/{dataRadarFijo.getNombre()}.json";
                Debug.Log("URL drone : " + RadarFijoURL);
                RestClient.Put(RadarFijoURL, dataRadarFijo).Then(response =>
                {
                    Debug.Log("Datos del radarFijo agregados correctamente");
                }).Catch(error =>
                {
                    Debug.LogError("Error al agregar datos del radar fijo " + error.Message);
                });
            }
            //RadarMovil
            if(dataRadarMovil!=null)
            {
                string RadarMovilURL = $"{firebaseURL2}/{dataRadarMovil.getNombre()}.json";
                Debug.Log("URL drone : " + RadarMovilURL);
                RestClient.Put(RadarMovilURL, dataRadarMovil).Then(response =>
                {
                    Debug.Log("Datos del radarMovil agregados correctamente");
                }).Catch(error =>
                {
                    Debug.LogError("Error al agregar datos del radar Movilo " + error.Message);
                });

            }
            //Central
            if(dataCentral!=null)
            {
                string CentralURL = $"{firebaseURL2}/{dataCentral.getNombre()}.json";
                Debug.Log("URL drone : " + CentralURL);
                RestClient.Put(CentralURL, dataCentral).Then(response =>
                {
                    Debug.Log("Datos del radarMovil agregados correctamente");
                }).Catch(error =>
                {
                    Debug.LogError("Error al agregar datos del radar Movilo " + error.Message);
                });

            }
            
            
           

        }).Catch(error =>
        {
            Debug.LogError("Error al eliminar datos antiguos de drones: " + error.Message);
        });
      
    }


    public numeroPartida getDatos(string nombre){
        string url = $"https://proyectoude-5c466-default-rtdb.firebaseio.com/partidas/{nombre}.json";
        numeroPartida data = null;
        RestClient.Get<numeroPartida>(url).Then(response =>
        {
            if (response != null)
            {
               data = new numeroPartida(response.getPartida(), response.getNumeroDrones());
             
            }
            else
            {
                Debug.Log("No se encontraron datos para la partida.");
            }
        }).Catch(error =>
        {
            Debug.LogError("Ya no hay mas");
        });
        return data;
    }    


    public void cargarDatos(string nombreJugador, string nombrePartidas){
        string firebaseURL = $"https://proyectoude-5c466-default-rtdb.firebaseio.com/partidas/{nombreJugador}/{nombrePartidas}.json";

        //Drones
        string numeroDrone = "Drone" + 0;       
        string firebaseURLAux = $"https://proyectoude-5c466-default-rtdb.firebaseio.com/partidas/{nombreJugador}/{nombrePartidas}/{numeroDrone}.json";
        List<DatosDrone> a = new List<DatosDrone>();
        RestClient.Get<DatosDrone>(firebaseURLAux).Then(response =>
        {
            if (response != null)
            {
               DatosDrone data = response;
               a.Add(data);
             
            }
            else
            {
                Debug.Log("No se encontraron datos para la partida.");
            }
        }).Catch(error =>
        {
            Debug.LogError("Ya no hay mas");
        });
    
        numeroDrone = "Drone" + 1;       
     
        firebaseURLAux = $"https://proyectoude-5c466-default-rtdb.firebaseio.com/partidas/{nombreJugador}/{nombrePartidas}/{numeroDrone}.json";
        RestClient.Get<DatosDrone>(firebaseURLAux).Then(response =>
        {
            if (response != null)
            {
               DatosDrone data = response;
              a.Add(data);
             
            }
            else
            {
                Debug.Log("No se encontraron datos para la partida.");
            }
        }).Catch(error =>
        {
            Debug.LogError("Ya no hay mas");
        });

        
        numeroDrone = "Drone" + 2;       
        firebaseURLAux = $"https://proyectoude-5c466-default-rtdb.firebaseio.com/partidas/{nombreJugador}/{nombrePartidas}/{numeroDrone}.json";
        RestClient.Get<DatosDrone>(firebaseURLAux).Then(response =>
        {
            if (response != null)
            {
               DatosDrone data = response;
              a.Add(data);
             
            }
            else
            {
                Debug.Log("No se encontraron datos para la partida.");
            }
        }).Catch(error =>
        {
            Debug.LogError("Ya no hay mas");
        });
    
        numeroDrone = "Drone" + 3;       

        firebaseURLAux = $"https://proyectoude-5c466-default-rtdb.firebaseio.com/partidas/{nombreJugador}/{nombrePartidas}/{numeroDrone}.json";
        RestClient.Get<DatosDrone>(firebaseURLAux).Then(response =>
        {
            if (response != null)
            {
               DatosDrone data = response;
               a.Add(data);
             
            }
            else
            {
                Debug.Log("No se encontraron datos para la partida.");
            }
        }).Catch(error =>
        {
            Debug.LogError("Ya no hay mas");
        });
     

        numeroDrone = "Drone" + 4;       
 
        firebaseURLAux = $"https://proyectoude-5c466-default-rtdb.firebaseio.com/partidas/{nombreJugador}/{nombrePartidas}/{numeroDrone}.json";
        RestClient.Get<DatosDrone>(firebaseURLAux).Then(response =>
        {
            if (response != null)
            {
               DatosDrone data = response;
               a.Add(data);
             
            }
            else
            {
                Debug.Log("No se encontraron datos para la partida.");
            }
        }).Catch(error =>
        {
            Debug.LogError("Ya no hay mas");
        });
     

        numeroDrone = "Drone" + 5;       
        firebaseURLAux = $"https://proyectoude-5c466-default-rtdb.firebaseio.com/partidas/{nombreJugador}/{nombrePartidas}/{numeroDrone}.json";
        RestClient.Get<DatosDrone>(firebaseURLAux).Then(response =>
        {
            if (response != null)
            {
               DatosDrone data = response;
              a.Add(data);
             
            }
            else
            {
                Debug.Log("No se encontraron datos para la partida.");
            }
        }).Catch(error =>
        {
            Debug.LogError("Ya no hay mas");
        });
     
        listaDatosDrone = a;

        RestClient.Get<DatosTorretaLazer>(firebaseURL).Then(response =>{
            if (response != null)
            {
               dataTorreta = response;

            }
            else
            {
                Debug.Log("No se encontraron datos para la partida.");
            }
        }).Catch(error =>
        {
            Debug.LogError("Ya no hay mas");
        });

        RestClient.Get<DatosBofors>(firebaseURL).Then(response =>{
            if (response != null)
            {
               dataBofors = response;

            }
            else
            {
                Debug.Log("No se encontraron datos para la partida.");
            }
        }).Catch(error =>
        {
            Debug.LogError("Ya no hay mas");
        });

        RestClient.Get<DatosBateriaMisiles>(firebaseURL).Then(response =>{
            if (response != null)
            {
               dataBateriaMisiles = response;

            }
            else
            {
                Debug.Log("No se encontraron datos para la partida.");
            }
        }).Catch(error =>
        {
            Debug.LogError("Ya no hay mas");
        });

        
        RestClient.Get<DatosRadarFijo>(firebaseURL).Then(response =>{
            if (response != null)
            {
               dataRadarFijo = response;

            }
            else
            {
                Debug.Log("No se encontraron datos para la partida.");
            }
        }).Catch(error =>
        {
            Debug.LogError("Ya no hay mas");
        });
        
        RestClient.Get<DatoRadarMovil1>(firebaseURL).Then(response =>{
            if (response != null)
            {
               dataRadarMovil = response;

            }
            else
            {
                Debug.Log("No se encontraron datos para la partida.");
            }
        }).Catch(error =>
        {
            Debug.LogError("Ya no hay mas");
        });

        
        RestClient.Get<DatosCentral>(firebaseURL).Then(response =>{
            if (response != null)
            {
                dataCentral = response;
                Debug.Log("CARGO CENTRAL");
            }
            else
            {
                Debug.Log("No se encontraron datos para la partida.");
            }
        }).Catch(error =>
        {
            Debug.LogError("Ya no hay mas");
        });
    }

}