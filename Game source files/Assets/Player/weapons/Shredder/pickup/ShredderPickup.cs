using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShredderPickup : MonoBehaviour
{
    public GameObject Shredder;
    public Transform WeaponsHolder;

    public WeaponsNotiController WeaponPickupNoti;
    public Text WeaponsNoti;

    public SwitchWeapons switchWeapons;

    public WeaponsOrder weaponsOrder;


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
        if (other.CompareTag("Player") && Shredder.transform.parent != WeaponsHolder)
        {

            WeaponsNoti.gameObject.SetActive(true);
            WeaponsNoti.text = "You picked up the Shredder!";

            Shredder.transform.SetParent(WeaponsHolder);


            weaponsOrder.Reoder();

            int currentPlace = Shredder.transform.GetSiblingIndex();

            switchWeapons.selectedWeapon = currentPlace;
            switchWeapons.SelectWeapon();


            Destroy(gameObject);
        }
        if (other.CompareTag("Player") && Shredder.transform.parent == WeaponsHolder)
        {
            Destroy(gameObject);
            //ammo pickup
        }

        else
        {
            return;
        }
    }
}
