using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    [Header("Ghost Settings")]
    public GameObject ghostPrefab;

    [Header("Recording")]
    public float recordInterval = 0.02f;

    private List<Vector3> recordedPositions = new List<Vector3>();

    private bool isRecording = false;

    void Update()
    {
        // Press R to start recording
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRecording();
        }

        // Press T to replay
        if (Input.GetKeyDown(KeyCode.T))
        {
            StopRecording();

            SpawnGhost();
        }
    }

    void StartRecording()
    {
        recordedPositions.Clear();

        isRecording = true;

        StartCoroutine(Record());
    }

    void StopRecording()
    {
        isRecording = false;
    }

    IEnumerator Record()
    {
        while (isRecording)
        {
            recordedPositions.Add(transform.position);

            yield return new WaitForSeconds(recordInterval);
        }
    }

    void SpawnGhost()
    {
        GameObject ghost = Instantiate(
            ghostPrefab,
            recordedPositions[0],
            Quaternion.identity
        );

        StartCoroutine(Replay(ghost));
    }

    IEnumerator Replay(GameObject ghost)
    {
        foreach (Vector3 position in recordedPositions)
        {
            ghost.transform.position = position;

            yield return new WaitForSeconds(recordInterval);
        }
    }
}