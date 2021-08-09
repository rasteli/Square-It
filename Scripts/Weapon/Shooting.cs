using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public Color color;
    public SpriteRenderer sr;
    public Transform firePoint;
    public Rigidbody2D parentRB;
    public GameObject bulletPrefab;
    public ParticleSystem muzzleFlash;

    public static Gun gun;
    public static Shooting Instance;

    private bool readyToShoot;

    private void Awake()
    {
        Instance = this;
        readyToShoot = true;
    }

    private void Start()
    {
        Inventory.Instance.SelectGun((int)SavePrefs.LoadState("CurrentGun"));
        Inventory.Instance.UnlockGun((int)SavePrefs.LoadState("CurrentGun"));
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(Shoot(gun));
        }
    }

    private IEnumerator Shoot(Gun _gun)
    {
        if (readyToShoot)
        {
            readyToShoot = false;

            muzzleFlash.Play();

            for (int i = 0; i < _gun.GetMagazineCap(); i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D brb = bullet.GetComponent<Rigidbody2D>();
                TrailRenderer tr = bullet.GetComponent<TrailRenderer>();
                SpriteRenderer bsr = bullet.GetComponent<SpriteRenderer>();

                float randomSpread = Random.Range(-_gun.GetSpread(), _gun.GetSpread());
                float totalSpeed = _gun.GetBulletSpeed() + parentRB.velocity.magnitude;
                float frameCorrectedSpeed = totalSpeed * Time.fixedDeltaTime;

                Vector2 per = Vector2.Perpendicular(firePoint.up) * randomSpread;
                Vector2 direction = (per + (Vector2)firePoint.up).normalized;

                bullet.tag = "BOP";
                bsr.color = color;
                tr.startColor = color;
                tr.endColor = color;

                brb.AddForce(direction * frameCorrectedSpeed * 1000f);
                PlayerMovement.Instance.rb.velocity = -direction * frameCorrectedSpeed * _gun.GetRecoil();
            }

            AudioManager.Instance.Play("Shoot");

            yield return new WaitForSeconds(_gun.GetFireRate());

            readyToShoot = true;
        }
    }
}
