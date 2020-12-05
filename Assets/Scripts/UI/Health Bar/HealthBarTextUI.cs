﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthBarTextUI : HealthBar {
	
	//Enumerators
	
	//Structs
	
	//Set Variables
	
		//Static
        
        
		//No Static
		[Header("UI Text")]
		[SerializeField] private TextMeshProUGUI m_healthText = null;
		[SerializeField] private string m_hpText = "HP:";

        
    //Functions
	
		//MonoBehaviour Functions
        
        
		//Public Functions
        public override void SetValue(float health, float maxHealth) {

			base.SetValue(health, maxHealth);
			m_healthText.text = m_hpText + " " + health.ToString() + " / " + maxHealth.ToString();
			}
        
        
		//Private Functions
        
        
	//Coroutines
	
	}