using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceFill : MonoBehaviour {
	[Tooltip("exp image")]
	public Image expFill;

	// Use this for initialization
	void Start () {
		if (this.expFill == null)
			return;
		expFill.fillAmount = Player.instance.playerStats.currentExperience/ Player.instance.playerStats.targetExperience;
	}

	// Update is called once per frame
	void Update () {
		if (this.expFill == null)
			return;
		expFill.fillAmount = (float)Player.instance.playerStats.currentExperience / (float)Player.instance.playerStats.targetExperience;

	}
}
