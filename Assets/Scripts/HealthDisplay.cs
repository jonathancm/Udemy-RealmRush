using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class HealthDisplay : MonoBehaviour
{
	// Cached References
	PlayerBase playerBase;
	Text healthText;

	void Start()
    {
		playerBase = FindObjectOfType<PlayerBase>();
		healthText = GetComponent<Text>();

		UpdateDisplay();
	}

    void Update()
    {
		UpdateDisplay();
    }

	private void UpdateDisplay()
	{
		if(playerBase)
			healthText.text = playerBase.GetHitPoints().ToString();
		else
			healthText.text = "0";
	}
}
