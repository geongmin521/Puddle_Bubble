using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [Header("UI Asset")]
    [SerializeField] private Sprite miusHpSprite;

    [Header("UI Elements")]
    [SerializeField] private Image[] hpImageArray = new Image[5];
    [SerializeField] private Image mainAmmoImage;
    [SerializeField] private Image subAmmoImage;
    [SerializeField] private Text mainAmmoText;
    [SerializeField] private Text subAmmoText;
    [SerializeField] private Text currentWeaponText;

    //[Header("UI Key Bindings")]
    //[SerializeField] private KeyCode testUIKey = KeyCode.Escape;

    public static PlayerUIManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // ���� ���� UI
    public void UpdateCurrentWeaponUI(string text)
    {
        currentWeaponText.text = "���繫�� : [" + text + "]";
    }

    // HP UI
    public void HpUiIconDown()
    {
        hpImageArray[Player.Instance.hp].sprite = miusHpSprite;
    }

    // ����źâ UI
    public void UpdateMainAmmoUI(int mainAmmo, int maxAmmo)
    {
        mainAmmoText.text = mainAmmo.ToString();
        mainAmmoImage.fillAmount = (float)mainAmmo / (float)maxAmmo;
        Debug.Log(mainAmmo / maxAmmo);
    }

    // ����źâ UI
    public void UpdateSubAmmoUI(int subAmmo, int maxAmmo)
    {
        subAmmoText.text = subAmmo.ToString();
        subAmmoImage.fillAmount = (float)subAmmo / (float)maxAmmo;
    }
}
