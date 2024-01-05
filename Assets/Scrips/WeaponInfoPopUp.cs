using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoPopUp : MonoBehaviour
{

    public WeaponInfo weapon;
    public ShopManager shopManager;

    string WeapName;
    string WeapDamage;
    string UpgradeLevel;

    // Start is called before the first frame update
    void Start()
    {
        shopManager = GameObject.Find("ShopManager").GetComponent<ShopManager>();
        weapon = GetComponent<WeaponInfo>();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PopupSet();
            ChangeWeap = true;
            StartCoroutine(ChangeWeapon(other));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("player");

            
        }
    }

    bool ChangeWeap =false;
    IEnumerator ChangeWeapon(Collider other)
    {
        while (ChangeWeap)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("e´©¸§");
                Status PStatus = other.GetComponent<Status>();
                PStatus.nowWeap = this.gameObject;
                foreach (GameObject weapon in PStatus.Weapons)
                {
                    if (weapon == PStatus.nowWeap)
                        weapon.SetActive(true);
                    else
                        weapon.SetActive(false);

                }
            }
            yield return new WaitForEndOfFrame();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            shopManager.WeaPopup.SetActive(false);
            ChangeWeap = false;
        }
    }

    [SerializeField]
    private TMP_Text Name;
    [SerializeField]
    private TMP_Text Damage;
    [SerializeField]
    private Image Icon;
    void PopupSet()
    {
        shopManager.ShopPopup();
        string WeapName = weapon.Name;
        float WeapDamage = weapon.Damage;
        //string WeapType = weapon.type;
        Sprite WeapImg = weapon.WeapImg;

        Name.text = WeapName;
        Damage.text = WeapDamage.ToString();
        Icon.sprite = WeapImg;
    }
}
