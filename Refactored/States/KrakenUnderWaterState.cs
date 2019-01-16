using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenUnderWaterState : KrakenState {

    [SerializeField] private float timerUnderWater = 7;
    [HideInInspector] public bool canUnder;
    private float minWait = 7f;
    private float maxWait = 15f;


    public override void OnExit()
    {
    }

    public override void OnStart()
    {
    }

    public override void OnUpdate()
    {
        timerUnderWater -= Time.deltaTime;
        if (timerUnderWater <= 0)
        {
            HideInWater();
        }
    }

    /// <summary>
    /// -- le reste du code pour caché dans l'eau est partagé dans une classe apart
    /// -- il serait bon de le ramené ici pour pouvoir gérer son beheavior, mais dans cette version du code
    /// -- cela prendrait pas mal de temps...
    /// </summary>
    void HideInWater()
    {
        canUnder = true;
        timerUnderWater = Random.Range(minWait, maxWait);
    }
}
