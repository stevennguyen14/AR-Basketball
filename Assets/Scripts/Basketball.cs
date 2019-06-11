using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basketball : MonoBehaviour
{
	//public
	public float throwSpeed;
	public Vector3 offset;

	//private
	private float speed;
	private float lastMouseX, lastMouseY;
	private bool thrown, holding;
	private Rigidbody rb;
	private Vector3 newPosition;


    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
		Reset();
    }

    // Update is called once per frame
    void Update()
    {
		if (holding)
		{
			OnTouch();
		}

		if (thrown)
		{
			return;
		}

		if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit hit;

			if(Physics.Raycast (ray, out hit, 100f))
			{
				if(hit.transform == transform)
				{
					holding = true;
					transform.SetParent(null);
				}
			}
		}

		if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			if(lastMouseY < Input.GetTouch(0).position.y)
			{
				ThrowBall(Input.GetTouch(0).position);
			}
		}

		if(Input.touchCount == 1)
		{
			lastMouseX = Input.GetTouch(0).position.x;
			lastMouseY = Input.GetTouch(0).position.y;
		}
    }

	void Reset()
	{
		CancelInvoke();
		transform.position = Camera.main.ViewportToWorldPoint(new Vector3(offset.x, offset.y, Camera.main.nearClipPlane * 7.5f));
		newPosition = transform.position;
		thrown = false;
		holding = false;

		rb.useGravity = false;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		transform.rotation = Quaternion.Euler(0f, 200f, 0f);
		transform.SetParent(Camera.main.transform);
	}

	void OnTouch()
	{
		Vector3 mousePos = Input.GetTouch(0).position;
		mousePos.z = Camera.main.nearClipPlane * 7.5f;

		newPosition = Camera.main.ScreenToWorldPoint(mousePos);

		transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, 50f * Time.deltaTime);
	}

	void ThrowBall(Vector2 mousePos)
	{
		rb.useGravity = true;

		float differenceY = (mousePos.y - lastMouseY) / Screen.height * 100;
		speed = throwSpeed * differenceY;

		float x = (mousePos.x / Screen.width) - (lastMouseX / Screen.width);
		x = Mathf.Abs(Input.GetTouch(0).position.x - lastMouseX) / Screen.width * 100 * x;

		Vector3 direction = new Vector3(x, 0f, 1f);
		direction = Camera.main.transform.TransformDirection(direction);

		rb.AddForce((direction*speed/2f)+(Vector3.up*speed));

		holding = false;
		thrown = true;

		//reset after 5 seconds
		Invoke("Reset", 3.0f);
	}
}
