using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float strength = 3, delay = 0.15f;

    public UnityEvent OnBegin, OnDone;
    EnemyMovement movement;

    public Transform tempTarget;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<EnemyMovement>();
        rb = GetComponent<Rigidbody2D>();   
    }

    public void ApplyKnockback (Transform sender)
    {
        if (movement.target != null)
        {
            tempTarget = movement.target;
        }
        movement.target = null;
        StopAllCoroutines();
        OnBegin?.Invoke();

        Vector2 direction = (transform.position- sender.position).normalized;
        
       rb.AddForce(direction * strength, ForceMode2D.Impulse);

        StartCoroutine(ResetKnockback());

    }

    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
        movement.target = tempTarget;
        OnDone?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

}
