using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public Camera cam;
    public EnemyShooting es;

    private Vector2 position;

    private void Update()
    {
        if (cam)
            position = cam.ScreenToWorldPoint(Input.mousePosition);
        else
            position = PlayerMovement.Instance.PredictPosition(
                Vector2.Distance(PlayerMovement.Instance.transform.position, transform.position) / es.gun.GetBulletSpeed()
            );
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = position - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
