using UnityEngine;
public class RockManager_level2_2 : MonoBehaviour
    {
        [Header("Rock Spawning")]
        [SerializeField] private int maxRocks = 10;
        [SerializeField] private GameObject rockPrefab;
        [SerializeField] private float spawnMinDelay = 3f;
        [SerializeField] private float spawnMaxDelay = 5f;
        
        private BoxCollider2D boxCollider;
        private float startX;
        private float endX;
        private int rockCount = 0;
        private float delay = 1f;
        private float timer = 20;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            
            var bounds = boxCollider.bounds;
            startX = bounds.min.x;
            endX = bounds.max.x;
        }

        private void Update()
        {
            if (rockCount < maxRocks && timer >= delay)
            {
                SpawnRock();
                timer = 0;
                delay = UnityEngine.Random.Range(spawnMinDelay, spawnMaxDelay);
            }
            
            timer += Time.deltaTime * 1;
        }
        
        void SpawnRock()
        {
            var position = new Vector3(UnityEngine.Random.Range(startX, endX), transform.position.y, transform.position.z);
            var rock = Instantiate(rockPrefab, position, Quaternion.identity);
            rock.gameObject.GetComponent<Rock_level2_2>().SetManager(this);
            
            rockCount++;
        }

        public void RockDestroyed()
        {
            rockCount--;
        }
    }