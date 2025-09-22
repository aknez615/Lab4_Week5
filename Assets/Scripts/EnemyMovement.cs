using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float desiredDistance = 5f;
    public float orbitSpeed = 1f;
    public float avoidanceRadius = 1.5f;
    public float maxSpeed = 5f;

    private void Update()
    {
        if (player == null) return;

        Vector3 toPlayer = player.position - transform.position;
        float distanceToPlayer = toPlayer.magnitude;

        //Orbiting
        Vector3 orbitDirection = new Vector3(-toPlayer.y, toPlayer.x, 0).normalized;
        float speedFactor = Mathf.Clamp(distanceToPlayer / desiredDistance, 0.5f, 2f);

        Vector3 orbitVelocity = orbitDirection * orbitSpeed * speedFactor;

        //Maintaining distance
        Vector3 avoidPlayer = Vector3.zero;
        if (distanceToPlayer < desiredDistance * 0.8f)
        {
            avoidPlayer = -toPlayer.normalized * (desiredDistance - distanceToPlayer);
        }

        //Avoiding enemies
        Vector3 avoidanceForce = Vector3.zero;
        Collider[] nearby = Physics.OverlapSphere(transform.position, avoidanceRadius);
        foreach (var other in nearby)
        {
            if (other.gameObject != this.gameObject && other.CompareTag("Enemy"))
            {
                Vector3 away = transform.position - other.transform.position;
                float dist = away.magnitude;
                if (dist > 0)
                {
                    avoidanceForce += away.normalized / dist;
                }
            }
        }

        //Movement
        Vector3 finalVelocity = orbitVelocity + avoidPlayer + avoidanceForce;
        finalVelocity = Vector3.ClampMagnitude(finalVelocity, maxSpeed);

        transform.position += finalVelocity * Time.deltaTime;

        //Face player
        FaceTowards(player.position);
    }
    void FaceTowards(Vector3 targetPos)
    {
        Vector3 toTarget = targetPos - transform.position;
        float angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}