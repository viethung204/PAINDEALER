using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HFG40KPickup : MonoBehaviour
{
    private GameObject HFG40K;

    private Transform WeaponsHolder;
    private AmmoManager ammoManager;

    private Text WeaponsNoti;
    private WeaponsNotiController notification;
    private SwitchWeapons switchWeapons;

    private WeaponsOrder weaponsOrder;



    // Start is called before the first frame update
    void Start()
    {
        ammoManager = (GameObject.Find("Weapons Holder")).gameObject.GetComponent<AmmoManager>();

        WeaponsHolder = (GameObject.Find("Weapons Holder")).gameObject.GetComponent<Transform>();

        switchWeapons = (GameObject.Find("Weapons Holder")).gameObject.GetComponent<SwitchWeapons>();

        weaponsOrder = (GameObject.Find("Weapons Holder")).gameObject.GetComponent<WeaponsOrder>();
        notification = (GameObject.Find("weaponsNoti")).gameObject.GetComponent<WeaponsNotiController>();
        WeaponsNoti = (GameObject.Find("weaponsNoti")).gameObject.GetComponent<Text>();

        HFG40K = FindInActiveObjectByName("HFG40K");


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && HFG40K.transform.parent != WeaponsHolder)
        {
            HFG40K.SetActive(true);
            WeaponsNoti.enabled = true;
            WeaponsNoti.text = "You picked up the legendary HFG40K! Hell yeah!";
            notification.textTimer = 0;
            HFG40K.transform.SetParent(WeaponsHolder);

            weaponsOrder.Reoder();

            int currentPlace = HFG40K.transform.GetSiblingIndex();

            switchWeapons.selectedWeapon = currentPlace;
            switchWeapons.SelectWeapon();

            Destroy(gameObject);
        }
        else if (other.CompareTag("Player") && HFG40K.transform.parent == WeaponsHolder)
        {
            ammoManager.CoreInvAmmo += 2;
            Destroy(gameObject);
        }

        else
        {
            return;
        }
    }
    GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
