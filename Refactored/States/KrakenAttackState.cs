using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenAttackState : KrakenState
{
    [SerializeField] private GameObject shootDirection;
    [SerializeField] private GameObject projectile;
    private float timer = 0.5f;
    private float yOffset = 5f;
    private float yRotationOffset = 180f;
    private float boostIntensity = 100f;
    private Collider krakenCollider;

    public override void Start()
    {
        base.Start();
        krakenCollider = krakenFSM.kraken.GetComponent<Collider>();
    }

    public override void OnExit()
    {
    }

    public override void OnStart()
    {
        timer = 0.5f;

        GameObject newProjectile = Instantiate(projectile);
        newProjectile.transform.position = transform.position + Vector3.up * yOffset;
        newProjectile.transform.eulerAngles = transform.eulerAngles - Vector3.up * yRotationOffset;

        Physics.IgnoreCollision(krakenCollider, newProjectile.GetComponent<Collider>());
        newProjectile.GetComponent<ProjectileManager>().krakenCol = krakenFSM.kraken.gameObject;

        Rigidbody projectileRigidbody = newProjectile.AddComponent<Rigidbody>();
        projectileRigidbody.velocity = (shootDirection.transform.position - transform.position).normalized * boostIntensity;
    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            krakenFSM.SetState(KrakenFSM.EnumState.PATROL);
        }
    }
}
