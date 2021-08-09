using TMPro;
using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shields;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI description;

    public GameObject[] upgradesCost;
    private string[] data = new string[3]{"QJ", "MS", "S"};

    public void Update()
    {
        coinText.text = SavePrefs.LoadState("Coins").ToString();

        for (int i = 0; i < upgradesCost.Length; i++)
        {
            upgradesCost[i].SetActive(SavePrefs.LoadState(data[i]) == 0);
        } 
    }

    public void BuyGun(int index)
    {
        int[] prices = new int[4]{5, 150, 50, 20};
        string[] descs = new string[4]{"Pistol", "Shotgun", "AR", "Dual Pistol"};

        int price = prices[index];
        string desc = descs[index];

        bool gunExists = Array.Exists(Inventory.Instance.unlockedGun, gun => gun.GetDescription() == desc);

        if (!gunExists)
        {
            if (SavePrefs.LoadState("Coins") >= price)
            {
                Inventory.Instance.UnlockGun(index);

                SavePrefs.SaveState("CurrentGun", index);
                SavePrefs.SaveState("Coins", SavePrefs.LoadState("Coins") - price);

            } else {
                SetDescription("You're too poor!");
            }
        } else {
            SetDescription("You're already wielding this gun!");
        }
    }

    public void QuadJump()
    {
        if (SavePrefs.LoadState("QJ") == 0)
        {
            if (SavePrefs.LoadState("Coins") >= 120)
            {
                SavePrefs.SaveState("Jumps", 4);
                SavePrefs.SaveState("Coins", SavePrefs.LoadState("Coins") - 120);

                SavePrefs.SaveState("QJ", 1);

            } else {
                SetDescription("You're too poor!");
            }
        }
    }

    public void MoreSpeed()
    {
        if (SavePrefs.LoadState("MS") == 0)
        {
            if (SavePrefs.LoadState("Coins") >= 150)
            {
                PlayerMovement.Instance.movementVel += .5f * PlayerMovement.Instance.movementVel;
                SavePrefs.SaveState("Speed", PlayerMovement.Instance.movementVel);
                SavePrefs.SaveState("Coins", SavePrefs.LoadState("Coins") - 150);

                SavePrefs.SaveState("MS", 1);

            } else {
                SetDescription("You're too poor!");
            }
        }
    }

    public void Shield()
    {
        if (SavePrefs.LoadState("S") == 0)
        {
            if (SavePrefs.LoadState("Coins") >= 60)
            {
                SavePrefs.SaveState("Shielded", 1);
                SavePrefs.SaveState("Coins", SavePrefs.LoadState("Coins") - 60);

                SavePrefs.SaveState("S", 1);

            } else {
                SetDescription("You're too poor!");
            }
        }
    }

    public void Reset()
    {
        SavePrefs.SaveState("Jumps", 3);
        SavePrefs.SaveState("Speed", 10);
        SavePrefs.SaveState("Shielded", 0);

        SavePrefs.SaveState("QJ", 0);
        SavePrefs.SaveState("MS", 0);
        SavePrefs.SaveState("S", 0);

        SetDescription("Upgrades reset successfully");
    }

    public void SetDescription(string message)
    {
        description.text = message;
    }
}
