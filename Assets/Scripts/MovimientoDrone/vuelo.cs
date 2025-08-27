using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vuelo : MonoBehaviour
{
    public Rigidbody rigidBody;
    public float alturaDeseada = 4f; // Altura deseada del objeto sobre el terreno
    public float velocidadMaximaCaida = 4f;
    public float fuerzaAscenso = 10f;
    public float fuerzaDescenso = 10f;

    void Update()
    {
        RaycastHit hit;
        
        // Lanzar un rayo hacia abajo desde la posición del objeto
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Verificar si el rayo golpea un objeto con el tag "Terreno"
            if (hit.collider.CompareTag("Terreno"))
            {
                // Calcular la distancia entre el objeto y el terreno
                float distanciaAlTerreno = hit.distance;

                // Calcular la diferencia entre la altura deseada y la distancia al terreno
                float diferenciaAltura = alturaDeseada - distanciaAlTerreno;

                // Aplicar fuerza hacia arriba si la diferencia es positiva (el objeto está por debajo de la altura deseada)
                // o hacia abajo si la diferencia es negativa (el objeto está por encima de la altura deseada)
                if (diferenciaAltura > 0)
                {
                    rigidBody.AddForce(Vector3.up * diferenciaAltura * fuerzaAscenso, ForceMode.Acceleration);
                }
                else
                {
                    rigidBody.AddForce(Vector3.down * Mathf.Abs(diferenciaAltura) * fuerzaDescenso, ForceMode.Acceleration);
                }
            }
        }
        
        if (rigidBody.velocity.y < -velocidadMaximaCaida)
        {
            // Limitar la velocidad de caída al valor máximo permitido
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, -velocidadMaximaCaida, rigidBody.velocity.z);
        }
    }
}
