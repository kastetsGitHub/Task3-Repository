using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    private List<Item> _spawnedGems;

    [SerializeField] private float _timeSpawning;
    [SerializeField] private Gem _gem;
    [SerializeField] private Transform _gemParent;
    [SerializeField] private List<Transform> _pointsOfSpawn;

    private void Start()
    {
        SpawnGems();
    }

    private void SpawnGems()
    {
        _spawnedGems = new List<Item>();

        foreach (Transform pointOfSpawn in _pointsOfSpawn)
        {
            Gem gem = Instantiate(_gem, pointOfSpawn.position, Quaternion.identity, _gemParent);
            _spawnedGems.Add(gem);
        }

        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        bool isSpawning = true;
        WaitForSeconds wait = new WaitForSeconds(_timeSpawning);
        int counter = 0;

        while (isSpawning)
        {
            if (!_spawnedGems[counter].gameObject.activeSelf)
            {
                Gem gem = Instantiate(_gem, _pointsOfSpawn[counter].position, Quaternion.identity, _gemParent);
                _spawnedGems[counter] = gem;
            }

            counter++;
            
            if (counter == _pointsOfSpawn.Count)
            {
                counter = 0;
            }

            yield return wait;
        }
    }

    private void OnDrawGizmos()
    {
        if (_pointsOfSpawn.Count > 0)
        {
            Gizmos.color = Color.magenta;
            float radius = .5f;

            foreach (Transform pointSpawn in _pointsOfSpawn)
            {
                Gizmos.DrawSphere(pointSpawn.position, radius);
            }
        }
    }

}
