using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public GameObject WeaPopup;
    Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WEAPONMODEL")
        {
            weapon = other.GetComponent<Weapon>();
            ShopPopup();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WEAPONMODEL")
        {
            weapon = other.GetComponent<Weapon>();
            WeaPopup.SetActive(false);
        }
    }

    public void ShopPopup()
    {
        if (WeaPopup.activeSelf)
            WeaPopup.SetActive(false);
        else 
            WeaPopup.SetActive(true);
    }

    /*var characterGo = Instantiate<GameObject>(character, this.contents.transform);
        this.btn = characterGo.transform.Find("btnCh").gameObject.GetComponent<Button>();
 

this.btn = characterGo.transform.Find("btnCh").gameObject.GetComponent<Button>();
*/

}
