using UnityEngine;

namespace L2.Scripts
{
    public enum LocationName
    {
        Bedroom,
        Livingroom,
        Kitchen,
        Stairs
    };

    public class LevelManager : MonoBehaviour
    {
        [SerializeField] GameObject stairs;
        [SerializeField] GameObject defaultSpawnPoint;
        
        Transform currentSpawnPoint;
        LocationName currentLocationName;
        private bool isPlayerUnderTable;
        private Dad dad;

        void Awake()
        {
            dad = FindObjectOfType<Dad>();
            currentSpawnPoint = defaultSpawnPoint.transform;
            FindObjectOfType<Player>().disableAutoWalk();
            currentLocationName = LocationName.Bedroom;
        }

        void Update()
        {
            if (isPlayerUnderTable)
                dad.GoDaddyGo();
        }
        
        public void SetPlayerUnderTable()
        {
            isPlayerUnderTable = true;
        }
        
        public void RespawnPlayer()
        {
            FindObjectOfType<Player>().transform.position = currentSpawnPoint.position;
        }

        public void SetSpawnPoint(GameObject point)
        {
            currentSpawnPoint = point.transform;

            // Disable stairs if player reached the living room
            if (currentLocationName == LocationName.Livingroom)
                stairs.GetComponent<EdgeCollider2D>().enabled = false;
        }

        public void SetPlayerLocation(LocationName locationName)
        {
            currentLocationName = locationName;
        }
        
    }
}