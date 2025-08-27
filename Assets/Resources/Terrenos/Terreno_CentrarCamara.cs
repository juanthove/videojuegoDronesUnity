using System.Collections;
using System.Collections.Generic;

namespace Terreno{
    using UnityEngine;
    public class Terreno_CentrarCamara : MonoBehaviour
    {

        public Terrain terreno; 

        // Start is called before the first frame update
        void Start()
        {
            if(terreno != null){
                //Obtenemos el centro del terreno
                Vector3 centroTerreno = terreno.terrainData.bounds.center;

                //Ajustar la posición de la cámara al centro del terreno
                Camera.main.transform.position = new Vector3(centroTerreno.x, Camera.main.transform.position.y, centroTerreno.z);
            }else{
                Debug.LogError("No se ha asignado un terrno al script Terreno_CentrarCamara");
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

}