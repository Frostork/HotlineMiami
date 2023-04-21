using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public LayerMask EnemyLayer;
    public List<Enemy> Enemies;

    public static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (Contains(EnemyLayer, other.gameObject.layer))
        {
            if (other.GetComponent<Enemy>() != null)
            {
                Enemies.Add(other.GetComponent<Enemy>());
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (Contains(EnemyLayer, other.gameObject.layer))
        {
            if (Enemies.Contains(other.GetComponent<Enemy>()))
            {
                Enemies.Remove(other.GetComponent<Enemy>());
            }
        }
    }

    public void Attack()
    {
        foreach (var enemi in Enemies)
        {
            enemi.TakeDamage();
        }
    }
}