using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class JugadorControlador : MonoBehaviourPun
{
    public static JugadorControlador yo;
    public static JugadorControlador enemigo;
    [SerializeField] List<Vector3> posicionesUnidades;
    [SerializeField] List<Vector3> posicionesDefensa;
    [SerializeField] private List<Unit_Settings> unidades = new List<Unit_Settings>();
    //[SerializeField] private List<Defensas> defensas = new List<Defensas>();
    private bool soyAtacante;
    private int dinero=1000;

    private bool empezoPartida=false;

    public DatosPartida datosPar;

    [SerializeField] public GameObject panelInformacionJugador;
    //[SerializeField] GameObject panelBotonesJugador;
    //[SerializeField] GameObject panelInformacionJugadorB;


    //public Drone drone;
    //[SerializeField] GameObject drone;


    Player jugadorPUN;
    public Player JugadorPUN { get { return jugadorPUN; } }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EmpezarPartida());
        //ManejadorInformacionUI.instance.ColocarNombreJugadorA(jugadorPUN.NickName);
        //ManejadorInformacionUI.instance.ColocarDineroA(dinero);
        //ManejadorInformacionUI.instance.OcultarPanelEnemigo(panelInformacionJugador);
    }

    // Update is called once per frame
    void Update()
    {
        if(empezoPartida){
            if(!GameObject.Find("Drone(Clone)")){
                PlayerPrefs.SetString("NombreJugador", GameObject.Find("Jugador2").GetComponent<JugadorControlador>().JugadorPUN.NickName);
                PlayerPrefs.Save();
                ManejadorPUN.instancia.photonView.RPC("LanzarEscena", RpcTarget.All, "Victoria");
            }
            if(!GameObject.Find("CañonLaser(Clone)") && !GameObject.Find("CañonBofors(Clone)") && !GameObject.Find("BateriaMisiles(Clone)")){
                PlayerPrefs.SetString("NombreJugador", GameObject.Find("Jugador1").GetComponent<JugadorControlador>().JugadorPUN.NickName);
                PlayerPrefs.Save();
                ManejadorPUN.instancia.photonView.RPC("LanzarEscena", RpcTarget.All, "Victoria");
            }
        }
    }

    [PunRPC]
    private void Inicializar(Player jugadorPUN, int cantidad){
        this.jugadorPUN = jugadorPUN;
        if(jugadorPUN.IsLocal){
            yo = this;
            soyAtacante = true;
            DesplegarDrones(cantidad);

            if(PhotonNetwork.IsMasterClient){
                MostrarPanelA();
            }
        }else{
            enemigo = this;
            soyAtacante = false;
            photonView.RPC("DesplegarDefensas", RpcTarget.All);
            photonView.RPC("MostrarPanelB", jugadorPUN);
            
        }
    }

    private void DesplegarDrones(int cantidad){
        Quaternion rotacion = Quaternion.identity;
        string prefabNombre = "Prefabs/Drone";
        for (int i=0; i<cantidad; i++){
            #region Instancia Drones
                GameObject unidad = PhotonNetwork.Instantiate(prefabNombre, posicionesUnidades[i], rotacion);
                unidad.GetComponent<Unit_Settings>().SetNumeroDrone(i);
                unidad.GetPhotonView().RPC("Inicializar", RpcTarget.Others);
                unidad.GetPhotonView().RPC("Inicializar", jugadorPUN);
                unidades.Add(unidad.GetComponent<Unit_Settings>()); 
            #endregion
        }
    }

    [PunRPC]
    private void DesplegarDefensas(){
        if(!PhotonNetwork.IsMasterClient){
            Quaternion rotacion = Quaternion.identity;
            #region Instancia Central (Element 0)
                GameObject unidad = PhotonNetwork.Instantiate("Prefabs/Central", posicionesDefensa[0], rotacion); 
                unidad.GetPhotonView().RPC("Inicializar", RpcTarget.All);
                unidades.Add(unidad.GetComponent<Unit_Settings>()); 
            #endregion

            #region Instancia Bateria Misiles (Element 1)
                unidad = PhotonNetwork.Instantiate("Prefabs/BateriaMisiles", posicionesDefensa[1], rotacion); 
                unidad.GetPhotonView().RPC("Inicializar", RpcTarget.All);
                unidades.Add(unidad.GetComponent<Unit_Settings>()); 
            #endregion

            #region Instancia Cañon Laser (Element 2)
                unidad = PhotonNetwork.Instantiate("Prefabs/CañonLaser", posicionesDefensa[2], rotacion);
                unidad.GetPhotonView().RPC("Inicializar", RpcTarget.All);
                unidades.Add(unidad.GetComponent<Unit_Settings>()); 
            #endregion

            #region Instancia Radar Fijo (Element 3)
                unidad = PhotonNetwork.Instantiate("Prefabs/RadarFijo", posicionesDefensa[3], rotacion); 
                unidad.GetPhotonView().RPC("Inicializar", RpcTarget.All);
                unidades.Add(unidad.GetComponent<Unit_Settings>()); 
            #endregion

            #region Instancia Radar Movil (Element 4)
                unidad = PhotonNetwork.Instantiate("Prefabs/RadarMovil", posicionesDefensa[4], rotacion); 
                unidad.GetPhotonView().RPC("Inicializar", RpcTarget.All);
                unidades.Add(unidad.GetComponent<Unit_Settings>()); 
            #endregion

            #region Instancia Cañon Bosfor (Element 5)
                unidad = PhotonNetwork.Instantiate("Prefabs/CañonBofors", posicionesDefensa[5], rotacion); 
                unidad.GetPhotonView().RPC("Inicializar", RpcTarget.All);
                unidades.Add(unidad.GetComponent<Unit_Settings>()); 
            #endregion
            
        }
        
    }


    private void MostrarPanelA(){
        GameObject panelPrefab = Resources.Load<GameObject>("panelJugador1");
        GameObject panel = PhotonNetwork.Instantiate(panelPrefab.name, Vector3.zero, Quaternion.identity);
        panel.transform.position = panelPrefab.transform.position;
        panel.transform.rotation = panelPrefab.transform.rotation;
        panel.transform.SetParent(GameObject.Find("Canvas").transform, false);
        panelInformacionJugador = panel;
        panelInformacionJugador.GetPhotonView().RPC("Inicializar", jugadorPUN);
        panelInformacionJugador.GetPhotonView().RPC("Mostrar", jugadorPUN);
        panelInformacionJugador.GetComponent<ManejadorInformacionUI>().ColocarNombreJugador(jugadorPUN.NickName);
        panelInformacionJugador.GetComponent<ManejadorInformacionUI>().ColocarDinero(dinero);
    }

    [PunRPC]
    private void MostrarPanelB(){
        if(!PhotonNetwork.IsMasterClient){
            GameObject panelPrefab = Resources.Load<GameObject>("panelJugador2");
            GameObject panel = PhotonNetwork.Instantiate(panelPrefab.name, Vector3.zero, Quaternion.identity);
            panel.transform.position = panelPrefab.transform.position;
            panel.transform.rotation = panelPrefab.transform.rotation;
            panel.transform.SetParent(GameObject.Find("Canvas").transform, false);
            panelInformacionJugador = panel;
            panelInformacionJugador.GetPhotonView().RPC("Inicializar", jugadorPUN);
            panelInformacionJugador.GetPhotonView().RPC("Mostrar", jugadorPUN);
            panelInformacionJugador.GetComponent<ManejadorInformacionUI>().ColocarNombreJugador(jugadorPUN.NickName);
            panelInformacionJugador.GetComponent<ManejadorInformacionUI>().ColocarDinero(dinero);
        }
        
    }

    [PunRPC]
    private void CargarUnidades(Player jugadorPUN){
        Debug.Log("CargarUNIDADES");
        DatosPartida datos = ManejadorPUN.instancia.datosP;
        this.jugadorPUN = jugadorPUN;
        if(jugadorPUN.IsLocal){
            yo = this;
            soyAtacante = true;
            DesplegarDronesCargados(datos.listaDatosDrone);

            
            if(PhotonNetwork.IsMasterClient){
                GameObject.Find("Jugador2").GetComponent<JugadorControlador>().datosPar = ManejadorPUN.instancia.datosP;
                MostrarPanelA();
            }
        }else{
            enemigo = this;
            soyAtacante = false;
            StartCoroutine(DesplegarDefCar());
            
            photonView.RPC("MostrarPanelB", jugadorPUN);
            
        }
    }

    IEnumerator DesplegarDefCar(){
        yield return new WaitForSeconds(3f);
        photonView.RPC("DesplegarDefensasCargadas", RpcTarget.All);
    }

    private void DesplegarDronesCargados(List<DatosDrone> drones){
        Quaternion rotacion = Quaternion.identity;
        foreach (DatosDrone drone in drones)
        {
            GameObject unidad = PhotonNetwork.Instantiate("Prefabs/Drone", drone.posicion, rotacion);
            unidad.GetComponent<Unit_Settings>().SetNumeroDrone(drone.numero);
            unidad.GetComponent<Unit_Settings>().saludActual = drone.vida;
            unidad.GetComponent<Unit_Settings>().SetMunicionRestante(drone.misiles);
            unidad.GetPhotonView().RPC("Inicializar", RpcTarget.Others);
            unidad.GetPhotonView().RPC("Inicializar", jugadorPUN);
            unidades.Add(unidad.GetComponent<Unit_Settings>()); 
        }
    }

    [PunRPC]
    private void DesplegarDefensasCargadas(){
        
        Debug.Log("Entre defensas");
        if(!PhotonNetwork.IsMasterClient){
            Quaternion rotacion = Quaternion.identity;
            #region Instancia Central (Element 0)
                if(datosPar.dataCentral!=null){
                    Debug.Log("Entre central");
                    DatosCentral datoC = datosPar.dataCentral;
                    GameObject unidad = PhotonNetwork.Instantiate("Prefabs/Central", datoC.posicion, rotacion); 
                    unidad.GetComponent<Unit_Settings>().saludActual = datoC.vida;
                    unidad.GetPhotonView().RPC("Inicializar", RpcTarget.All);
                    unidades.Add(unidad.GetComponent<Unit_Settings>()); 
                }
            #endregion

            #region Instancia Bateria Misiles (Element 1)
                if(datosPar.dataBateriaMisiles!=null){
                    DatosBateriaMisiles datoB = datosPar.dataBateriaMisiles;
                    GameObject unidad = PhotonNetwork.Instantiate("Prefabs/BateriaMisiles", datoB.posicion, rotacion); 
                    unidad.GetComponent<Unit_Settings>().saludActual = datoB.vida;
                    unidad.GetComponent<Unit_Settings>().SetMunicionRestante(datoB.cantBalas);
                    unidad.GetPhotonView().RPC("Inicializar", RpcTarget.All);
                    unidades.Add(unidad.GetComponent<Unit_Settings>()); 
                }
            #endregion

            #region Instancia Cañon Laser (Element 2)
                if(datosPar.dataTorreta!=null){
                    DatosTorretaLazer datoT = datosPar.dataTorreta;
                    GameObject unidad = PhotonNetwork.Instantiate("Prefabs/CañonLaser", datoT.posicion, rotacion);
                    unidad.GetComponent<Unit_Settings>().saludActual = datoT.vida;
                    unidad.GetComponent<Unit_Settings>().SetMunicionRestante(datoT.cantBalas);
                    unidad.GetPhotonView().RPC("Inicializar", RpcTarget.All);
                    unidades.Add(unidad.GetComponent<Unit_Settings>()); 
                }
            #endregion

            #region Instancia Radar Fijo (Element 3)
                if(datosPar.dataRadarFijo!=null){
                    DatosRadarFijo datoRF = datosPar.dataRadarFijo;
                    GameObject unidad = PhotonNetwork.Instantiate("Prefabs/RadarFijo", datoRF.posicion, rotacion); 
                    unidad.GetComponent<Unit_Settings>().saludActual = datoRF.vida;
                    unidad.GetPhotonView().RPC("Inicializar", RpcTarget.All);
                    unidades.Add(unidad.GetComponent<Unit_Settings>()); 
                }
            #endregion

            #region Instancia Radar Movil (Element 4)
                if(datosPar.dataRadarMovil!=null){
                    DatoRadarMovil1 datoRM = datosPar.dataRadarMovil;
                    GameObject unidad = PhotonNetwork.Instantiate("Prefabs/RadarMovil", datoRM.posicion, rotacion); 
                    unidad.GetComponent<Unit_Settings>().saludActual = datoRM.vida;
                    unidad.GetPhotonView().RPC("Inicializar", RpcTarget.All);
                    unidades.Add(unidad.GetComponent<Unit_Settings>()); 
                }
            #endregion

            #region Instancia Cañon Bosfor (Element 5)
                if(datosPar.dataBofors!=null){
                    DatosBofors datoBF = datosPar.dataBofors;
                    GameObject unidad = PhotonNetwork.Instantiate("Prefabs/CañonBofors", datoBF.posicion, rotacion); 
                    unidad.GetComponent<Unit_Settings>().saludActual = datoBF.vida;
                    unidad.GetComponent<Unit_Settings>().SetMunicionRestante(datoBF.cantBalas);
                    unidad.GetPhotonView().RPC("Inicializar", RpcTarget.All);
                    unidades.Add(unidad.GetComponent<Unit_Settings>()); 
                }
            #endregion
            
        }
        
    }

    IEnumerator EmpezarPartida(){
        yield return new WaitForSeconds(10f);
        empezoPartida=true;
    }

    //No funciona
    internal void LimpiarSeleccion(){
        //drone.LimpiarSelec();
    }

    public bool esAtacante(){
        return soyAtacante;
    }

    
    
}
