using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMoveTest : MonoBehaviour
{
 public float speed = 2.0f;
    public bool isMoving = false; 
    public Grid grid;
    public Transform[] seats;
    private Transform currentSeat;
    private int currentSeatIndex = 0;
    void Start()
    {
        if (seats.Length > 0)
        {
            StartCoroutine(MoveToSeatRoutine());
        }
    }
    IEnumerator MoveToSeatRoutine()
    {
        while (true)
        {
            currentSeat = seats[currentSeatIndex];
            if (currentSeat == null) yield break;
            Vector3Int currentCell = grid.WorldToCell(transform.position); Vector3Int targetCell = grid.WorldToCell(currentSeat.position);
            while (currentCell.x != targetCell.x)
            {
                currentCell.x += (int)Mathf.Sign(targetCell.x - currentCell.x);
                yield return StartCoroutine(MoveToCell(currentCell));
            }
            while (currentCell.y != targetCell.y)
            {
                currentCell.y += (int)Mathf.Sign(targetCell.y - currentCell.y);
                yield return StartCoroutine(MoveToCell(currentCell));
            }
            Debug.Log("È‚É‚Â‚«‚Ü‚µ‚½");
            float stayDuration = GetStayDurationForSeat(currentSeat);
            yield return new WaitForSeconds(stayDuration);
            Debug.Log("È‚ð—£‚ê‚Ü‚·");
            currentSeatIndex = (currentSeatIndex + 1) % seats.Length;
        }
    }
    IEnumerator MoveToCell(Vector3Int targetCell)
    {
        isMoving = true;
        Vector3 targetWorldPos = grid.GetCellCenterWorld(targetCell);
        while (Vector3.Distance(transform.position, targetWorldPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWorldPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetWorldPos;
        isMoving = false;
    }
    float GetStayDurationForSeat(Transform seat)
    {
        TargetProperties seatProperties = seat.GetComponent<TargetProperties>();
        if (seatProperties != null)
        {
            return seatProperties.stayDuration;
        }
        return 3.0f;
    }
}
