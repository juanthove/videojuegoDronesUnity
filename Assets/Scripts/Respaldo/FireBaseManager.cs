using UnityEngine;
using Proyecto26;
using TMPro; 
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class FirebaseManager : MonoBehaviour
{
    [Header ("Login")]
    public TMP_InputField usuario;
    public TMP_InputField pass;
    public TMP_Text mens;
    public Button loginButton; 
    private bool fallo = true;
    
    [Header ("Sing Up")]
    public TMP_InputField usuarioR;
    public TMP_InputField passR;
    public TMP_Text mensR;
    public Button signUpButton;

    

    private void Start()
    {
        loginButton.onClick.AddListener(LoginButtonClicked);
            
        signUpButton.onClick.AddListener(SignUpButtonClicked);

       // guardarPartida.onClick.AddListener(salvarPartida);
    }

    private void SignUpButtonClicked()
    {
       
        agregarUsuario(usuarioR.text,passR.text);
    }

    private void LoginButtonClicked()
    {
        // Llama a la función LoginButton en el AuthManager
        login(usuario.text,pass.text);
    }
    public void agregarUsuario(string nombre, string pass)
    {
        Usuario nuevoUsuario = new Usuario(nombre, pass);
        
        string firebaseURL = $"https://proyectoude-5c466-default-rtdb.firebaseio.com/usuarios/{nombre}/.json";
        
        // Utiliza el nombre como identificador en la URL
        RestClient.Put(firebaseURL, nuevoUsuario).Then(response =>
        {
            mensR.text = "Usuario agregado correctamente";
            Debug.Log("Usuario agregado correctamente");
        }).Catch(error =>
        {
            mensR.text = "Error al agregar usuario: ";
            Debug.LogError("Error al agregar usuario: " + error.Message);
        });
    }

    public void login(string nombre, string pass)
    {
        // Realiza una solicitud GET para obtener el usuario con el nombre proporcionado
        RestClient.Get<Usuario>($"https://proyectoude-5c466-default-rtdb.firebaseio.com/usuarios/{nombre}.json").Then(response =>
        {
            if (response != null)
            {
                Usuario usuario = response;

                // Verifica si la contraseña coincide
                if (usuario != null && usuario.pass == pass)
                {
                    fallo = false;
                    mens.text = "Inicio de sesión exitoso";
                    Debug.Log("Inicio de sesión exitoso");
                }
                else
                {
                    mens.text = "Nombre de usuario o contraseña incorrectos";
                    Debug.LogWarning("Nombre de usuario o contraseña incorrectos");
                }
            }
            else
            {
                mens.text = "El usuario no existe";
                Debug.LogWarning("El usuario no existe");
            }
        }).Catch(error =>
        {
            mens.text = "Error al iniciar sesión: " + error.Message;
            Debug.LogError("Error al iniciar sesión: " + error.Message);
        });
    }

  // List<DatosDrone> drones = drones.GetComponent<DatosPartida>. GetListaDatosDrones();

    /* public void salvarPartida(string nombrePartida, string nombreAtacante, string nombreDefensa, List<DatosDrone> drones){
      /*  string firebaseURL = $"https://proyectoude-5c466-default-rtdb.firebaseio.com/jugadores/{nombrePartida}.json";

     

        DatosPartida data = new DatosPartida(nombreAtacante, nombreDefensa, drones);

        RestClient.Put(firebaseURL, data).Then(response =>
        {
            mensR.text = "Usuario agregado correctamente";
            Debug.Log("Usuario agregado correctamente");
        }).Catch(error =>
        {
            mensR.text = "Error al agregar usuario: ";
            Debug.LogError("Error al agregar usuario: " + error.Message);
        });

    }
*/
    public bool GetFallo(){
        return fallo;
    }

    public bool existeUsuario(string nombre){
        bool existe = false;
        RestClient.Get<Usuario>($"https://proyectoude-5c466-default-rtdb.firebaseio.com/jugadores/{nombre}.json").Then(response =>
        {
            if (response != null)
            {
                existe = true;   
            }
        }).Catch(error =>
        {
            mens.text = "Error al iniciar sesión: " + error.Message;
            Debug.LogError("Error al iniciar sesión: " + error.Message);
        });

        return existe;
    }
    
}