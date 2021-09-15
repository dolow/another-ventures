using UnityEngine;

public class Gravity
{
    public static float MaxGravity = 6.0f;

    public static float InertiaAcceleration = 2.0f;
    public static float ReveresedInertiaAcceleration = 4.0f;
    public static float StoppedInertiaDump = 2.0f;
    public static float AdditionalInertiaPerupdate = 0.02f;

    public static bool ShouldAffect(Vector3 center, Vector3 target, float gravity)
    {
        float distance = Vector3.Distance(center, target);
        return (distance <= gravity);
    }
    public static Vector3 GravityVelocity(Vector3 center, Vector3 target, float gravity)
    {
        float distance = Vector3.Distance(center, target);
        float rate = (distance / gravity);

        float a = Gravity.MaxGravity;
        float x = 1.0f + ((a - 1.0f) * rate);
        float y = a / x;

        Vector3 vector = (center - target).normalized;
        return (vector * y);
    }

    public static Vector3 InertiaVelocity(Vector3 directionIntent, Vector3 currentForce, float forceLimit)
    {
        float xDirection = (currentForce.x < 0.0f) ? -1.0f : 1.0f;
        float yDirection = (currentForce.y < 0.0f) ? -1.0f : 1.0f;
        float zDirection = (currentForce.z < 0.0f) ? -1.0f : 1.0f;

        float xReveresed = ((directionIntent.x > 0.0f && currentForce.x < 0.0f) || (directionIntent.x < 0.0f && currentForce.x > 0.0f)) ? Gravity.ReveresedInertiaAcceleration : Gravity.InertiaAcceleration;
        float yReveresed = ((directionIntent.y > 0.0f && currentForce.y < 0.0f) || (directionIntent.y < 0.0f && currentForce.y > 0.0f)) ? Gravity.ReveresedInertiaAcceleration : Gravity.InertiaAcceleration;
        float zReveresed = ((directionIntent.z > 0.0f && currentForce.z < 0.0f) || (directionIntent.z < 0.0f && currentForce.z > 0.0f)) ? Gravity.ReveresedInertiaAcceleration : Gravity.InertiaAcceleration;

        if (directionIntent.x > 0.0f)
            currentForce.x += Gravity.AdditionalInertiaPerupdate * xReveresed;
        else if (directionIntent.x < 0.0f)
            currentForce.x -= Gravity.AdditionalInertiaPerupdate * xReveresed;
        else
            currentForce.x -= Gravity.AdditionalInertiaPerupdate * Gravity.StoppedInertiaDump * xDirection;

        if (directionIntent.y > 0.0f)
            currentForce.y += Gravity.AdditionalInertiaPerupdate * yReveresed;
        else if (directionIntent.y < 0.0f)
            currentForce.y -= Gravity.AdditionalInertiaPerupdate * yReveresed;
        else
            currentForce.y -= Gravity.AdditionalInertiaPerupdate * Gravity.StoppedInertiaDump * yDirection;

        if (directionIntent.z > 0.0f)
            currentForce.z += Gravity.AdditionalInertiaPerupdate * zReveresed;
        else if (directionIntent.z < 0.0f)
            currentForce.z -= Gravity.AdditionalInertiaPerupdate * zReveresed;
        else
            currentForce.z -= Gravity.AdditionalInertiaPerupdate * Gravity.StoppedInertiaDump * zDirection;

        currentForce.x = (currentForce.x > 0.0f)
            ? Mathf.Min(currentForce.x, forceLimit)
            : Mathf.Max(currentForce.x, -forceLimit);
        currentForce.y = (currentForce.y > 0.0f)
            ? Mathf.Min(currentForce.y, forceLimit)
            : Mathf.Max(currentForce.y, -forceLimit);
        currentForce.z = (currentForce.z > 0.0f)
            ? Mathf.Min(currentForce.z, forceLimit)
            : Mathf.Max(currentForce.z, -forceLimit);

        return currentForce;
    }
}
