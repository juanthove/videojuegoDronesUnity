using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ManejadorInformacionUI : MonoBehaviourPun
{
    [Header("Paneles Principales")]
    [SerializeField] GameObject panelInformacionJugador;
    //[SerializeField] GameObject panelJugadorB;
    [SerializeField] GameObject panelBotonesJugador;

    [Header("Panel Jugador")]
    [SerializeField] TextMeshProUGUI txtNombre;
    [SerializeField] TextMeshProUGUI txtDineroActual;
    [SerializeField] Button btnGuardar;
    [SerializeField] PhotonView pv;

    [Header("Panel Botones")]
    [SerializeField] Button btnDrone0;
    [SerializeField] Button btnDrone1;
    [SerializeField] Button btnDrone2;
    [SerializeField] Button btnDrone3;
    [SerializeField] Button btnDrone4;
    [SerializeField] Button btnDrone5;

    [SerializeField] Button btnCañonLaser;
    [SerializeField] Button btnBateriaMisiles;
    [SerializeField] Button btnCañonBofors;
    [SerializeField] Button btnRadarFijo;
    [SerializeField] Button btnRadarMovil;


    [PunRPC]
    public void Inicializar(){
        pv = photonView;
        //txtNombre.text = nombre;
        //txtDineroActual.text = dinero.ToString();
    }

    void Awake(){
        if(btnCañonLaser != null){
            btnCañonLaser.onClick.AddListener(OnClickCañonLaser);
        }
        if(btnBateriaMisiles != null){
            btnBateriaMisiles.onClick.AddListener(OnClickBateriaMisiles);
        }
        if(btnCañonBofors != null){
            btnCañonBofors.onClick.AddListener(OnClickCañonBofors);
        }
        if(btnRadarFijo != null){
            btnRadarFijo.onClick.AddListener(OnClickRadarFijo);
        }
        if(btnRadarMovil != null){
            btnRadarMovil.onClick.AddListener(OnClickRadarMovil);
        }
        if(btnDrone0 != null){
            btnDrone0.onClick.AddListener(OnClickDrone0);
        }
        if(btnDrone1 != null){
            btnDrone1.onClick.AddListener(OnClickDrone1);
        }
        if(btnDrone2 != null){
            btnDrone2.onClick.AddListener(OnClickDrone2);
        }
        if(btnDrone3 != null){
            btnDrone3.onClick.AddListener(OnClickDrone3);
        }
        if(btnDrone4 != null){
            btnDrone4.onClick.AddListener(OnClickDrone4);
        }
        if(btnDrone5 != null){
            btnDrone5.onClick.AddListener(OnClickDrone5);
        }
        if(btnGuardar != null){
            btnGuardar.onClick.AddListener(OnClickGuardar);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        txtDineroActual.text="1000";
    }

    // Update is called once per frame
    void Update()
    {

        //Activar o Desactivar Cañon Laser
        if(btnCañonLaser != null){
            if(DevolverDinero()>=400){
                if(GameObject.Find("Central(Clone)") && GameObject.Find("CañonLaser(Clone)")){
                    if(GameObject.Find("CañonLaser(Clone)").GetComponent<Unit_Settings>().CantidadMunicion() == 0){
                        btnCañonLaser.interactable = true;
                    }else{
                        btnCañonLaser.interactable = false;
                    }
                }else{
                    btnCañonLaser.interactable = false;
                }
            }else{
                btnCañonLaser.interactable = false;
            }
            
        }

        //Activar o Desactivar Bateria Misiles
        if(btnBateriaMisiles != null){
            if(DevolverDinero()>=200){
                if(GameObject.Find("BateriaMisiles(Clone)") && GameObject.Find("Central(Clone)")){
                    if(GameObject.Find("BateriaMisiles(Clone)").GetComponent<Unit_Settings>().CantidadMunicion() < 4){
                        btnBateriaMisiles.interactable = true;
                    }else{
                        btnBateriaMisiles.interactable = false;
                    }
                }else{
                    btnBateriaMisiles.interactable = false;
                }
            }else{
                btnBateriaMisiles.interactable = false;
            }
        }

        //Activar o Desactivar Cañon Bofors
        if(btnCañonBofors != null){
            if(DevolverDinero()>=200){
                if(GameObject.Find("CañonBofors(Clone)")){
                    if(GameObject.Find("CañonBofors(Clone)").GetComponent<Unit_Settings>().CantidadMunicion() < 4){
                        btnCañonBofors.interactable = true;
                    }else{
                        btnCañonBofors.interactable = false;
                    }
                }else{
                    btnCañonBofors.interactable = false;
                }
            }else{
                btnCañonBofors.interactable = false;
            }
        }

        //Activar o Desactivar Radar Fijo
        if(btnRadarFijo != null){
            if(DevolverDinero()>=500){
                if(GameObject.Find("Central(Clone)") && GameObject.Find("RadarFijo(Clone)")){
                    btnRadarFijo.interactable = true;
                }else{
                    btnRadarFijo.interactable = false;
                }
            }else{
                btnRadarFijo.interactable = false;
            }
        }

        //Activar o Desactivar Radar Movil
        if(btnRadarMovil != null){
            if(DevolverDinero()>=300){
                if(GameObject.Find("RadarMovil(Clone)")){
                    btnRadarMovil.interactable = true;
                }else{
                    btnRadarMovil.interactable = false;
                }
            }else{
                btnRadarMovil.interactable = false;
            }
        }

        //Activar o Desactivar Drone 0
        if(btnDrone0 != null){
            if(DevolverDinero()>=400){
                bool sinMunicion = false;
                GameObject[] allObjects = FindObjectsOfType<GameObject>();
                foreach (GameObject obj in allObjects){
                    if(obj.name == "Drone(Clone)"){
                        if(obj.GetComponent<Unit_Settings>().GetNumeroDrone() == 0){
                            if(obj.GetComponent<Unit_Settings>().CantidadMunicion() == 0){
                                sinMunicion = true;
                                btnDrone1.interactable = true;
                            }
                        }else{
                            btnDrone0.interactable = false;
                        }
                    }else{
                        btnDrone0.interactable = false;
                    }
                
                }
                if(sinMunicion){
                    btnDrone0.interactable = true;
                }else{
                    btnDrone0.interactable = false;
                }
            }else{
                btnDrone0.interactable = false;
            }
        }

        //Activar o Desactivar Drone 1
        if(btnDrone1 != null){
            if(DevolverDinero()>=400){
                bool sinMunicion = false;
                GameObject[] allObjects = FindObjectsOfType<GameObject>();
                foreach (GameObject obj in allObjects){
                    if(obj.name == "Drone(Clone)"){
                        if(obj.GetComponent<Unit_Settings>().GetNumeroDrone() == 1){
                            if(obj.GetComponent<Unit_Settings>().CantidadMunicion() == 0){
                                sinMunicion = true;
                                btnDrone1.interactable = true;
                            }
                        }else{
                            btnDrone1.interactable = false;
                        }
                    }else{
                        btnDrone1.interactable = false;
                    }
                    
                }
                if(sinMunicion){
                    btnDrone1.interactable = true;
                }else{
                    btnDrone1.interactable = false;
                }
            }else{
                btnDrone1.interactable = false;
            }
        }

        //Activar o Desactivar Drone 2
        if(btnDrone2 != null){
            if(DevolverDinero()>=400){
                bool sinMunicion = false;
                GameObject[] allObjects = FindObjectsOfType<GameObject>();
                foreach (GameObject obj in allObjects){
                    if(obj.name == "Drone(Clone)"){
                        if(obj.GetComponent<Unit_Settings>().GetNumeroDrone() == 2){
                            if(obj.GetComponent<Unit_Settings>().CantidadMunicion() == 0){
                                sinMunicion = true;
                                btnDrone2.interactable = true;
                            }
                        }else{
                            btnDrone2.interactable = false;
                        }
                    }else{
                        btnDrone2.interactable = false;
                    }
                    
                }
                if(sinMunicion){
                    btnDrone2.interactable = true;
                }else{
                    btnDrone2.interactable = false;
                }
            }else{
                btnDrone2.interactable = false;
            }
        }

        //Activar o Desactivar Drone 3
        if(btnDrone3 != null){
            if(DevolverDinero()>=400){
                bool sinMunicion = false;
                GameObject[] allObjects = FindObjectsOfType<GameObject>();
                foreach (GameObject obj in allObjects){
                    if(obj.name == "Drone(Clone)"){
                        if(obj.GetComponent<Unit_Settings>().GetNumeroDrone() == 3){
                            if(obj.GetComponent<Unit_Settings>().CantidadMunicion() == 0){
                                sinMunicion = true;
                                btnDrone3.interactable = true;
                            }
                        }else{
                            btnDrone3.interactable = false;
                        }
                    }else{
                        btnDrone3.interactable = false;
                    }
                    
                }
                if(sinMunicion){
                    btnDrone3.interactable = true;
                }else{
                    btnDrone3.interactable = false;
                }
            }else{
                btnDrone3.interactable = false;
            }
        }

        //Activar o Desactivar Drone 4
        if(btnDrone4 != null){
            if(DevolverDinero()>=400){
                bool sinMunicion = false;
                GameObject[] allObjects = FindObjectsOfType<GameObject>();
                foreach (GameObject obj in allObjects){
                    if(obj.name == "Drone(Clone)"){
                        if(obj.GetComponent<Unit_Settings>().GetNumeroDrone() == 4){
                            if(obj.GetComponent<Unit_Settings>().CantidadMunicion() == 0){
                                sinMunicion = true;
                                btnDrone4.interactable = true;
                            }
                        }else{
                            btnDrone4.interactable = false;
                        }
                    }else{
                        btnDrone4.interactable = false;
                    }
                    
                }
                if(sinMunicion){
                    btnDrone4.interactable = true;
                }else{
                    btnDrone4.interactable = false;
                }
            }else{
                btnDrone4.interactable = false;
            }
        }

        //Activar o Desactivar Drone 5
        if(btnDrone5 != null){
            if(DevolverDinero()>=400){
                bool sinMunicion = false;
                GameObject[] allObjects = FindObjectsOfType<GameObject>();
                foreach (GameObject obj in allObjects){
                    if(obj.name == "Drone(Clone)"){
                        if(obj.GetComponent<Unit_Settings>().GetNumeroDrone() == 5){
                            if(obj.GetComponent<Unit_Settings>().CantidadMunicion() == 0){
                                sinMunicion = true;
                                btnDrone5.interactable = true;
                            }
                        }else{
                            btnDrone5.interactable = false;
                        }
                    }else{
                        btnDrone5.interactable = false;
                    }
                    
                }
                if(sinMunicion){
                    btnDrone5.interactable = true;
                }else{
                    btnDrone5.interactable = false;
                }
            }else{
                btnDrone5.interactable = false;
            }
        }
    }

    [PunRPC]
    public void Mostrar(){
        this.gameObject.SetActive(true);
    }

    public void ColocarNombreJugador(string jug){
        txtNombre.text = jug;
    }

    public void ColocarDinero(int din){
        txtDineroActual.text = din.ToString();
    }
    
    public int DevolverDinero(){
        return int.Parse(txtDineroActual.text);
    }

    public void QuitarDinero(int din){
        ColocarDinero(int.Parse(txtDineroActual.text)-din);
    }

    public void SumarDinero(int din){
        ColocarDinero(int.Parse(txtDineroActual.text)+din);
    }

    private void OnClickGuardar(){
        ManejadorPUN.instancia.datosP.actualizarPartida(PhotonNetwork.MasterClient.NickName, PhotonNetwork.CurrentRoom.Name);
    }

    private void OnClickCañonLaser(){
        GameObject.Find("CañonLaser(Clone)").GetComponent<Unit_Settings>().CargarMunicion(1);
        QuitarDinero(400);
    }

    private void OnClickBateriaMisiles(){
        GameObject.Find("BateriaMisiles(Clone)").GetComponent<Unit_Settings>().CargarMunicion(1);
        QuitarDinero(200);
    }

    private void OnClickCañonBofors(){
        GameObject.Find("CañonBofors(Clone)").GetComponent<Unit_Settings>().CargarMunicion(1);
        QuitarDinero(200);
    }

    private void OnClickRadarFijo(){
        //Activar Radar
    }

    private void OnClickRadarMovil(){
        //Activar Radar
    }

    private void OnClickDrone0(){
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects){
            if(obj.name == "Drone(Clone)"){
                if(obj.GetComponent<Unit_Settings>().GetNumeroDrone() == 0){
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    QuitarDinero(400);
                }
            }
            
        }
    }
    
    private void OnClickDrone1(){
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects){
            if(obj.name == "Drone(Clone)"){
                if(obj.GetComponent<Unit_Settings>().GetNumeroDrone() == 1){
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    QuitarDinero(400);
                }
            }
            
        }
    }

    private void OnClickDrone2(){
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects){
            if(obj.name == "Drone(Clone)"){
                if(obj.GetComponent<Unit_Settings>().GetNumeroDrone() == 2){
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    QuitarDinero(400);
                }
            }
            
        }
    }

    private void OnClickDrone3(){
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects){
            if(obj.name == "Drone(Clone)"){
                if(obj.GetComponent<Unit_Settings>().GetNumeroDrone() == 3){
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    QuitarDinero(400);
                }
            }
            
        }
    }

    private void OnClickDrone4(){
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects){
            if(obj.name == "Drone(Clone)"){
                if(obj.GetComponent<Unit_Settings>().GetNumeroDrone() == 4){
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    QuitarDinero(400);
                }
            }
            
        }
    }

    private void OnClickDrone5(){
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects){
            if(obj.name == "Drone(Clone)"){
                if(obj.GetComponent<Unit_Settings>().GetNumeroDrone() == 5){
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    obj.GetComponent<Unit_Settings>().CargarMunicion(1);
                    QuitarDinero(400);
                }
            }
            
        }
    }
    
}
