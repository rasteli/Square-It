using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
    public SpriteRenderer sr;
    public Transform firePoint;
    public Rigidbody2D parentRB;
    public GameObject bulletPrefab;
    public ParticleSystem muzzleFlash;

    public int gunNumber = 0;
    public float shootingDelay = 1f;
    public float fireRateMultiplier = 1f;

    public Gun gun;
    private bool readyToShoot;

    private void Awake()
    {
        gun = GunContainer.guns[gunNumber];
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(shootingDelay);
        fireRateMultiplier *= Random.Range(5f, 7f);
        readyToShoot = true;
    }

    private void Update()
    {
        StartCoroutine(Shoot(gun));
    }

    private IEnumerator Shoot(Gun _gun)
    {
        Vector3 hitDir = (PlayerMovement.Instance.transform.position - firePoint.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, hitDir); 

        if (readyToShoot && (hit.collider && hit.collider.name == "Player"))
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

                bullet.tag = "BOE";
                tr.endColor = sr.color;
                tr.startColor = sr.color;
                bsr.color = sr.color;

                brb.AddForce(direction * frameCorrectedSpeed * 1000f);
            }

            AudioManager.Instance.Play("Shoot");
            
            yield return new WaitForSeconds(_gun.GetFireRate() * fireRateMultiplier);

            readyToShoot = true;
        }
    }
}
