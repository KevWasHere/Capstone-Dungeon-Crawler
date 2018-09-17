/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # ArcherAttack.cs
  # Instantiate enemy archer attack
*-----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * archerAttackRate - Rate of archer attack
 * archerAttack - Reference to archer attack object
 * archerAttackSpawn - Transform of archer attack
 * nextArcherAttack - Time of next archer attack call
 * anim - Reference to CharacterAnimator script
 * delay - Attack delay
 * arrowShot - Archer shot SFX
 * arrowPull - Archer pull SFX
*/

/*
 * Creator: Kevin Ho, Shane Weerasuriya
 */

public class ArcherAttack : MonoBehaviour {
	
	//Archer attack
	public float archerAttackRate;
	public GameObject archerAttack;
	public Transform archerAttackSpawn;
	private float nextArcherAttack;
    CharacterAnimator anim;
    public float delay = 1.0f;
	float archerAttackDestroy = 5f;
	
	//Audio
	public AudioClip arrowShot;
	public AudioClip arrowPull;

    private void Start()
    {
        anim = GetComponent<CharacterAnimator>();
    }

    /*
	Fires archer attack
	Creator: Kevin Ho, Shane Weerasuriya
	*/
    public void fireArcherAttack() {
		if (Time.time > nextArcherAttack) {
			nextArcherAttack = Time.time + archerAttackRate;
            //Spawn default attack hitbox
            anim.OnAttack();
			if (arrowPull != null)
				AudioSource.PlayClipAtPoint(arrowPull, transform.position, 0.8f);
            StartCoroutine(DelayAttack(delay));
			//GameObject hitBox = Instantiate(archerAttack, archerAttackSpawn.position, archerAttackSpawn.rotation);
			//Destroy(hitBox, archerAttackRate);		
		}
	}

	/*
	Attack delay for archer
	Creator: Kevin Ho, Shane Weerasuriya
	*/
    IEnumerator DelayAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject hitBox = Instantiate(archerAttack, archerAttackSpawn.position, archerAttackSpawn.rotation);
		if (arrowShot != null)
			AudioSource.PlayClipAtPoint(arrowShot, transform.position, 0.8f);
        Destroy(hitBox, archerAttackDestroy);
        //enemyStats.TakeDamage (myStats.damage.GetValue ());
    }
}
