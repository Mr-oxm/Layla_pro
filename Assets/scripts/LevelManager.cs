using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject CurrentCheckpoint;
    public virtual void RespawnPlayer()
    {
        foreach (var p in FindObjectsOfType<Player>())
            p.transform.position = CurrentCheckpoint.transform.position;
        
        foreach (var criminal in FindObjectsOfType<EnemyAI>())
            criminal.RespawnEnemy();
    }
}
