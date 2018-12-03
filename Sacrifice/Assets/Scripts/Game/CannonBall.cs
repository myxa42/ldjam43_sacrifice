using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public RoyalPerson target;
    public float speed;
    private Vector3 targetPosition;
    [System.NonSerialized] public GameController gameController;

    void Explode()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        if (target == null)
        {
            Explode();
            return;
        }

        targetPosition = transform.parent.worldToLocalMatrix * target.transform.position;

        float step = Time.deltaTime * speed;

        var pos = transform.localPosition;
        var delta = targetPosition - pos;
        float length = delta.magnitude;
        delta /= length;
        if (length <= step)
        {
            transform.localPosition = targetPosition;
            if (target != null)
            {
                var body = Instantiate(gameController.deadPrincePrefab, target.transform.parent);
                body.transform.position = target.transform.position;
                Destroy(target.gameObject);
            }
            Explode();
        }
        else
        {
            pos += delta * step;
            transform.localPosition = pos;
        }
    }
}
