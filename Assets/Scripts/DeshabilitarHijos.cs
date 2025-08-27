using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeshabilitarHijos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            // Obtener el componente que deseas deshabilitar en el hijo
            MonoBehaviour ui = child.GetComponent<ManejadorInformacionUI>();

            // Si se encuentra el componente, se deshabilita
            if (ui != null)
            {
                ui.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
