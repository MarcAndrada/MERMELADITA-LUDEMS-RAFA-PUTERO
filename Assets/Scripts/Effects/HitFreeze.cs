using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFreeze : MonoBehaviour
{
    bool hitStopped = false;
    
	public void Stop(float duration, float timeScale)
    {
		if (hitStopped)
        {
			return;
        }
		Time.timeScale = timeScale;
		StartCoroutine(Wait(duration));
	}

	public void Stop(float duration)
    {
		Stop(duration, 0.0f);
	}

	IEnumerator Wait(float duration)
    {
		hitStopped = true;

		yield return new WaitForSecondsRealtime(duration);
		Time.timeScale = 1.0f;

		hitStopped = false;
	}
}
