using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameWaveState : State
{
    protected override Animator anim { get; set; }
    protected override StateMachine stm { get; set; }
    protected override Coroutine coroutineHolder { get; set; }

    public override int Priority { get; set; }

    [SerializeField] int priority;

    [SerializeField] GameObject flameWaveProjectile;

    [SerializeField] float startupTime;
    [SerializeField] float endTime;

    public AK.Wwise.Event FireballSound;

    protected override void Start()
    {
        base.Start();

        Priority = priority;
    }
    protected override void OnFinish()
    {
        base.OnFinish();
        Debug.Log("FlameWave Finished");
    }

    protected override void PlayAnimation()
    {
        PlayFireballSound();
        anim.Play("ShootFireball");
    }

    protected override IEnumerator StateCoroutine()
    {
        yield return new WaitForSeconds(startupTime);

        GameObject flameWave = Instantiate(flameWaveProjectile, new Vector3(transform.position.x, -3.85f, transform.position.z), transform.rotation);
        //flameWave.transform.localScale = transform.localScale.normalized;
        flameWave.transform.localScale = new Vector2(flameWave.transform.localScale.x * Mathf.Sign(transform.localScale.x), flameWave.transform.localScale.y);

        yield return new WaitForSeconds(endTime);
        OnFinish();
    }

    public void PlayFireballSound()
    {
        FireballSound.Post(gameObject);
    }
}
