using UnityEngine;

public class BGMovement : MonoBehaviour
{
    public float speed = 5f;
    public float height = 2f;
    public float smoothTime = 0.2f;

    public Transform teleport;

    private Camera cam;
    private Vector2 screenPos;
    private Vector3 desiredPos;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        cam = GameManager.Instance.cam;
    }

    private void Update()
    {
        desiredPos = new Vector3(
            transform.position.x,
            Mathf.PingPong(Time.time, height),
            transform.position.z
        );

        screenPos = cam.WorldToScreenPoint(transform.position);

        if (screenPos.x > cam.pixelWidth + 50)
        {
            transform.position = teleport.position;
            transform.localScale = new Vector3(Random.Range(.5f, 3f), Random.Range(1f, 15f), 1);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothTime  * Random.Range(1f, 3f));
    }
}
