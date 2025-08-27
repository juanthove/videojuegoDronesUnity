using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CustomTypeRegistration : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonPeer.RegisterType(typeof(DatosPartida), (byte)'D', SerializeDatosPartida, DeserializeDatosPartida);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static byte[] SerializeDatosPartida(object obj)
    {
        DatosPartida datosPartida = (DatosPartida)obj;
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream();
        formatter.Serialize(memoryStream, datosPartida);
        return memoryStream.ToArray();
    }

    static object DeserializeDatosPartida(byte[] data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream memoryStream = new MemoryStream(data);
        DatosPartida datosPartida = (DatosPartida)formatter.Deserialize(memoryStream);
        return datosPartida;
    }
}
