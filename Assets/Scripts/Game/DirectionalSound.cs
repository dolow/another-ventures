using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class DirectionalSound : MonoBehaviour
    {
        [SerializeField]
        private AudioSource left = null;
        [SerializeField]
        private AudioSource right = null;
        [SerializeField]
        private AudioSource center = null;
        [SerializeField]
        private AudioSource bottom = null;

        [SerializeField]
        private float overwrapDurationThreashold = 0.05f;

        private float sincePlayedLeft = 0.0f;
        private float sincePlayedRight = 0.0f;
        private float sincePlayedCenter = 0.0f;

        public void Play(Vector3 direction)
        {
            if (direction.x > 0.0f && this.sincePlayedLeft >= this.overwrapDurationThreashold)
            {
                this.left.PlayOneShot(this.left.clip);
                this.sincePlayedLeft = 0.0f;
            }
            if (direction.x < 0.0f && this.sincePlayedRight >= this.overwrapDurationThreashold)
            {
                this.right.PlayOneShot(this.right.clip);
                this.sincePlayedRight = 0.0f;
            }
            if (direction.x == 0.0f && this.sincePlayedCenter >= this.overwrapDurationThreashold)
            {
                this.center.PlayOneShot(this.center.clip);
                this.sincePlayedCenter = 0.0f;
            }
        }

        public void PlayBottom()
        {
            this.bottom.PlayOneShot(this.bottom.clip);
        }

        private void Start()
        {
            this.sincePlayedLeft = this.overwrapDurationThreashold;
            this.sincePlayedRight = this.overwrapDurationThreashold;
            this.sincePlayedCenter = this.overwrapDurationThreashold;
        }

        private void Update()
        {
            this.sincePlayedLeft += Time.deltaTime;
            this.sincePlayedRight += Time.deltaTime;
            this.sincePlayedCenter += Time.deltaTime;
        }
    }
}