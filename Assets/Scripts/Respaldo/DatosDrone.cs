using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//[System.Serializable]
public class DatosDrone
{
   public int vida;
   public Vector3 posicion;
   public int misiles;
   public string nombre; 
   public int numero;

   public DatosDrone(int vid, Vector3 pos, int mis, string nom, int num){
    this.vida = vid;
    this.posicion = pos;
    this.misiles = mis;
    this.nombre = nom;
    this.numero = num;
   }
   
   

   public int getVida(){
    return vida;
   }

    public int getNumero(){
    return numero;
   }


   public int getCantMisiles(){
    return misiles;
   }

   public string  getNombre(){
    return nombre;
   }

    public Vector3 getPos(){
        return posicion;
    }

   
}
