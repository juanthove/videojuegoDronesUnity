using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Explosion : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EsperarYEliminarExplosion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EsperarYEliminarExplosion(){
            // Espera 2 segundos
            yield return new WaitForSeconds(1.5f);
            PhotonNetwork.Destroy(gameObject);
        }
}
