using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AnimatorOverrideController defaultSkin;
    public AnimatorOverrideController apple;
    public AnimatorOverrideController water;
    public AnimatorOverrideController vulcano;


    public void SetSkin(string skinName, GameObject player)
    {
        Animator playerAnimator = player.GetComponent<Animator>();
        if (playerAnimator!=null)
        {
            if (skinName == "Default")
                playerAnimator.runtimeAnimatorController = defaultSkin;
            else if (skinName =="Apple")
                playerAnimator.runtimeAnimatorController = apple;
            else if (skinName == "Water")
                playerAnimator.runtimeAnimatorController = water;
            else if (skinName == "Vulcano")
                playerAnimator.runtimeAnimatorController = vulcano;

        }
    }
}
