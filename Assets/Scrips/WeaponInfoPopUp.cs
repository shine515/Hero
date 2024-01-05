using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoPopUp : MonoBehaviour
{

    public WeaponInfo weapon;
    public ShopManager shopManager;

    //���� �˾��� ��� ������
    [SerializeField]
    private TMP_Text Name;
    [SerializeField]
    private TMP_Text Damage;
    [SerializeField]
    private TMP_Text Price; //���Ⱑ��
    [SerializeField]
    private Image Icon;

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


    bool ChangeWeap =false;
    IEnumerator ChangeWeapon(Collider other)
    {
        bool hasExecuted = false;  //GetKeyDown�� �۵� �ȵǼ� ���� ��
        while (ChangeWeap)
        {
            if (Input.GetKey(KeyCode.E)&&!hasExecuted)
            {
                Status PStatus = other.GetComponent<Status>();
                PStatus.nowWeap = this.gameObject;
                foreach (GameObject weapon in PStatus.Weapons)
                {
                    if (weapon.name == PStatus.nowWeap.name)
                        weapon.SetActive(true);
                    else
                        weapon.SetActive(false);

                }
                hasExecuted = true;
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

    void PopupSet()
    {
        shopManager.ShopPopup();
        string WeapName = "<"+weapon.Name+">";
        string WeapDamage = "���ݷ�: "+weapon.Damage.ToString();
        //string WeapType = weapon.type;
        Sprite WeapImg = weapon.WeapImg;

        Name.text = WeapName;
        Damage.text = WeapDamage;
        Damage.text = WeapDamage;
        Icon.sprite = WeapImg;
    }
}
