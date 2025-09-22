using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] InputReader input;
    [SerializeField] GameObject laserPrefab;

    [Header("Settings")]
    [SerializeField] float speed = 6f;
    [SerializeField] private float fireCooldown = 0.25f;
    private float horizontalScreenLimit = 10f;
    private float verticalScreenLimit = 6f;
    private bool canShoot = true;

    private Vector2 moveInput;

    void OnEnable()
    {
        input.Move += OnMove;
        input.Attack += OnAttack;
    }

    void OnDisable()
    {
        input.Move -= OnMove;
        input.Attack -= OnAttack;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0) * speed * Time.fixedDeltaTime;
        transform.Translate(move);
    }

    public void Move(Vector2 direction)
    {
        Vector3 move = new Vector3(direction.x, direction.y, 0) * speed * Time.fixedDeltaTime;
        transform.Translate(move);

        if (transform.position.x > horizontalScreenLimit)
        {
            transform.position = new Vector3(-horizontalScreenLimit, transform.position.y, 0);
        }
        else if (transform.position.x < -horizontalScreenLimit)
        {
            transform.position = new Vector3(horizontalScreenLimit, transform.position.y, 0);
        }

        if (transform.position.y > verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, -verticalScreenLimit, 0);
        }
        else if (transform.position.y < -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, verticalScreenLimit, 0);
        }
    }

    public void OnMove(Vector2 movement)
    {
        moveInput = movement;
    }

    public void OnAttack(Vector2 _)
    {
        if (!canShoot) return;

        Instantiate(laserPrefab, transform.position + Vector3.up, Quaternion.identity);
        canShoot = false;
        Invoke(nameof(ResetShoot), fireCooldown);
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
    }

    private void ResetShoot() => canShoot = true;
}
