using UnityEngine;
using System.Collections.Generic;

public class GunContainer : MonoBehaviour
{
    public static List<Gun> guns;

    // Gun(spread, fireRate, damage, bulletSpeed, magazineCap, recoil, description) */
    
    private static readonly Gun pistol = new Gun(.08f, .25f, 20, 70f, 1, 2.2f, "Pistol");
    private static readonly Gun shotgun = new Gun(.21f, .6f, 50, 90f, 5, 4f, "Shotgun");
    private static readonly Gun assaultRifle = new Gun(.02f, .08f, 12, 170f, 1, 1.6f, "AR");
    private static readonly Gun dualPistol = new Gun(.16f, .45f, 20, 80f, 2, 2.2f, "Dual Pistol");

    private void Awake()
    {
        guns = new List<Gun>();

        guns.Add(pistol);
        guns.Add(shotgun);
        guns.Add(assaultRifle);
        guns.Add(dualPistol);
    }
}
