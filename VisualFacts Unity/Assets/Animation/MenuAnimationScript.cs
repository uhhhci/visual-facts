using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimationScript : MonoBehaviour {

	public void enableMenuAnimator(Animator anim)
	{
		anim.SetBool ("Menu_displayed", true);
	}

	public void enableButtonAnimator(Animator anim)
	{
		anim.SetBool ("Menu_button_out", true);
	}

	public void disableMenuAnimator(Animator anim)
	{
		anim.SetBool ("Menu_displayed", false);
	}

	public void disableButtonAnimator(Animator anim)
	{
		anim.SetBool ("Menu_button_out", false);
	}

}
