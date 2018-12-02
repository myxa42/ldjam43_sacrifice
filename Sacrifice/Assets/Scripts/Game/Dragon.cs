using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class Dragon : MonoBehaviour
{
    public SkeletonGraphic animation;
    private Queue<RoyalPerson> personsToEat = new Queue<RoyalPerson>();
    private TrackEntry currentEatAnimation;

    public void Eat(RoyalPerson person)
    {
        if (currentEatAnimation != null)
        {
            personsToEat.Enqueue(person);
            return;
        }

        DoEat(person);
    }

    private void OnComplete(TrackEntry track)
    {
        if (track == currentEatAnimation)
        {
            currentEatAnimation = null;
            if (personsToEat.Count > 0)
            {
                var person = personsToEat.Dequeue();
                DoEat(person);
            }
            else
            {
                animation.AnimationState.SetAnimation(0, "idle", true);
            }
        }
    }

    private void DoEat(RoyalPerson person)
    {
        if (person.isEdible)
            currentEatAnimation = animation.AnimationState.SetAnimation(0, "eat_princess", false);
        else
            currentEatAnimation = animation.AnimationState.SetAnimation(0, "eat_prince", false);

        Destroy(person.gameObject);

        currentEatAnimation.Complete -= OnComplete;
        currentEatAnimation.Complete += OnComplete;
    }
}
