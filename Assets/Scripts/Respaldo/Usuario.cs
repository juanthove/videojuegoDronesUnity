using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Usuario
{
    public string nombre;
    public string pass;

    public Usuario(string nombre, string passw)
    {
        this.nombre = nombre;
        this.pass = passw;
    }
}
