using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour 
{
    [SerializeField] private float velocidad = 0.1f;
    [SerializeField] private float fluidez = 5f;
    [SerializeField] private Vector2 rango = new(100, 100);

    private Vector3 objPos;
    private Vector3 distancia;

    void Start()
    {
       
    }

    private void Valores()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 derecha = transform.right * x;
        Vector3 atras = transform.forward * z;

        distancia = (atras + derecha).normalized;
    }
   
    
    private void Mover()
    {
        Vector3 siguientePos = objPos + distancia * velocidad;
        if(EstaDentro(siguientePos))
        {
            objPos = siguientePos;
        }
        transform.position = Vector3.Lerp(transform.position, objPos, Time.deltaTime * fluidez);
                
    }

    private bool EstaDentro(Vector3 pos)
    {
        return pos.x > -rango.x && pos.x < rango.x && pos.z > -rango.y && pos.z < rango.y;
    }


    void Update()
    {
        Valores();
        Mover();
    }

   /* private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 5f);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(rango.x * 2f, 5f, rango.y * 2f));
    }*/
}
