using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Gem : Item
{
    public override void Take() => gameObject.SetActive(false);

    public override void Get() => gameObject.SetActive(true);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.GetItem(this);
        }
    }
}
