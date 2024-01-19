using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class AnimatorManager : MonoBehaviour
    {
        public Animator animator;
        public virtual void Start()
        {
            if (!animator)
                animator = GetComponent<Animator>();
        }

        public void PlayAnimation(string animation, bool IsInteracting, float transitionDuration)
        {
            animator.applyRootMotion = IsInteracting;
            animator.SetBool("isInteracting", IsInteracting);
            animator.CrossFade(animation, transitionDuration);
        }
        public void PlayAnimation(string animation, bool IsInteracting)
        {
            PlayAnimation(animation, IsInteracting, 0.2f);
        }
    }
}
