using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Door connectedDoor;

    private int objectsOnPlate = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectsOnPlate++;

            connectedDoor.OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectsOnPlate--;

            if (objectsOnPlate <= 0)
            {
                objectsOnPlate = 0;

                connectedDoor.CloseDoor();
            }
        }
    }
}