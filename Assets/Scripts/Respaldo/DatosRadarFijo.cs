using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatosRadarFijo 
{
   public int vida;
   public Vector3 posicion;
   public string nombre;

   public DatosRadarFijo(int v, Vector3 pos, string nom){
    this.vida  = v;
    this.posicion = pos;
    this.nombre = nom;
   }

   public string getNombre(){
        return nombre;
   }

    public Vector3 getPosicion(){
        return posicion;
    }

}
