using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Movement : MonoBehaviour
{   
    //Definir la velocidad de movimiento

    [SerializeField]
    private float velocidad = 5f;
    [SerializeField]
    private float alturaRelativa = 2f; //Esta se actualiza
    [SerializeField]
    private float alturaDesdeSuelo = 2f; //Esta es la inicial

    //Distanciamiento de unidades
    private float distanciaMinima = 10f; //Distancia a la que puede estar de otras unidade

    //Solo podremos mover la unidad si esta seleccionada desde Unit_Settings
    private bool estaSeleccionado;
    //Lugar a donde queremos mandar la unidad --> Este se ultiliza para rodear unidades
    private Vector3 posicionObjetivo;
    //Lugar a donde queremos mandar la unidad cuando hacemos click derecho
    private Vector3 posicionObjetivoOriginal;

    //Instancia de Seleccion de  unidad
    private Unit_Selectable unitSelectable;

    public void SetPosicionObjetivo(Vector3 pos){
        //transform.Translate(Vector3.zero);
        posicionObjetivo = pos;
        posicionObjetivoOriginal = pos;
    }
    void Start()
    {
        unitSelectable = GetComponent<Unit_Selectable>();   
    }

    // void Update()
    // {
    //     //Si esta seleccionado lo podemos mover, ademas de acuerdo a la configuracion de la unidad debe ser una unidad movil
    //     if(unitSelectable.GetObjetoSeleccionado()){
    //         //Si no hemos llegado a la posicion objetivo moveremos la unidad
    //         if(Mathf.FloorToInt(posicionObjetivo.x) != Mathf.FloorToInt(transform.position.x) &&
    //             Mathf.FloorToInt(posicionObjetivo.z) != Mathf.FloorToInt(transform.position.z)){
    //             //Obtenemos el terreno para saber a que altura respecto de este debe ir nuestra unidad
    //             GameObject objTerreno = GameObject.Find("Terrenos/Terreno");
    //             Terrain terreno = objTerreno.GetComponent<Terrain>();
    //             alturaRelativa = terreno.SampleHeight(transform.position) + alturaDesdeSuelo;
    //             posicionObjetivo.y = alturaRelativa;
    //             // Mover gradualmente la unidad hacia la posición objetivo
    //             MoverUnidad();
    //         }
    //         // Verificar si se hizo clic con el botón izquierdo del mouse
    //         if (Input.GetMouseButtonDown(1))
    //         {
    //             // Obtener la posición del clic del mouse en el mundo
    //             posicionObjetivo = ObtenerPosicionClic();
    //             posicionObjetivoOriginal = posicionObjetivo;
    //         }
    //     }
           
    // }

    // Método para obtener la posición del clic del mouse en el mundo

    // Update is called once per frame
    void Update()
    {
        //Si esta seleccionado lo podemos mover, ademas de acuerdo a la configuracion de la unidad debe ser una unidad movil
        if(unitSelectable.GetObjetoSeleccionado()){
            //Si no hemos llegado a la posicion objetivo moveremos la unidad
            if(Mathf.FloorToInt(posicionObjetivo.x) != Mathf.FloorToInt(transform.position.x) &&
                Mathf.FloorToInt(posicionObjetivo.z) != Mathf.FloorToInt(transform.position.z)){
                //Mantenemos la altura de la unidad respecto al terreno;
                MantenerAlturaUnidad();
                // Mover gradualmente la unidad hacia la posición objetivo
                MoverUnidad();
            }
            // Verificar si se hizo clic con el botón izquierdo del mouse
            if (Input.GetMouseButtonDown(1))
            {
                // Obtener la posición del clic del mouse en el mundo
                posicionObjetivo = ObtenerPosicionClic();
                posicionObjetivoOriginal = posicionObjetivo;
            }
        
            //MantenerDistanciaDeOtrasUnidades();
        }
    }

    public void MantenerAlturaUnidad(){
        //Obtenemos el terreno para saber a que altura respecto de este debe ir nuestra unidad
        GameObject objTerreno = GameObject.Find("Terrenos/Terreno");
        Terrain terreno = objTerreno.GetComponent<Terrain>();
        alturaRelativa = terreno.SampleHeight(transform.position) + alturaDesdeSuelo;
        posicionObjetivo.y = alturaRelativa;
    }

     public void MoverUnidad(){
        // Calcular la dirección hacia la posición objetivo
        Vector3 direccion = (posicionObjetivo - transform.position).normalized;
        // Ignorar el componente Y de la dirección para moverse solo en los ejes X y Z
        direccion.y = 0f;

        // Almacenar la rotación inicial antes de iniciar el movimiento
        Quaternion rotacionInicial = transform.rotation;

        
        
        //Manejamos la rotacion de la unidad de acuerdo hacia a donde vamos
        Quaternion rotacionDeseada = Quaternion.LookRotation(direccion);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionDeseada, Time.deltaTime * 50f);

        // Calcular el movimiento basado en la dirección y la velocidad
        Vector3 movimiento = direccion * velocidad * Time.deltaTime;

        // Aplicar el movimiento a la unidad
        transform.Translate(movimiento, Space.World);

        // Mantener la altura relativa de la unidad
        MantenerAlturaRelativa();
    }

    Vector3 ObtenerPosicionClic(){
        Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(rayo, out hit))
        {
            return hit.point;
        }
        return transform.position;
    }

    public void MantenerDistanciaDeOtrasUnidades(){
        // Calcular el vector de desplazamiento total para evitar entrar en el rango de las unidades cercanas
            Vector3 desplazamientoTotal = Vector3.zero;
            // Obtener una lista de unidades cercanas dentro de la distancia mínima
            Collider[] unidadesCercanas = Physics.OverlapSphere(transform.position, distanciaMinima, LayerMask.GetMask("Unidades"));
            foreach (Collider unidad in unidadesCercanas)
            {   
                //Chequeamos que la unidad sea distinta a si misma
                if(unidad.gameObject != gameObject){
                    // Calculamos el vector de dirección desde nuestra posición hacia la unidad cercana
                    Vector3 direccionUnidad = (unidad.transform.position - transform.position).normalized;
                    // Calculamos el desplazamiento necesario para mantener la distancia mínima
                    float distanciaNecesaria = (distanciaMinima - Vector3.Distance(transform.position, unidad.transform.position)) * 0.2f;
                    // Sumamos el vector de dirección ajustado al desplazamiento total
                    desplazamientoTotal += direccionUnidad * (distanciaNecesaria);
                }
            }    
            // Si no estamos cerca de ninguna unidad, rotar hacia la dirección de movimiento
            posicionObjetivo = posicionObjetivoOriginal;
            // Calcular el movimiento basado en la dirección y la velocidad
            // Aplicar el movimiento a la unidad
            transform.Translate(desplazamientoTotal, Space.World);
    }

    public void MantenerseFueraRangoUnidades(){

    }

    void MantenerAlturaRelativa(){
        Vector3 posicionActual = transform.position;
        posicionActual.y = alturaRelativa;
        transform.position = posicionActual;
    }

    // public bool GetEstaSeleccionado(){
    //     Debug.Log("Obtenemos el seleccionable del Movement "+ unitSelectable.GetObjetoSeleccionado());
    //     return unitSelectable.GetObjetoSeleccionado();
    // }
}
