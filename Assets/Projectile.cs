using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Team team;
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private float _radius = .2f;


    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * _speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radius);

        for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out HurtBox h))
            {
                if (h.team != team)
                {
                    h.Hit(_damage);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
