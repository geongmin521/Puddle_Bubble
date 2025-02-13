using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageTest : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[3];
    public Image imgae;
    public bool isMain;
    // Start is called before the first frame update

    void MainWeapon()
    {
        if (Player.Instance.mainWeaponType == Player.WeaponType.Bomb)
        {
            imgae.sprite = sprites[0];
        }
        if (Player.Instance.mainWeaponType == Player.WeaponType.Water)
        {
            imgae.sprite = sprites[1];
        }
        if (Player.Instance.mainWeaponType == Player.WeaponType.Getling)
        {
            imgae.sprite = sprites[2];
        }
    }

    void subWeapon()
    {
        if (Player.Instance.subWeaponType == Player.WeaponType.Bomb)
        {
            imgae.sprite = sprites[0];
        }
        if (Player.Instance.subWeaponType == Player.WeaponType.Water)
        {
            imgae.sprite = sprites[1];
        }
        if (Player.Instance.subWeaponType == Player.WeaponType.Getling)
        {
            imgae.sprite = sprites[2];
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isMain == true)
        {
            MainWeapon();
        }
        else { subWeapon(); }
      
    }
}
