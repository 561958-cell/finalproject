using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRecorder : MonoBehaviour
{
    public GameObject ghostPrefab;

    private List<InputFrame> recordedFrames =
        new List<InputFrame>();

    private bool isRecording = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRecording();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            StopRecording();

            SpawnGhost();
        }
    }

    void StartRecording()
    {
        recordedFrames.Clear();

        isRecording = true;

        StartCoroutine(RecordInputs());
    }

    void StopRecording()
    {
        isRecording = false;
    }

    IEnumerator RecordInputs()
    {
        while (isRecording)
        {
            float horizontal =
                Input.GetAxisRaw("Horizontal");

            bool jump =
                Input.GetButton("Jump");

            recordedFrames.Add(
                new InputFrame(horizontal, jump)
            );

            yield return new WaitForFixedUpdate();
        }
    }

    void SpawnGhost()
    {
        GameObject ghost =
            Instantiate(
                ghostPrefab,
                transform.position,
                Quaternion.identity
            );

        PlayerMovement ghostMovement =
            ghost.GetComponent<PlayerMovement>();

        ghostMovement.isReplayGhost = true;

        StartCoroutine(
            ReplayInputs(ghostMovement)
        );
    }

    IEnumerator ReplayInputs(
        PlayerMovement ghostMovement
    )
    {
        foreach (InputFrame frame in recordedFrames)
        {
            ghostMovement.SetReplayInput(
                frame.horizontal,
                frame.jumpPressed
            );

            yield return new WaitForFixedUpdate();
        }
    }
}
