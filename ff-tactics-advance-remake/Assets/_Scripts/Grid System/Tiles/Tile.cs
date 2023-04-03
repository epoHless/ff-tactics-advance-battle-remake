using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public class Tile : MonoBehaviour
    {
        public bool Visited = false;
        
        #region Properties

        [field: SerializeField] public List<NeighborTile> Neighbors { get; private set; }
        [field: SerializeField] public Transform ArrivalTransform { get; private set; }
        
        [field: SerializeField] public int Height { get; private set; }

        #region Path Inits

        private BoxCollider Collider => GetComponent<BoxCollider>();
        public GameObject SelectionBox => ArrivalTransform.GetChild(0).gameObject;
        public bool CanTravel => SelectionBox.gameObject.activeInHierarchy;

        #endregion

        #region Pathfinding Properties

        public int G { get; set; }
        public int H { get; set; }

        public int F => G + H;
        
        public Tile PreviousTile { get; set; }
        
        #endregion

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (Neighbors.Count <= 0) GetNeighbors();
        }

        #endregion

        #region Methods

        [ContextMenu("Calculate Neighbors")]
        private void GetNeighbors()
        {
            ArrivalTransform = transform.GetChild(0);
            Height = Mathf.FloorToInt(Collider.size.y);

            Neighbors = new List<NeighborTile>();

            CheckForNeighbors(Vector3.forward);
            CheckForNeighbors(Vector3.back);
            CheckForNeighbors(Vector3.right);
            CheckForNeighbors(Vector3.left);
        }

        private void CheckForNeighbors(Vector3 direction)
        {
            var position = transform.position;
            position.y = .5f;
            
            Ray ray = new Ray(position, direction);
            
            if (Physics.Raycast(ray, out RaycastHit hit, 1f))
            {
                var tile = hit.transform.GetComponent<Tile>();

                if (tile)
                {
                    Neighbors.Add(new NeighborTile(
                        tile, 
                        Vector3Int.FloorToInt(ray.direction),
                        Height));
                }
            }
        }

        #endregion

        
    }
}

