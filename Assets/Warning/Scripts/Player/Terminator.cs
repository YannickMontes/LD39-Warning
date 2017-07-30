﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminator : MonoBehaviour
{
    public Stat hp;
    public Stat energy;
    public float radarRadius;
	static Terminator TerminatorCurrent;

    public bool isAlive;

	void Start ()
    {
		TerminatorCurrent = this;
	}

	// Update is called once per frame
	void Update ()
    {
      
	}

	public static Terminator GetTerminator(){
		return TerminatorCurrent;
	}

	public void DecreaseEnergy(float energyCost)
    {
		this.energy.CurrentValue -= energyCost;
    }

    private void CheckForEnnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 20.0f);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Ennemy")
            {
                return;
            }
            i++;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 10, 100, 20), "HP: "+this.hp.CurrentValue);
        GUI.Label(new Rect(20, 30, 200, 20), "ENERGY: " + this.energy.CurrentValue);
    }
}