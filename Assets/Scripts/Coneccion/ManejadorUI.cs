using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ManejadorUI : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    [Header("Paneles Principales")]
    [SerializeField] GameObject panelPrincipal;
    [SerializeField] GameObject panelBotones; //add
    [SerializeField] GameObject panellog; //add
    [SerializeField] GameObject panelsing; //add
    [SerializeField] GameObject panelLogin; //Cambiar despues, ya que no es log
    [SerializeField] GameObject panelCreacion;
    [SerializeField] GameObject panelLobby;
    [SerializeField] GameObject panelDisponibles;
    [SerializeField] GameObject panelGuardadas;

    [Header("Botones iniciales")]
    [SerializeField] Button btnIrALoguearse;
    [SerializeField] Button btnIrASingUP;

    [Header("Panel Sing")]
    [SerializeField] TMP_InputField inpUsuarioSing;
    [SerializeField] TMP_InputField ContrasenaSing;
    [SerializeField] Button btnRegistrarse;
    [SerializeField] Button btnVolverSing;

     [Header("Panel Log")]
    [SerializeField] TMP_InputField inpUsuariolog;
    [SerializeField] TMP_InputField Contrasenalog;
    [SerializeField] Button btnALoguearse;
    [SerializeField] Button btnVolverLog;

    [Header("Panel Inicio Juego")]
    [SerializeField] TMP_InputField inpNombre;
    [SerializeField] Button btnCrearRoom;
    [SerializeField] Button btnBuscarRoom;
    [SerializeField] Button btnGuardada;

    [Header("Panel Creacion Sala")]
    [SerializeField] TMP_InputField inpNombreRoom;
    [SerializeField] Button btnCrear;
    [SerializeField] Button btnRegresarC;

    [Header("Panel Lobby")]
    [SerializeField] TextMeshProUGUI txtNombreRoom;
    [SerializeField] TextMeshProUGUI txtJugador1;
    [SerializeField] TextMeshProUGUI txtJugador2;
    [SerializeField] Button btnLanzar;
    [SerializeField] Button btnSalir;

    [Header("Panel Disponibles")]
    [SerializeField] Button btnRefrescar;
    [SerializeField] Button btnRegresarD;
    [SerializeField] RectTransform contenedorRooms;
    [SerializeField] GameObject prefabRoomDisponible;

    [Header("Panel Guardadas")]
    [SerializeField] TMP_InputField nombreSala;
    [SerializeField] Button btnCargarPartida;
    
    private bool partidaCargada=false;
    public static  ManejadorUI instance;

    

    List<RoomInfo> roomsDisponibles = new List<RoomInfo>(); //Se usa para mantener registro de las salas disponibles
    List<GameObject> roomsCargados = new List<GameObject>(); //Creo que se cargan los botoes y paneles de las salas
    
    private FirebaseManager firebaseManager;

    private void Start()
    {
        firebaseManager = GetComponent<FirebaseManager>();
    }
    
    private void Awake(){ //Awake se ejecuta cuando se inicia el objeto al q esta adj el script
   
  
        if (instance == null)
        {
                instance = this;
        }
        else if (instance != null)
        {
                Debug.Log("Instance already exists, destroying object!");
                Destroy(this);
        }
        //El sing n log
        btnIrALoguearse.interactable = false;
        btnIrASingUP.interactable = false;

        btnIrALoguearse.onClick.AddListener(OnClickLoguearse);
        btnIrASingUP.onClick.AddListener(OnClickRegistrarse);
        
        btnVolverSing.onClick.AddListener(OnClickMostrarBotones);
        btnVolverLog.onClick.AddListener(OnClickMostrarBotones);

        btnALoguearse.onClick.AddListener(OnClickMostrarInicio);

        btnCrearRoom.onClick.AddListener(OnClickCrearRoom);
        btnBuscarRoom.onClick.AddListener(OnClickBuscarRoom);
        btnGuardada.onClick.AddListener(OnClickGuardada);
        inpUsuariolog.onValueChanged.AddListener(OnNombreJugadorCambia);


        //Creacion
        btnCrear.onClick.AddListener(OnClickMostrarLobby);
        btnRegresarC.onClick.AddListener(OnClickMostrarInicio);

        //Disponibles
        btnRefrescar.onClick.AddListener(OnClickRefrescarDisponibles);
        btnRegresarD.onClick.AddListener(OnClickMostrarInicio);

        //Lobby
        btnSalir.onClick.AddListener(OnClickSalirRoom);
        btnLanzar.onClick.AddListener(OnClickLanzarJuego);

        //Partidas Guardadas
        btnCargarPartida.onClick.AddListener(OnClickCargarPartida);

        panelLogin.SetActive(false);
        panellog.SetActive(false);
        panelsing.SetActive(false);
    }

    public override void OnConnectedToMaster(){
       btnIrALoguearse.interactable = true;
        btnIrASingUP.interactable =true;
    }
//Add to me
    private void OnClickLoguearse(){
       panelBotones.SetActive(false);
       panellog.SetActive(true);
    }

    private void OnClickRegistrarse(){
       panelBotones.SetActive(false);
       panelsing.SetActive(true);
    }
