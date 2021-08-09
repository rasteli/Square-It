using UnityEngine;
using Pathfinding;

public class RoomSpawner : MonoBehaviour
{
    public GameObject[] rooms;

    private void Start()
    {
        SpawnRoom();
    }

    private void SpawnRoom()
    {
        int randomIndex = Random.Range(0, rooms.Length);

        Instantiate(rooms[randomIndex], transform.position, Quaternion.identity);

        AstarPath.active.Scan();
    }
}
