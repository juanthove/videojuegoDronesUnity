using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terreno_BordesNegros : MonoBehaviour
{   

    private Terrain bordeTerreno;
    private Vector3 tamanoOriginal;
    private Vector3 posicionOriginal;
    // Start is called before the first frame update
    void Start()
    {  
        GameObject terrenoObj = GameObject.Find("Terrenos/Terreno");
        GameObject bordeTerrenoObj = GameObject.Find("Terrenos/Borde Terreno");

        Terrain terreno = terrenoObj.GetComponent<Terrain>();
        bordeTerreno = bordeTerrenoObj.GetComponent<Terrain>();

        if(terreno != null && bordeTerreno != null){
            //Tamano
            tamanoOriginal = terreno.terrainData.size;
            Vector3 tamanoActual = tamanoOriginal;
            tamanoActual.x += 400f;
            tamanoActual.z += 400f;
            bordeTerreno.terrainData.size = tamanoActual;

            //posicion
            posicionOriginal = terreno.transform.position;
            bordeTerreno.transform.position = new Vector3(bordeTerreno.transform.position.x -200f, -30f, bordeTerreno.transform.position.z-200f);

        }else{
            Debug.Log("Algo fallo en la obtencion de terrenos");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit(){
        if(bordeTerreno != null){
            bordeTerreno.transform.position = posicionOriginal;
            bordeTerreno.terrainData.size = tamanoOriginal;
        }
    }


}
