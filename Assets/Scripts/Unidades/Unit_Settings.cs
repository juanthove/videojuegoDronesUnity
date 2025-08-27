using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; //Usar sistema de query para consulta de colecciones
using Photon.Pun;
using Photon.Realtime;

public class Unit_Settings : MonoBehaviourPun
{
    #region Informacion base de la unidad
        public enum enumTipoUnidad{Terrestre, Aerea}
        public enum enumEquipo{ROJO, AZUL}

        private string nombre {get ; set;}
        private enumTipoUnidad tipoUnidad { get; set;}
        private float rangoVision { get; set;}
        private enumEquipo equipo {get; set;}
        private Vector3 posicion ;
        [SerializeField] int numeroDrone;

    #endregion

    #region Datos de Salud de la Unidad
            private int saludMaxima = 1;
            public int saludActual { get; set;}
            public Slider healthSlider;
            public HealthUI healthUI;
    #endregion


    #region Parametros relacionados con la Central electrica
        private bool dependeCentral = false;
        private bool centralActiva = true;
        //private bool esCentral = false;
    #endregion

    #region Informacion del objetivo al que esta unidad ataca
        Unit_Settings datosObjetivo; 
        Vector3 posicionObjetivo;
    #endregion

    #region Informacion de ataque 
        private int municionRestante = 0;
        private float rangoAtaque = 100f;
        private bool puedeLanzarMisiles = false;
        private bool puedeLanzarMunicion = false;
        private bool puedeLanzarLaser= false;
        private bool esRayo = false;
        private int danoMisil = 1;
        private int danoMunicion = 25;
        // Cadencia de fuego en segundos
        public float cadenciaDeFuego = 1f;
        private float tiempoUltimoDisparo = 0f;
    #endregion

    [SerializeField] PhotonView pv;
    [PunRPC]
    public void Inicializar(){
        pv = photonView;
    }

    #region Referencias y metodos de incializacion
        private Unit_Movement unitMovement;
        private Unit_Selectable unitSelect;
        void Start(){
            
            unitSelect = GetComponent<Unit_Selectable>();
            if(GetComponent<Unit_Movement>()!=null && !photonView.IsMine){
                unitMovement = GetComponent<Unit_Movement>();
                unitMovement.enabled = false;
            }
            if(!photonView.IsMine){
                
                unitSelect.enabled = false;
            }
            // if(healthSlider != null){
            //     healthSlider.maxValue = saludMaxima;
            //     healthSlider.value = saludActual;
            // }

            
        
        }

        void Update(){
            tiempoUltimoDisparo += Time.deltaTime;
            //MantenerBarraHaciaCamara();
            // if(nombre == "Central"){
            //     ActualizarEstadoCentral();
            // } 
        }
    #endregion

    #region Geters y Seters
        public enumEquipo GetEquipo(){
            return this.equipo;
        }

        public int GetSalud(){
            return saludActual;
        }
        public string GetNombre(){
            return nombre;
        }
        public int GetMunicionRestante(){
            return municionRestante;
        }

        public void SetMunicionRestante(int mun){
            municionRestante = mun;
        }
        public int GetDanoMisil(){
            return danoMisil;
        }
        public int GetNumeroDrone(){
            return numeroDrone;
        }
        public void SetNumeroDrone(int num){
            numeroDrone = num;
        }
    #endregion

    #region Metodos para creacion de unidades
        public void CrearUnidad(string nombre){
            switch(nombre){
                case "Drone":{
                    CrearDrone();
                    break;
                }
                case "Central":{
                    CrearCentral();
                    break;
                }
                case "Radar Fijo":{
                    CrearRadarFijo();
                    break;
                }
                case "Radar Movil":{
                    CrearRadarMovil();
                    break;
                }
                case "Canon Bofors":{
                    CrearCanonBosfor();
                    break;
                }
                case "Canon Laser":{
                    CrearCanonLaser();
                    break;
                }
                case "Bateria Misiles":{
                    CrearBateriaMisiles();
                    break;
                }
            }
            healthUI = GetComponent<HealthUI>();
            healthUI.Start3DSlider(saludMaxima);
        }

        public void CrearDrone(){
            this.nombre = "Drone";
            this.tipoUnidad = enumTipoUnidad.Aerea;
            this.rangoVision = 100f;
            this.saludMaxima = 2;
            this.saludActual = 2;
            this.equipo = enumEquipo.ROJO;
            this.puedeLanzarMisiles = true;
            this.municionRestante = 4;
            this.rangoAtaque = 50f;
        }

