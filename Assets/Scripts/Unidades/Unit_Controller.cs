using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Controller : MonoBehaviour
{
    Unit_Settings unitConfig;
    Unit_Selectable unitSelect;
    //Unit_Movement unitMovement;

    [SerializeField]
    public string tipoUnidad;

    // Start is called before the first frame update
    void Start()
    {
        unitConfig = GetComponent<Unit_Settings>();
        unitConfig.CrearUnidad(tipoUnidad);
        unitSelect = GetComponent<Unit_Selectable>();
        //unitMovement = new UnitMovement();
    }

    // Update is called once per frame
    void Update(){    
        //Llamado al movimiento si el objetivo esta seleccionado
        if(unitSelect.GetObjetoSeleccionado()){
            unitConfig.LanzarProyectil();
        }
        if(!unitSelect.enabled){

            Debug.Log("Tenemos la seleccion desactivada desde Unit_Controller" + tipoUnidad);
        }
        
    }

        
}
