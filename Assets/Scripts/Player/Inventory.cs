using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class Inventory : MonoBehaviour
{
    public IReadOnlyList<Item> CollectedItems;

    private List<Item> _items;

    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.OnHasTook += PutItem;
    }

    private void OnDisable()
    {
        _player.OnHasTook -= PutItem;
    }

    private void Start()
    {        
        _items = new List<Item>();
        CollectedItems = _items;
    }

    private void PutItem(Item collectable)
    {
        _items.Add(collectable);
        collectable.Take();
    }
}
