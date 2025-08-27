using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Linq;

public class Fog : MonoBehaviour
{

    /*[SerializeField] private float rangoVision = 30f;
    [SerializeField] private Collider[] colliders;
    [SerializeField] private GameObject[] enemigos;
    [SerializeField] private GameObject hijo;

    // Start is called before the first frame update
    void Start()
    {
        int layerToSearch = 0;
        if (PhotonNetwork.IsMasterClient)
        {
             layerToSearch = 8;
        }
        else
        {
             layerToSearch = 7;
        }
        
        GameObject[] objectsInLayer = FindObjectsOfType<GameObject>().Where(obj => obj.layer == layerToSearch).ToArray();
        foreach (GameObject go in objectsInLayer)
        {
            hijo = go.transform.GetChild(0).gameObject;
            hijo.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        colliders = Physics.OverlapSphere(transform.position, rangoVision);
        foreach (Collider co in colliders)
        {
            if (co.gameObject.CompareTag("fog"))
            {
               
                hijo = co.transform.GetChild(0).gameObject;
                //Debug.Log("ACTIVO = " + hijo.name);
                hijo.SetActive(true);

            }
        }
   
        
        hijo = null;
    }*/

    

}