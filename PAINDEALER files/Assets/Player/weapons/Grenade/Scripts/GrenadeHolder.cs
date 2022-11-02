using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using UnityEngine.UI;
using JetBrains.Annotations;

public class GrenadeHolder : MonoBehaviour
{
    public GameObject weaponsHolder;
    public GameObject grenadeHolder;

    public AmmoManager ammoManager;
    public Text grenadeCountUI;

    public GameObject Grenade;
    public GameObject spawnLocation;
    public Animator animator;
    int animLayer = 0;


    public float throwForce;
    public float throwUpwardForce;

    private void Start()
    {
        ammoManager = (GameObject.Find("Weapons Holder")).gameObject.GetComponent<AmmoManager>();
        grenadeCountUI = GameObject.Find("grenadeCount").GetComponent<Text>();
    }


    void Update()
    {
        grenadeCountUI.text = ammoManager.grenadeCount.ToString("0#");
        if (Input.GetMouseButtonDown(2) )
        {
            if (ammoManager.grenadeCount > 0 && !isPlaying(animator, "throwGrenade"))
            {
                StartCoroutine(throwingGrenade());
                StartCoroutine(switchingWeapons());
                ammoManager.grenadeCount -= 1;
            }
            else if (ammoManager.grenadeCount == 0)
            {
                return;
            }
        }
       
    }

    void ThrowGrenade()
    {

        GameObject grenade = Instantiate(Grenade, spawnLocation.transform.position, spawnLocation.transform.rotation);
        Rigidbody projectileRb = grenade.GetComponent<Rigidbody>();
        Vector3 force = spawnLocation.transform.forward * throwForce + transform.up * throwUpwardForce;
        projectileRb.AddForce(force, ForceMode.Impulse);
    }

    IEnumerator switchingWeapons()
    {
        weaponsHolder.SetActive(false);
        grenadeHolder.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        grenadeHolder.SetActive(false);
        weaponsHolder.SetActive(true);
    }

    IEnumerator throwingGrenade()
    {
        yield return new WaitForSeconds(0.8f);
        ThrowGrenade();
    }

    //check if animtion is playing
    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }
}
