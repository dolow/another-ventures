using UnityEngine;

public class Title : MonoBehaviour
{
    private float stay = 3.0f;
    private float zProgressPerFrame = -0.01f;

    private float deltaTimes = 0.0f;

    private void Update()
    {
        this.deltaTimes += Time.deltaTime;

        if (this.deltaTimes < this.stay)
        {
            return;
        }

        Vector3 pos = this.transform.localPosition;
        pos.z += zProgressPerFrame;
        this.transform.localPosition = pos;

        if (this.transform.localPosition.z < 0.0f)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
