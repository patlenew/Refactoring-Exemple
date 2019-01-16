using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenPatrolState : KrakenState {

    [SerializeField] private int waypointInt = 0;
    [SerializeField] private Transform[] waypoints;
    public GameObject objectTarget;
    private float minimalRangeForAttack = 58f;
    private float transitionValue = 8f;
    private float lerpSpeed = 0.7f;
    private float cooldown = 0f;
    private bool isCooldownActive = false;

    public override void OnExit()
    {
    }

    public override void OnStart()
    {
    }

    public override void OnUpdate()
    {

        if (CheckIfCanAttack())
        {
            cooldown = 5f;
            isCooldownActive = true;

            krakenFSM.SetState(KrakenFSM.EnumState.ATTACK);
            return;
        }

        ActivateCooldown();
        SwitchWaypoints();
        krakenFSM.kraken.transform.LookAt(objectTarget.transform);
    }

    void SwitchWaypoints()
    {
        if (Vector3.Distance(transform.position, waypoints[waypointInt].position) >= transitionValue)
        {
            transform.position = Vector3.Lerp(transform.position, waypoints[waypointInt].position, lerpSpeed * Time.deltaTime);
        }
        else
        {
            CheckWaypointNumb();
        }
    }

    private void CheckWaypointNumb()
    {
        waypointInt = (waypointInt >= waypoints.Length - 1) ? 0 : waypointInt += 1;
    }

    void ActivateCooldown()
    {
        if (!isCooldownActive)
        {
            return;
        }

        cooldown -= Time.deltaTime;
        if (cooldown <= 0f)
        {
            cooldown = 0f;
            isCooldownActive = false;
        }
    }

    bool CheckIfCanAttack()
    {
        return (Vector3.Distance(transform.position, objectTarget.transform.position) <= minimalRangeForAttack) && cooldown == 0f;
    }
}
