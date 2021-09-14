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
        private IntentHandler intentHandler = null;

        [SerializeField]
        private List<Mineral> subjects = null;

        [SerializeField]
        private Text progressText = null;

        [SerializeField]
        private Text resultText = null;

        private int totalSubjects = 0;

        private float elapsedTime = 0;

        private bool gameFinished = false;

        private void Start()
        {
            this.intentHandler.RequestAccelerate = (Vector3 direction, Vector3 torque) =>
            {
                this.player.AccelerateDirection(direction);
                this.player.AccelerateTorque(torque);
            };

            for (int i = 0; i < this.subjects.Count; i++)
            {
                this.subjects[i].OnRetrieved = this.Progress;
            }
            this.totalSubjects = this.subjects.Count;
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
            this.progressText.text = "" + (this.totalSubjects - this.subjects.Count) + "/" + this.totalSubjects;
            if (this.subjects.Count == 0 && !this.gameFinished)
            {
                this.gameFinished = true;
                StartCoroutine(this.ShowResult());
            }
        }

        public void Progress(Mineral target)
        {
            this.subjects.Remove(target);
            GameObject.Destroy(target.gameObject);

            this.player.Retrieve();

            this.UpdateProgress();
        }

        private IEnumerator<WaitForSeconds> ShowResult()
        {
            yield return new WaitForSeconds(1);

            string timeStr = ("" + this.elapsedTime);
            int position = timeStr.IndexOf(".");
            if (position > 0)
            {
                timeStr = timeStr.Substring(0, position + 2);
            }
                
            this.resultText.text = "Score " + timeStr + " Sec\n";

            yield return new WaitForSeconds(3);

            this.resultText.text = "Score " + timeStr + " Sec\nThank you for playing !";
        }
    }
}