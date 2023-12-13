using UnityEngine;

namespace L3._2.Scripts
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] private Sprite openedDoorSprite;
        [SerializeField] private Sprite closedDoorSprite;
        
        private SpriteRenderer spriteRenderer;
        private EdgeCollider2D collider;
        private bool isOpened;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<EdgeCollider2D>();
            Close();
        }

        public void Open()
        {
            spriteRenderer.sprite = openedDoorSprite;
            collider.enabled = false;
        }

        public void Close()
        {
            spriteRenderer.sprite = closedDoorSprite;
            collider.enabled = true;
        }

        public bool IsOpened()
        {
            return isOpened;
        }
    }
}