        public void CrearCentral(){
            this.nombre = "Central";
            this.tipoUnidad = enumTipoUnidad.Terrestre;
            this.rangoVision = 200f;
            this.saludMaxima = 4;
            this.saludActual = 4;
            this.equipo = enumEquipo.AZUL;
        }


        public void CrearRadarFijo(){
            this.nombre = "Radar Fijo";
            this.tipoUnidad = enumTipoUnidad.Terrestre;
            this.rangoVision = 200f;
            this.saludMaxima = 2;
            this.saludActual = 2;
            this.equipo = enumEquipo.AZUL;
            this.dependeCentral = true;
        }

        public void CrearRadarMovil(){
            this.nombre = "Radar Movil";
            this.tipoUnidad = enumTipoUnidad.Terrestre;
            this.rangoVision = 200f;
            this.saludMaxima = 2;
            this.saludActual = 2;
            this.equipo = enumEquipo.AZUL;
        }

        public void CrearCanonBosfor(){
            this.nombre = "Canon Bosfor";
            this.tipoUnidad = enumTipoUnidad.Terrestre;
            this.rangoVision = 200f;
            this.saludMaxima = 2;
            this.saludActual = 2;
            this.equipo = enumEquipo.AZUL;
            this.puedeLanzarMisiles = true;
            this.municionRestante = 4;
        }

        public void CrearCanonLaser(){
            this.nombre = "Canon Laser";
            this.tipoUnidad = enumTipoUnidad.Terrestre;
            this.rangoVision = 200f;
            this.saludMaxima = 4;
            this.saludActual = 4;
            this.equipo = enumEquipo.AZUL;
            this.puedeLanzarLaser = true;
            this.esRayo = true;
            this.dependeCentral = true;
            this.danoMisil = 2;
        }

        public void CrearBateriaMisiles(){
            this.nombre = "Bateria Misiles";
            this.tipoUnidad = enumTipoUnidad.Terrestre;
            this.rangoVision = 200f;
            this.saludMaxima = 4;
            this.saludActual = 4;
            this.equipo = enumEquipo.AZUL;
            this.puedeLanzarMisiles = true;
            this.puedeLanzarMunicion = false;
            this.municionRestante = 4;
            
        }
    #endregion

    # region Lanzamiento de proyectiles
        public void LanzarProyectil(){
            //Damos click derecho y tenemos una unidad seleccionada
            if(Input.GetMouseButtonDown(1)){
                ObtenerDatosObjetivo();
            }
            if(GetPuedeLanzarMisiles() && EsOjetivoValido() && EstaRangoAtaque() && EnfriamientoMisil()){
                DetenerUnidad();
                LanzarMisil();
                tiempoUltimoDisparo = 0f;
            }
        }

