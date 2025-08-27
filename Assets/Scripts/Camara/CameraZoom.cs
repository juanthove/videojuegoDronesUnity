using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float velocidad = 100f;
    [SerializeField] private float fluidez = 5f;
    [SerializeField] private float maxrange = 20F;
    [SerializeField] private float xi = 2F;
    [SerializeField] private Transform cameraHolder;

    private Vector3 camaraDirection => transform.InverseTransformDirection(cameraHolder.forward);

    private Vector3 objPos;
    private float distancia;

    void Start()
    {
        objPos = cameraHolder.localPosition;
    }

    private void Valores()
    {
        distancia = Input.GetAxisRaw("Mouse ScrollWheel");
    }

    private void Zoom()
    {
        Vector3 sigPos = objPos + camaraDirection * (distancia * velocidad);
   
        if (EstaDentro(sigPos))
        {
            objPos = sigPos;
        }
        cameraHolder.localPosition = Vector3.Lerp(cameraHolder.localPosition, objPos, Time.deltaTime * fluidez);
    }

    private bool EstaDentro(Vector3 pos)
    {
        return pos.magnitude > xi && pos.magnitude < maxrange;
    }

    void Update()
    {
        Valores();
        Zoom();
    }

  
}
