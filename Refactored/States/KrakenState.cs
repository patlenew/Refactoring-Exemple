using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KrakenState : MonoBehaviour
{
    public KrakenFSM krakenFSM;

    public virtual void Start()
    {
        krakenFSM = GetComponent<KrakenFSM>();
    }

    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
