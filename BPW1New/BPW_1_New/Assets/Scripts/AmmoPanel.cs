using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPanel : MonoBehaviour
{
    public GameObject[] bulletIcons;
    // Start is called before the first frame update
    
    public void RemoveBullet(int bullet)
    {
        if(bullet >= bulletIcons.Length) { Debug.LogError("That bullet doesn't exist love"); return; }
        bulletIcons[bullet].SetActive(false);
    }

    public void ReloadBullets()
    {
        foreach(GameObject b in bulletIcons) { b.SetActive(true); }
    }
}
