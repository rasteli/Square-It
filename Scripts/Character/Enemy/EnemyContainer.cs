using UnityEngine;
using System.Collections.Generic;

public class EnemyContainer : MonoBehaviour
{
    public static List<GameObject> enemies;

    private void Awake()
    {
        enemies = new List<GameObject>();
    }
}
