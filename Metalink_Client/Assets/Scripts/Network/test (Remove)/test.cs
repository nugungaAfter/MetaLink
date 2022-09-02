using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using UnityEngine;

public class test : MonoBehaviour
{
    private void Awake()
    {
        ManagementPacket packet = new ManagementPacket(TrackingSubscriberType.OnlyMe, "localhost", "UUID ¿‘¥œ¥Ÿ.");
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ManagementPacket));
        MemoryStream ms = new MemoryStream();
        serializer.WriteObject(ms, packet);
        //Debug.Log(Encoding.UTF8.GetString(ms.ToArray()));
    }
}
