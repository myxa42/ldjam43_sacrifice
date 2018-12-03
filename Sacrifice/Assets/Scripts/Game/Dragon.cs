using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Spine;
using Spine.Unity;

public class Dragon : MonoBehaviour
{
    public SkeletonGraphic animation;
    private Queue<RoyalPerson> personsToEat = new Queue<RoyalPerson>();
    private TrackEntry currentEatAnimation;
    public GameController gameController;
    public AudioSource omnomnomSound;
    public AudioSource niceFoodSound;
    public AudioSource badFoodSound;
    private bool gameOver;

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
            if (gameOver)
                SceneManager.LoadScene("GameOver");

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

    private void OnEvent(TrackEntry track, Spine.Event e)
    {
        if (e.data.Name == "omnomnom")
            omnomnomSound.Play();
        else if (e.data.Name == "mmm")
            niceFoodSound.Play();
        else if (e.data.Name == "badfood")
            badFoodSound.Play();
    }

    private void DoEat(RoyalPerson person)
    {
        if (person.isEdible)
        {
            gameController.sacrificedCount++;
            currentEatAnimation = animation.AnimationState.SetAnimation(0, "eat_princess", false);
        }
        else
        {
            currentEatAnimation = animation.AnimationState.SetAnimation(0, "eat_prince", false);
            gameOver = true;
        }

        Destroy(person.gameObject);

        currentEatAnimation.Complete -= OnComplete;
        currentEatAnimation.Complete += OnComplete;

        currentEatAnimation.Event -= OnEvent;
        currentEatAnimation.Event += OnEvent;
    }
}
