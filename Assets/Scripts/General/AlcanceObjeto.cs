using UnityEngine;

public class AlcanceObjeto : MonoBehaviour
{
    public float radioAlcance = 10f;
    public int numeroSegmentos = 36;
    
    //Todo lo comentado es lo que dibujaria el circulo rosa, de querer usarlo, deseleccionar
/*
   public LineRenderer lineRenderer;

    private void Start()
    {
        DibujarCirculo();
    }

    private void DibujarCirculo()
    {
        lineRenderer.positionCount = numeroSegmentos + 1;
        lineRenderer.useWorldSpace = true; // Usar coordenadas del mundo
    }
 


    private void Update()
    {
        // Obtener la posición del drone
        Vector3 posicionDrone = transform.position;

        // Actualizar las posiciones de los puntos del círculo
        for (int i = 0; i <= numeroSegmentos; i++)
        {
            float angulo = Mathf.PI * 2f / numeroSegmentos * i;
            float x = Mathf.Sin(angulo) * radioAlcance + posicionDrone.x;
            float z = Mathf.Cos(angulo) * radioAlcance + posicionDrone.z;
            Vector3 punto = new Vector3(x, posicionDrone.y, z);
            lineRenderer.SetPosition(i, punto);
        }

    }
       
*/

    // Función para verificar si una posición está dentro del alcance del círculo
    public Collider[] EstaEnAlcance()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioAlcance);
        return colliders;
    }
}