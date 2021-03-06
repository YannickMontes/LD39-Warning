﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminator : MonoBehaviour
{
    public Stat hp;
	public float runningDrainPerSec = 10.0f;
    public Stat energy;
    public float radarRadius;
	static Terminator TerminatorCurrent;
	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;
    public bool isAlive;
    public bool isRecharging;
	bool isCoroutineActive = false;

	void Start ()
    {
		TerminatorCurrent = this;
        this.isRecharging = false;
        this.energy.Bar = GameObject.Find("EnergyBar").GetComponent<UIBarScript>();
        this.hp.Bar = GameObject.Find("HealthBar").GetComponent<UIBarScript>();
    }

	// Update is called once per frame
	void Update ()
    {
		if (!controller.m_IsWalking && !isCoroutineActive && energy.CurrentValue>0) {
			StartCoroutine(StartEnergyDrain());
		}

		if (energy.CurrentValue <= 0) {
			controller.canRun = false;
		} else
			controller.canRun = true;

        controller.isRecharging = isRecharging;
	}

	IEnumerator StartEnergyDrain() {
		isCoroutineActive = true;

		while (!controller.m_IsWalking && energy.CurrentValue > 0) {
			controller.canDash = false;
			DecreaseEnergy (runningDrainPerSec);
			yield return new WaitForSeconds(controller.DashTime);
			controller.canDash = true;
		}
		isCoroutineActive = false;
	}



	public static Terminator GetTerminator(){
		return TerminatorCurrent;
	}
	public void DecreaseHP(float amount){
		this.hp.CurrentValue -= amount;
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


}
