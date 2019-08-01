using UnityEngine;
using System.Collections;

public class camera_behaviour : MonoBehaviour
{

    public controller2D target;
    public Vector2 focusAreaSize;
    public float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;

    public float verticalOffset;

    FocusArea focusArea;

    float currentLookAheadX;
    float targetLookAheadX;
    float lookAheadDirX;
    float smoothLookVelocityX;
    float smoothVelocityY;

    bool lookAheadStoppped;

    void Start()
    {
        focusArea = new FocusArea(target.collider.bounds, focusAreaSize);
    }

    void LateUpdate()
    {
        focusArea.Update(target.collider.bounds);

        Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;

        if (focusArea.velocity.x != 0)
        {
            lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
            if (Mathf.Sign(target.playerInput.x) == Mathf.Sign(focusArea.velocity.x) && target.playerInput.x != 0)
            {
                lookAheadStoppped = false;
                targetLookAheadX = lookAheadDirX * lookAheadDstX;
            }
            else
            {
                if (!lookAheadStoppped)
                {
                    lookAheadStoppped = true;
                    targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX / 4f);
                }
            }
        }


        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);
        focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
        focusPosition += Vector2.right * currentLookAheadX;
        transform.position = (Vector3)focusPosition + Vector3.forward * -10;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.center, focusAreaSize);
    }

    struct FocusArea
    {
        public Vector2 center;
        public Vector2 velocity;
        float left, right;
        float top, bottom;

        public FocusArea(Bounds targetBound, Vector2 size)
        {
            left = targetBound.center.x - size.x / 2;
            right = targetBound.center.x + size.x / 2;
            bottom = targetBound.min.y;
            top = targetBound.min.y + size.y;

            velocity = Vector2.zero;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds targetBound)
        {
            float shiftX = 0;
            if (targetBound.min.x < left)
            {
                shiftX = targetBound.min.x - left;
            }
            else if (targetBound.max.x > right)
            {
                shiftX = targetBound.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBound.min.y < bottom)
            {
                shiftY = targetBound.min.y - bottom;
            }
            else if (targetBound.max.y > top)
            {
                shiftY = targetBound.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;

            center = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }

}

