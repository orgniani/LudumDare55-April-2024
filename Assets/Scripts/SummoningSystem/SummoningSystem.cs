using System.Collections.Generic;
using UnityEngine;

public class SummoningSystem : MonoBehaviour
{
    [SerializeField] private LineFeedback linePrefab;
    [SerializeField] private Canvas canvas;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private bool pressedOnce = false;

    private List<LineFeedback> activeLines= new List<LineFeedback>();
    private List<LineFeedback> linesPool = new List<LineFeedback>();

    private List<Vector3> usedPositions = new List<Vector3>();

    private Vector3 addAllPos;
    [SerializeField] private Vector3 hasWonValue;

    private const float epsilon = 0.005f;
    public void ButtonPressed(GameObject star)
    {
        if (!pressedOnce)
        {
            pressedOnce = true;
            startPosition = star.transform.position;
        }

        else
        {
            endPosition = star.transform.position;

            if (startPosition == endPosition) return;

            Vector3 usedPosition = startPosition + endPosition;

            if(usedPositions.Contains(usedPosition))
            {
                Debug.Log("A line has already been instantiated here!");
            }

            else
            {
                SpawnLine();

                usedPositions.Add(usedPosition);
                addAllPos += usedPosition;

                //Debug.Log("ALL POSITIONS SO FAR: " + addAllPos);

                ResetState();

                PuzzleWonCheck();
            }
        }
    }

    private void SpawnLine()
    {
        if (linesPool.Count == 0)
        {
            LineFeedback lineFeedback = Instantiate(linePrefab, canvas.transform);
            lineFeedback.transform.position = startPosition;

            lineFeedback.ShowShotDirection(endPosition);

            activeLines.Add(lineFeedback);
        }
        else
        {
            var reusedLine = linesPool[0];
            reusedLine.gameObject.SetActive(true);
            reusedLine.transform.position = startPosition;
            reusedLine.ShowShotDirection(endPosition);

            activeLines.Add(reusedLine);
            linesPool.Remove(reusedLine);
        }
    }

    public void RestartPuzzle(AudioSource failAudio)
    {
        failAudio.Play();

        pressedOnce = false;
        addAllPos = Vector3.zero;
        usedPositions.Clear();

        foreach (var line in activeLines)
        {
            line.gameObject.SetActive(false);
            linesPool.Add(line);
        }

        activeLines.Clear();
    }

    private void ResetState()
    {
        startPosition = endPosition;
        endPosition = Vector3.zero;
    }

    private void PuzzleWonCheck()
    {
        //Debug.Log("Distance between addAllPos and hasWonValue: " + Vector3.Distance(addAllPos, hasWonValue));

        if (Vector3.Distance(addAllPos, hasWonValue) < epsilon)
        {
            Debug.Log("WON THE PUZZLE!!!");
        }
    }
}