        public void ObtenerDatosObjetivo(){
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(rayo, out hit)){
                datosObjetivo = null;
                datosObjetivo = hit.collider.GetComponentInParent<Unit_Settings>();
                if(hit.collider.gameObject != null){
                    posicionObjetivo = hit.collider.gameObject.transform.position;
                }   
                
            }
        }
        public bool GetPuedeLanzarMisiles(){
            return puedeLanzarMisiles && municionRestante > 0;
        }
    
        public bool EsOjetivoValido(){
            if(datosObjetivo != null && datosObjetivo.GetEquipo() != GetEquipo()){
                return true;
            }
            return false;
        }

        public bool EstaRangoAtaque(){
            float distancia = Vector3.Distance(transform.position, posicionObjetivo);;
            return distancia <= rangoAtaque;
        }
        
        public bool EnfriamientoMisil(){
            return tiempoUltimoDisparo >= cadenciaDeFuego;
        }
        
        public void LanzarMisil(){
            GameObject misilPrefab = Resources.Load<GameObject>("Misil");
            if(misilPrefab != null){
                GameObject misil = PhotonNetwork.Instantiate(misilPrefab.name, transform.position, transform.rotation);
                Misil_Trayectoria scriptMisil = misil.GetComponent<Misil_Trayectoria>();
                if (scriptMisil != null)
                    {
                        if(GetComponent<Collider>() != null)
                            scriptMisil.EstablecerObjetivo(datosObjetivo.transform, gameObject);
                            municionRestante--;
                            Debug.Log("Cantidad de misiles restantes: "+ municionRestante);
                    }
                    else
                    {
                        Debug.LogError("No se encontró el script MisilScript en el prefab del misil.");
                    }
            }else
                Debug.LogError("No se encontro el Prefab");
            
        }

        public void DetenerUnidad(){
            if(unitMovement != null){
                unitMovement.SetPosicionObjetivo(transform.position);
            }
            
        } 

        public void CargarMunicion(int cantidad){
            municionRestante += cantidad;
        }
    #endregion

    #region Recibir Proyectil
        void OnCollisionEnter(Collision collision){
            RecibirDano(collision);
        }
        public void RecibirDano(Collision collision){
            //La colision es un misil
            if(collision.gameObject.GetComponent<Misil_Trayectoria>() != null && collision.gameObject.GetComponent<Misil_Trayectoria>().GetUnidadLanzadora() != gameObject){
                Misil_Trayectoria misilTrayectoria = collision.gameObject.GetComponent<Misil_Trayectoria>();
                Unit_Settings unidadLanzadoraSettings = misilTrayectoria.GetUnidadLanzadora().GetComponent<Unit_Settings>();
                
                if(misilTrayectoria != null && misilTrayectoria.GetUnidadLanzadora() != gameObject && unidadLanzadoraSettings != null && unidadLanzadoraSettings.GetEquipo() != GetEquipo()){
                    photonView.RPC("RestarSalud", RpcTarget.All, unidadLanzadoraSettings.GetDanoMisil());
                    GameObject explosionPrefab = Resources.Load<GameObject>("Explosion");
                    if(!photonView.IsMine){
                        PhotonNetwork.Destroy(collision.gameObject);
                        if(collision.gameObject.GetComponent<Misil_Trayectoria>().GetObjetivo() == null){
                            unidadLanzadoraSettings.DevolverMisil();
                        }
                    }   
                    if(explosionPrefab != null){
                        GameObject explosion = PhotonNetwork.Instantiate(explosionPrefab.name, collision.contacts[0].point, Quaternion.identity);
                    }
                        
                    photonView.RPC("DestruirUnidad", RpcTarget.All);
                }
            } 
        }

        
        
        [PunRPC]
        public void DestruirUnidad(){
            if(saludActual <= 0){
                if(nombre == "Central"){
                    centralActiva = false;
                    DesactivacionDeUnidades();
                }
                
                if(photonView.IsMine){
                    PhotonNetwork.Destroy(gameObject); 
                }     
            } 
        }

        [PunRPC]
        public void RestarSalud(int dano){
            saludActual -= dano;
            saludActual = Mathf.Clamp(saludActual, 0, saludMaxima);
            healthUI.Update3DSlider(saludActual);     
        }

        public void DevolverMisil(){
            municionRestante++;
            Debug.Log("Devuelvo un misil: "+ municionRestante);
        }

        public int CantidadMunicion(){
            return municionRestante;
        }

        void OnDestroy(){
            ManejadorInformacionUI jug1 = GameObject.Find("panelJugador1(Clone)").GetComponent<ManejadorInformacionUI>();
            switch(nombre){
                case "Drone":{
                    GameObject.Find("panelJugador2(Clone)").GetComponent<ManejadorInformacionUI>().SumarDinero(300);
                    break;
                }
                case "Central":{
                    jug1.SumarDinero(400);
                    break;
                }
                case "Radar Fijo":{
                    jug1.SumarDinero(200);
                    break;
                }
                case "Radar Movil":{
                    jug1.SumarDinero(100);
                    break;
                }
                case "Canon Bofors":{
                    jug1.SumarDinero(300);
                    break;
                }
                case "Canon Laser":{
                    jug1.SumarDinero(400);
                    break;
                }
                case "Bateria Misiles":{
                    jug1.SumarDinero(200);
                    break;
                }
            }
        }
    #endregion

    public void DesactivacionDeUnidades(){
        // Especifica el número de Layer que deseas buscar (por ejemplo, Layer 8)
        int layerToSearch = 6;
        // Busca todos los GameObjects en el Layer especificado
        GameObject[] objectsInLayer = FindObjectsOfType<GameObject>().Where(obj => obj.layer == layerToSearch).ToArray();
        // Muestra la cantidad de objetos encontrados en la consola
        foreach(GameObject go in objectsInLayer){
            Unit_Settings goConfig = go.GetComponent<Unit_Settings>();
            if(goConfig != null){
                goConfig.centralActiva = false;
                if(goConfig.dependeCentral){
                    puedeLanzarMisiles = false;
                    puedeLanzarMunicion = false;
                    puedeLanzarLaser = false;
                }
            }
        }

    }

    public Vector3 getPosicion(){
         return (posicion = transform.position);
    }

}
