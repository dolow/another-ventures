using System.Collections.Generic;
using Game.Role;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private Player player = null;

        [SerializeField]
        private UI ui = null;

        [SerializeField]
        private IntentHandler intentHandler = null;

        [SerializeField]
        private List<Mineral> subjects = null;

        private int totalSubjects = 0;

        private float elapsedTime = 0;

        private bool gameFinished = false;

        private void Start()
        {
            this.intentHandler.RequestAccelerate = (Vector3 direction, Vector3 torque) =>
            {
                this.player.AccelerateDirection(direction);
                this.player.AccelerateTorque(torque);
                this.ui.SetVelocity(this.player.AccelerationRate, this.player.AccelerationRateLimit);
            };

            for (int i = 0; i < this.subjects.Count; i++)
            {
                this.subjects[i].OnRetrieved = this.Progress;
            }
            this.totalSubjects = this.subjects.Count;

            this.UpdateProgress();
            this.ui.SetVelocity(this.player.AccelerationRate, this.player.AccelerationRateLimit);
        }

        private void Update()
        {
            if (!this.gameFinished)
            {
                this.elapsedTime += Time.deltaTime;
            }
        }

        public void UpdateProgress()
        {
            this.ui.SetProgress(this.totalSubjects, (this.totalSubjects - this.subjects.Count));

            if (this.subjects.Count == 0 && !this.gameFinished)
            {
                this.gameFinished = true;
                StartCoroutine(this.ui.ShowResult(this.elapsedTime));
            }
        }

        public void Progress(Mineral target)
        {
            this.subjects.Remove(target);
            GameObject.Destroy(target.gameObject);

            this.player.Retrieve();

            this.UpdateProgress();
        }
    }
}