//Add for me
    private void OnClickCrearRoom(){
        panelLogin.SetActive(false);
        panelCreacion.SetActive(true);
    }

    private void OnClickBuscarRoom(){
        panelLogin.SetActive(false);
        panelDisponibles.SetActive(true);
        ActualizarListaRoomsDisponibles();
    }

    private void OnClickGuardada(){
        panelLogin.SetActive(false);
        panelGuardadas.SetActive(true);
    }

    private void OnClickMostrarLobby(){
        panelLogin.SetActive(false);
        panelCreacion.SetActive(false);
        panelLobby.SetActive(true);

        ManejadorPUN.instancia.CrearRoom(inpNombreRoom.text);
    }

    private void OnClickMostrarInicio(){

   
        if(!firebaseManager.GetFallo())
        {
             bool fallo = firebaseManager.GetFallo();
            Debug.Log("El valor de fallo es: " + fallo);
            Debug.Log("Entro");
            panelLobby.SetActive(false);
            panelCreacion.SetActive(false);
            panelDisponibles.SetActive(false);
            panelLogin.SetActive(true);
            panelsing.SetActive(false);
            panelBotones.SetActive(false);
            panellog.SetActive(false);
        }else{
           OnClickLoguearse();
        }
    }

    private void seguirEnMismoPanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    private void OnClickMostrarBotones(){
        panelCreacion.SetActive(false);
        panelDisponibles.SetActive(false);
        panelBotones.SetActive(true);
        panellog.SetActive(false);
        panelLobby.SetActive(false);
        panelsing.SetActive(false);
        panelLogin.SetActive(false);
    }
    private void OnClickRefrescarDisponibles(){
        ActualizarListaRoomsDisponibles();
    }

    private void OnClickSalirRoom(){
        panelLobby.SetActive(false);
        panelLogin.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    private void OnClickLanzarJuego(){
        if(!partidaCargada){
            PlayerPrefs.SetString("Cargada", "false");
            PlayerPrefs.Save();
            ManejadorPUN.instancia.photonView.RPC("LanzarEscena", RpcTarget.All, "Juego");
            Debug.Log("No es cargada");
        }else{
            /*if(ManejadorPUN.instancia==null){
                Debug.Log("Es nulo");
            }*/
            ManejadorPUN.instancia.cargarD(PhotonNetwork.MasterClient.NickName, nombreSala.text);
            PlayerPrefs.SetString("Cargada", "true");
            PlayerPrefs.Save();
            StartCoroutine(cargar()); 
        }
        
    }

    private void OnClickCargarPartida(){
        partidaCargada=true;
        panelGuardadas.SetActive(false);
        panelLobby.SetActive(true);
        ManejadorPUN.instancia.CrearRoom(nombreSala.text);
    }

    private void OnNombreJugadorCambia(string nomb){
        PhotonNetwork.NickName = nomb;
    }
    
    public override void OnJoinedRoom(){
        photonView.RPC("ActualizarLobby", RpcTarget.All);
    }

    public override void OnLeftRoom(){
        photonView.RPC("ActualizarLobby", RpcTarget.All);
    }

    public override void OnPlayerEnteredRoom(Player jugador){
        photonView.RPC("ActualizarLobby", RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Player jugador){
        photonView.RPC("ActualizarLobby", RpcTarget.All);
    }

    [PunRPC]
    void ActualizarLobby(){
        btnLanzar.interactable = false;
        txtJugador1.text = string.Empty;
        txtJugador2.text = string.Empty;

        txtNombreRoom.text = PhotonNetwork.CurrentRoom.Name;

        txtJugador1.text = PhotonNetwork.PlayerList[0].NickName;
        if(PhotonNetwork.PlayerList.Length>1){
            txtJugador2.text = PhotonNetwork.PlayerList[1].NickName;
            btnLanzar.interactable = PhotonNetwork.IsMasterClient;//Espera a que esten 2 jugadores
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        roomsDisponibles = roomList;
    }

    public GameObject CrearBotonRoom(){
        GameObject botonRoom = Instantiate(prefabRoomDisponible, contenedorRooms.transform);
        roomsCargados.Add(botonRoom);
        return botonRoom;
    }

    void ActualizarListaRoomsDisponibles(){
        
        foreach (GameObject go in roomsCargados){
            Destroy(go);
        }
        
        for(int x=0; x<roomsDisponibles.Count; x++){
            RoomInfo roomActual = roomsDisponibles[x];
            GameObject boton = CrearBotonRoom();
            boton.SetActive(true);

            Debug.Log("NombreHost: "+roomActual.CustomProperties["Host"]);
            boton.GetComponent<InfoRoom>().txtNombre.text = $"Nombre: {roomActual.Name}";
            boton.GetComponent<InfoRoom>().txtCantidad.text = $"Jugadores: {roomActual.PlayerCount} / {roomActual.MaxPlayers}";

            Button b1 = boton.GetComponent<Button>();
            b1.onClick.RemoveAllListeners();
            b1.onClick.AddListener( () => {
                ManejadorPUN.instancia.UnirseRoom(roomActual.Name); 
                panelDisponibles.SetActive(false); 
                panelLobby.SetActive(true);
            });
        }
    }

    IEnumerator cargar(){
        yield return new WaitForSeconds(5f);
        ManejadorPUN.instancia.photonView.RPC("LanzarEscena", RpcTarget.All, "Juego");
    }

}