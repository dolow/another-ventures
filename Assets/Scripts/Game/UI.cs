using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UI : MonoBehaviour
    {
        static private float velocityBarMaxLength = 50.0f;

        [SerializeField]
        private Text progressText = null;
        [SerializeField]
        private Text resultText = null;

        [SerializeField]
        private Image horizontalVelocity = null;
        [SerializeField]
        private Image verticalVelocity = null;
        [SerializeField]
        private Image forwardVelocity = null;

        private Vector2 horizontalVelocityInitialPosition = Vector2.zero;
        private Vector2 verticalVelocityInitialPosition = Vector2.zero;
        private Vector2 forwardVelocityInitialPosition = Vector2.zero;

        private bool adjusted = false;

        private void Start()
        {
            this.horizontalVelocityInitialPosition = this.horizontalVelocity.rectTransform.anchoredPosition;
            this.verticalVelocityInitialPosition = this.verticalVelocity.rectTransform.anchoredPosition;
            this.forwardVelocityInitialPosition = this.forwardVelocity.rectTransform.anchoredPosition;

            this.adjusted = true;
        }

        public void SetProgress(int total, int current)
        {
            this.progressText.text = "" + current + "/" + total;
        }

        public IEnumerator<WaitForSeconds> ShowResult(float time)
        {
            yield return new WaitForSeconds(1);

            string timeStr = ("" + time);
            int position = timeStr.IndexOf(".");
            if (position > 0)
            {
                timeStr = timeStr.Substring(0, position + 2);
            }

            this.resultText.text = "Score " + timeStr + " Sec\n";

            yield return new WaitForSeconds(3);

            this.resultText.text = "Score " + timeStr + " Sec\nThank you for playing !";
        }

        public void SetVelocity(Vector3 direction, float max)
        {
            if (!this.adjusted)
            {
                return;
            }
            Vector2 horizontalSize = this.horizontalVelocity.rectTransform.sizeDelta;
            Vector2 verticalSize = this.verticalVelocity.rectTransform.sizeDelta;
            Vector2 forwardSize = this.forwardVelocity.rectTransform.sizeDelta;

            float horizontalSizeX = UI.velocityBarMaxLength * (direction.x / max);
            float verticalSizeY = UI.velocityBarMaxLength * (direction.y / max);
            float forwardSizeY = UI.velocityBarMaxLength * (direction.z / max);

            horizontalSize.x = Mathf.Abs(horizontalSizeX);
            verticalSize.y = Mathf.Abs(verticalSizeY);
            forwardSize.y = Mathf.Abs(forwardSizeY);

            this.horizontalVelocity.rectTransform.sizeDelta = horizontalSize;
            this.verticalVelocity.rectTransform.sizeDelta = verticalSize;
            this.forwardVelocity.rectTransform.sizeDelta = forwardSize;

            this.horizontalVelocity.rectTransform.anchoredPosition = this.horizontalVelocityInitialPosition - new Vector2(horizontalSizeX * 0.5f, 0.0f);
            this.verticalVelocity.rectTransform.anchoredPosition = this.verticalVelocityInitialPosition - new Vector2(0.0f, verticalSizeY * 0.5f);
            this.forwardVelocity.rectTransform.anchoredPosition = this.forwardVelocityInitialPosition - new Vector2(0.0f, -forwardSizeY * 0.5f);
        }
    }
}