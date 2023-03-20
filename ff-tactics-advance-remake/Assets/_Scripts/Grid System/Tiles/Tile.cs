using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class Tile : MonoBehaviour
    {
        [field: SerializeField] public List<NeighborTile> Neighbors { get; private set; }
        [field: SerializeField] public Transform ArrivalTransform { get; private set; }

        private void OnValidate()
        {
            Neighbors = new List<NeighborTile>();

            CheckForNeighbors(Vector3.forward);
            CheckForNeighbors(Vector3.back);
            CheckForNeighbors(Vector3.right);
            CheckForNeighbors(Vector3.left);
        }

        private void CheckForNeighbors(Vector3 direction)
        {
            Ray ray = new Ray(transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hit, 1f))
            {
                var tile = hit.transform.GetComponent<Tile>();

                if (tile)
                {
                    Neighbors.Add(new NeighborTile(tile, Vector3Int.FloorToInt(ray.direction), (tile.ArrivalTransform.position - ArrivalTransform.position).magnitude));
                }
            }
        }
    }
}

