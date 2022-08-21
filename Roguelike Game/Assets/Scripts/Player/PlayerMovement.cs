using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    [SerializeField] private bool grounded;
    [SerializeField] private LayerMask ground;

    private Vector2 _movement;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GroundCheck();
    }

    public void Move(Vector2 input)
    {
        var x = input.x * speed * Time.fixedDeltaTime;

        _movement = new Vector2(x, _rb.velocity.y);
        _rb.velocity = _movement;
    }

    public IEnumerator JumpBuffer()
    {
        var t = 0.05f;
        
        while (t > 0)
        {
            if (grounded)
            {
                Jump();
                yield break;
            }

            t -= 0.001f;
            yield return new WaitForSeconds(0.001f);
        }
    }
    
    private void Jump()
    {
        if (!grounded) return;

        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }

    private void GroundCheck()
    {
        grounded = Physics2D.Linecast(transform.position, transform.position + Vector3.down * 0.9f, ground);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.9f);
    }
}
