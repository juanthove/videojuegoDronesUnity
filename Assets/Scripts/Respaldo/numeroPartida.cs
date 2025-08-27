using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class numeroPartida : MonoBehaviour
{
    public int numeroPartidas;
    public int numeroDrone;

    public numeroPartida(int n, int m){
        this.numeroPartidas = n;
        this.numeroDrone = m;
    }

    public int getPartida(){
        return numeroPartidas;
    }
    public int getNumeroDrones(){
        return numeroDrone;
    }
}
