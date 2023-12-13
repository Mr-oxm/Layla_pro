using UnityEngine;

namespace L3.Scripts
{
    public class LevelManager : global::LevelManager
    {
        [SerializeField] private GameObject criminalTrigger;
        [SerializeField] private Player player;
        [SerializeField] private GameObject criminalPrefab;
        [SerializeField] private GameObject criminalSpawnPoint;
        [SerializeField] private GameObject criminalParent;
        [SerializeField] private int criminalCount = 3;
        
        private bool isCriminalTriggered;

        private void Start()
        {
            player = FindObjectOfType<Player>();
            player.disableAutoWalk();
        }

        private void Update()
        {
            if (!isCriminalTriggered && player.transform.position.x >= criminalTrigger.transform.position.x)
                TriggerCriminal();
        }
        
        private void InstantiateCriminal()
        {
            Instantiate(criminalPrefab, criminalSpawnPoint.transform.position, Quaternion.identity, criminalParent.transform);
        }

        private void TriggerCriminal()
        {
            isCriminalTriggered = true;
            const float delay = 0.5f;
            for (var i = 0; i < criminalCount; i++)
                Invoke("InstantiateCriminal",  i * delay);
        }
        
        public override void RespawnPlayer()
        {
            foreach (var p in FindObjectsOfType<Player>())
                p.transform.position = CurrentCheckpoint.transform.position;

            foreach (var criminal in FindObjectsOfType<EnemyAI>())
            {
                Destroy(criminal.gameObject);
                Destroy(criminal);
            }
                
            TriggerCriminal();
        }
    }
}
