using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosBofors
{
    public int vida;
    public Vector3 posicion;
    public int cantBalas;
    public string numero;

    public DatosBofors(int vid, Vector3 pos, int mis, string num){
        this.vida = vid;
        this.posicion = pos;
        this.cantBalas = mis;
        this.numero = num;
    } 

    public string getNumero(){
        return numero;
    }

 }
