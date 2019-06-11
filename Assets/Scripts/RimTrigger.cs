using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RimTrigger : MonoBehaviour
{
    public GameObject mathManager;
    public Text textScore;

    public bool hasTriggered = false;

    void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Ball") && !hasTriggered)
		{
			print("Scored!!!");
            hasTriggered = true;
            mathManager.SendMessage("CheckAnswer", textScore);
            StartCoroutine(Timer());
		}
	}

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        hasTriggered = false;
    }
}
