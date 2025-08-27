using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamaraChange : MonoBehaviour
{
    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject camera2;

    void Start()
    {
        camera1.gameObject.SetActive(true);
        camera2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            camera1.gameObject.SetActive(true);
            camera2.gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            camera1.gameObject.SetActive(false);
            camera2.gameObject.SetActive(true);
        }


    }
}
