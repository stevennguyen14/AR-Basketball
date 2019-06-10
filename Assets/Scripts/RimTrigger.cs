using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RimTrigger : MonoBehaviour
{

	public GameObject scoreText;

	void Start()
	{
		scoreText.SetActive(false);
	}

	IEnumerator DisplayScore()
	{
		scoreText.SetActive(true);
		yield return new WaitForSeconds(3f);
		scoreText.SetActive(false);
	}
   
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("Ball"))
		{
			print("Scored!!!");
			StartCoroutine("DisplayScore");
		}
	}
}
