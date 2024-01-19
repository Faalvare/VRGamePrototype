using UnityEngine;
using System.Collections;

namespace Main.Others
{
    [RequireComponent(typeof(Animator))]
    public class LookAt : MonoBehaviour
    {
        public Transform head = null;
        public Vector3 lookAtTargetPosition;
        public float lookAtCoolTime = 0.2f;
        public float lookAtHeatTime = 0.2f;
        public bool looking = true;
        public bool lockY = true;
        public float bodyWeight = 0.2f;
        public float headWeight = 0.5f;
        public float eyesWeight = 0.7f;
        public float clampWeight = 0.5f;

        private Vector3 lookAtPosition;
        private Animator animator;
        private float lookAtWeight = 0.0f;

        void Start()
        {
            animator = GetComponent<Animator>();
            if (!head)
            {
                Debug.LogError("No head transform - LookAt disabled");
                enabled = false;
                return;
            }
            lookAtTargetPosition = head.position + transform.forward;
            lookAtPosition = lookAtTargetPosition;
        }

        void OnAnimatorIK()
        {
            if (lockY)
                lookAtTargetPosition.y = head.position.y;
            float lookAtTargetWeight = looking ? 1.0f : 0.0f;

            Vector3 curDir = lookAtPosition - head.position;
            Vector3 futDir = lookAtTargetPosition - head.position;

            curDir = Vector3.RotateTowards(curDir, futDir, 6.28f * Time.deltaTime, float.PositiveInfinity);
            lookAtPosition = head.position + curDir;

            float blendTime = lookAtTargetWeight > lookAtWeight ? lookAtHeatTime : lookAtCoolTime;
            lookAtWeight = Mathf.MoveTowards(lookAtWeight, lookAtTargetWeight, Time.deltaTime / blendTime);

            if (animator != null)
            {
                animator.SetLookAtWeight(lookAtWeight, bodyWeight, headWeight, eyesWeight, clampWeight);
                animator.SetLookAtPosition(lookAtPosition);
            }
        }
    }
}