using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraRotate : MonoBehaviour
{
    [SerializeField] private float velocidad = 1f;
    [SerializeField] private float fluidez = 5f;

    private float objAngulo;
    private float anguloActual;

    void Start()
    {

    }

    private void Valores()
    {
        if(Input.GetMouseButton(1))
        {
            objAngulo = objAngulo + Input.GetAxisRaw("Mouse X") * velocidad;
        }
       
    }
  
    private void Rotar()
    {
        anguloActual = Mathf.Lerp(anguloActual, objAngulo, Time.deltaTime * fluidez);
        transform.rotation = Quaternion.AngleAxis(anguloActual, Vector3.up);
    }
    void Update()
    {
        Valores();
        Rotar();
    }
}
