using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    public float cellSize = 1.0f;
    private Vector2 targetPosition;
    [SerializeField] KeyCode upKey;
    [SerializeField] KeyCode downKey;
    [SerializeField] KeyCode rightKey;
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode shootKey;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 5.0f;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        Vector2 movementInput = Vector2.zero;

        if (Input.GetKeyDown(rightKey))
        {
            movementInput.x += 1;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        if (Input.GetKeyDown(leftKey))
        {
            movementInput.x -= 1;
            transform.rotation = Quaternion.Euler(0, 0, -270);
        }
        if (Input.GetKeyDown(upKey))
        {
            movementInput.y += 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKeyDown(downKey))
        {
            movementInput.y -= 1;
            transform.rotation = Quaternion.Euler(0, 0, -180);
        }

        if (Input.GetKeyDown(shootKey))
        {
            ShootProjectile();
        }

        if (movementInput != Vector2.zero)
        {
            targetPosition += movementInput * cellSize;

            transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        }

        void ShootProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.up, transform.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();


            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(projectile, 4.0f);
        }

    }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Projectile"))
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }

}
