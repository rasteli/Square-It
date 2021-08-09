using UnityEngine;

public class Gun
{
    private string desc;
    private float damage;
    private float spread;
    private float recoil;
    private float fireRate;
    private float bulletSpeed;
    private int magazineCap;

    public Gun(float _spread, float _fireRate, float _damage, float _bulletSpeed, int _mag, float _recoil, string _desc)
    {
        this.desc = _desc;
        this.damage = _damage;
        this.spread = _spread;
        this.recoil = _recoil;
        this.magazineCap = _mag;
        this.fireRate = _fireRate;
        this.bulletSpeed = _bulletSpeed;
    }

    public float GetDamage()
    {
        return damage;
    }

    public string GetDescription()
    {
        return desc;
    }

    public float GetSpread()
    {
        return spread;
    }

    public float GetRecoil()
    {
        return recoil;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    public int GetMagazineCap()
    {
        return magazineCap;
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
}
