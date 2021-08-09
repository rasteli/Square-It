using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public Image source;
    public Sprite pistol;
    public Sprite shotgun;
    public Sprite dualPistol;
    public Sprite assaultRifle;
    public SpriteRenderer gunRenderer;

    public Transform firePoint;
    public Transform muzzleFlash;

    public Gun[] unlockedGun;

    public static Inventory Instance;

    private void Awake()
    {
        Instance = this;
        unlockedGun = new Gun[1];
    }

    public void UnlockGun(int index)
    {
        unlockedGun[0] = GunContainer.guns[index];
    }

    public void SelectGun(int index)
    {
        Sprite[] sprites = new Sprite[4]{pistol, shotgun, assaultRifle, dualPistol};
        Vector2[,] positions = new Vector2[4, 2]
        {
            {new Vector2(.1437f, .0404f), new Vector2(.9f, .068f)},
            {new Vector2(.1437f, .0404f), new Vector2(1.054f, .068f)},
            {new Vector2(.136f, .0127f), new Vector2(1.054f, -.04f)},
            {new Vector2(.1437f, .0404f), new Vector2(.9f, .068f)}
        };

        firePoint.localPosition = positions[index, 0];
        muzzleFlash.localPosition = positions[index, 1];

        Shooting.gun = GunContainer.guns[index];
        gunRenderer.sprite = sprites[index];
        source.sprite = sprites[index];
    }
}
