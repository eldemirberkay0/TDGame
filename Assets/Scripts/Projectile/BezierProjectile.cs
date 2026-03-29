using System.Collections.Generic;
using UnityEngine;

public class BezierProjectile : ProjectileMovement
{
    protected float t;
    protected float passedTime;
    protected Vector2 initialPos;
    protected Vector2 controlPoint;
    protected Vector2 pos1;
    protected Vector2 pos2;
    protected Vector2 newPos;

    public override void Init(Transform target, float speed, bool isGuided)
    {
        base.Init(target, speed, isGuided);
        initialPos = transform.position;
        t = 1 / speed;
        controlPoint.x = (initialPos.x + targetPos.x) / 2f;
        controlPoint.y = Mathf.Max(initialPos.y, targetPos.y) + 2f;
        passedTime = 0;
    }

    protected override void GoToTarget()
    {
        passedTime += Time.deltaTime;
        pos1.x = Mathf.Lerp(initialPos.x, controlPoint.x, passedTime / t);
        pos1.y = Mathf.Lerp(initialPos.y, controlPoint.y, passedTime / t);
        pos2.x = Mathf.Lerp(controlPoint.x, targetPos.x, passedTime / t);
        pos2.y = Mathf.Lerp(controlPoint.y, targetPos.y, passedTime / t);

        newPos.x = Mathf.Lerp(pos1.x, pos2.x, passedTime / t);
        newPos.y = Mathf.Lerp(pos1.y, pos2.y, passedTime / t);

        // transform.rotation = new Vector4(new Vector3(newPos.x, newPos.y, 0f) - transform.position);
        transform.right = new Vector3(newPos.x, newPos.y, 0f) - transform.position;
        transform.position = newPos;
    }
}
