﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

	Controller2D controller;

	bool inputFromUI;
	bool leftButtonPushed;
	bool rightButtonPushed;
	bool upButtonPushed;
	bool upButtonReleased;

	public Animator playerAnimator;
	public GameObject playerSprite;
	Vector3 playerSpriteInitialScale;

	public CameraShake currentCamera;

	private Vector2 GetInputFromUI()
	{
		Vector2 input = Vector2.zero;
		if (leftButtonPushed)
		{
			input.x += -1;
		}
		if (rightButtonPushed)
		{
			input.x += 1;
		}
		return input;
	}
	public void PushLeftButton()
	{
		inputFromUI = true;
		leftButtonPushed = true;
	}
	public void ReleaseLeftButton()
	{
		inputFromUI = true;
		leftButtonPushed = false;
	}
	public void PushRightButton()
	{
		inputFromUI = true;
		rightButtonPushed = true;
	}
	public void ReleaseRightButton()
	{
		inputFromUI = true;
		rightButtonPushed = false;
	}
	public void PushUpButton()
	{
		inputFromUI = true;
		upButtonPushed = true;
		upButtonReleased = false;
	}
	public void ReleaseUpButton()
	{
		inputFromUI = true;
		upButtonReleased = true;
		upButtonPushed = false;
	}


	void Start() 
	{
		playerSpriteInitialScale = playerSprite.transform.localScale;

		controller = GetComponent<Controller2D> ();

		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
		print ("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);
	}

	void Update() 
	{
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		if (inputFromUI)
		{
			input = GetInputFromUI();
		}

		int wallDirX = (controller.collisions.left) ? -1 : 1;

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);

		bool wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) 
		{
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) 
			{
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) 
			{
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (input.x != wallDirX && input.x != 0) 
				{
					timeToWallUnstick -= Time.deltaTime;
				}
				else 
				{
					timeToWallUnstick = wallStickTime;
				}
			}
			else 
			{
				timeToWallUnstick = wallStickTime;
			}

		}

		if (Input.GetKeyDown (KeyCode.Space) || (inputFromUI && upButtonPushed))
		{
			upButtonPushed = false;
			if (wallSliding) 
			{
				if (wallDirX == input.x) 
				{
					velocity.x = -wallDirX * wallJumpClimb.x;
					velocity.y = wallJumpClimb.y;
				}
				else if (input.x == 0) 
				{
					velocity.x = -wallDirX * wallJumpOff.x;
					velocity.y = wallJumpOff.y;
				}
				else 
				{
					velocity.x = -wallDirX * wallLeap.x;
					velocity.y = wallLeap.y;
				}
			}
			if (controller.collisions.below)
			{
				velocity.y = maxJumpVelocity;
			}
		}
		if (Input.GetKeyUp (KeyCode.Space) || (inputFromUI && upButtonReleased)) 
		{
			upButtonReleased = false;
			if (velocity.y > minJumpVelocity) 
			{
				velocity.y = minJumpVelocity;
			}
		}

	
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime, input);

		if (controller.collisions.above || controller.collisions.below) 
		{
			velocity.y = 0;
		}

		UpdateAnimator ();

		if (Input.GetKeyDown(KeyCode.H))
		{
			Hit ();
		}
	}

	public void UpdateAnimator()
	{
		playerAnimator.SetBool ("IsOnGround", IsOnGround ());
		playerAnimator.SetBool ("IsRunning", IsRunning ());
		playerAnimator.SetBool ("IsOnWall", IsOnWall ());
		bool facingRight = controller.collisions.faceDir == 1;
		playerSprite.transform.localScale = new Vector3 (playerSpriteInitialScale.x * (facingRight ? 1 : -1), playerSpriteInitialScale.y, playerSpriteInitialScale.z); 
	}

	public bool IsOnGround()
	{
		return controller.collisions.below;
	}
	public bool IsRunning()
	{
		float epsilonMove = 0.01f;
		return Mathf.Abs(velocity.x) > epsilonMove;
	}
	public bool IsOnWall()
	{
		return controller.collisions.right || controller.collisions.left;
	}

	public void Hit()
	{
		playerAnimator.SetTrigger ("Hit");
		if (currentCamera != null)
		{
			currentCamera.TriggerShake();
		}
	}
}
