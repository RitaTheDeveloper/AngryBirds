using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _collider;

    private bool _hasBeenLaunched = false;
    private bool _shouldFaceVeldir = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();       
    }
    private void Start()
    {
        _rb.isKinematic = true;
        _collider.enabled = false;
    }

    private void FixedUpdate()
    {
        if (_hasBeenLaunched && _shouldFaceVeldir)
        {
            transform.right = _rb.velocity;
        }
    }

    public void LaunchShell(Vector2 direction, float force)
    {
        _rb.isKinematic = false;
        _collider.enabled = true;

        _rb.AddForce(direction * force, ForceMode2D.Impulse);

        _hasBeenLaunched = true;
        _hasBeenLaunched = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _shouldFaceVeldir = false;
    }
}
