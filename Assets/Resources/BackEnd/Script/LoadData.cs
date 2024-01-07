using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
public class LoadData : MonoBehaviour
{
    string WeaponFileLink = "BackEnd/DataFile/Csv/Weapon";
    string MosteFileLinkr = "BackEnd/DataFile/Csv/Moster";

    public GameObject[] WeaponPrefabList;
    public GameObject[] MonsterPrefabList;

    List<Dictionary<string, object>> WeaponList = new List<Dictionary<string, object>>();
    List<Dictionary<string, object>> MosterList = new List<Dictionary<string, object>>();

    Status PlayerStatus;

    private void Awake()
    {
        Debug.Log("000000000000000000000000000000000000000000000000000000000000000000");
        GetWeaponInfo();
        GetMosterInfo();
    }
    private void Start()
    {
        PlayerStatus = GameObject.FindWithTag("Player").GetComponent<Status>();
    }

    public void GetWeaponInfo()
    {
        WeaponList = CSVReader.Read(WeaponFileLink);
        Weapon weapon;
        for (int i = 0; i < WeaponList.Count; i++)
        {
            weapon = WeaponPrefabList[i].GetComponent<Weapon>();
            if (WeaponList[i]["WeaponCode"].ToString().Equals(this.name))
            {
                weapon.Name = WeaponList[i]["Name"].ToString();
                weapon.Damage = Int32.Parse(WeaponList[i]["Damage"].ToString());
                weapon.type = WeaponList[i]["Type"].ToString();
                weapon.AttSpeed = float.Parse(WeaponList[i]["AttackSpeed"].ToString());


                weapon.Price = Int32.Parse(WeaponList[i]["Price"].ToString());
                weapon.Upgrade = Int32.Parse(WeaponList[i]["Upgrade"].ToString());
            }
        }
    }

    public void GetMosterInfo()
    {
        MosterList = CSVReader.Read(MosteFileLinkr);

        MonsterControl monsterControl;
        for (int i = 0; i < MosterList.Count; i++)
        {
            monsterControl = MonsterPrefabList[i].GetComponent<MonsterControl>();
            if (WeaponList[i]["WeaponCode"].ToString().Equals(this.name))
            {
                monsterControl.mos.MaxHP = Int32.Parse(MosterList[i]["MaxHp"].ToString());
                monsterControl.mos.Damage = Int32.Parse(MosterList[i]["Damage"].ToString());
            }
        }
    }
}
