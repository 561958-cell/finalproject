using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider2D doorCollider;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OpenDoor()
    {
        doorCollider.enabled = false;

        // Optional visual fade
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
    }

    public void CloseDoor()
    {
        doorCollider.enabled = true;

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}