using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Metalink.Avatar;
using BackEnd;

public class MultiplayResourceLoadManager : MonoBehaviour
{
    [SerializeField] private Transform localPlayer;
    private XRRig localXRRig;

    private void Awake()
    {
        localXRRig = localXRRig.GetComponentInChildren<XRRig>();
    }

    public void LoadAvatar()
    {

    }
}
