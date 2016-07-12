using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Required componenets for the GameObject which are needed and cannot be deleted
[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(BoxCollider2D))]


//SCRIPT WRITTEN BY: ANDREA



//Benny is child from BaseUnit which have methods that are used also for enemies, like Move()
public class Benny : BaseUnit
{

	//Declaration of variables uxsed in Benny class

	public static Benny instance;

	Touch touch;

	public float minSwipeDistanceX = 250;
	public float minSwipeDistanceY = 250;
    public float jumpHeight;
    public float slidingSpeed;
    public float dashingSpeed;
    public float direction = 1;
    public float dashSecondsTime;
    //This float is used to add some height to lmour Raycast, to avoid collision with the terrain
    private float sideCheckHeightFix = 0.1f;


	float swipeDistVertical;
	float swipeDistHorizontal;
    float dashingTime;
    
    Vector2 posInitial;
    Vector2 posFinal;
    Vector2 defaultVelocity;
	Vector2 vel;
	Vector3 rot;
	Vector2 offsetDash;
	Vector2 offsetSizeDefault;
	Vector2 offsetDefault;
	
	public bool isDashing = false;
	public bool isHiding = false;

    bool isSwitchTriggered = false;
    bool wallSliding = false;
	bool canJump;
	bool playedSound = true;
	bool extendedLedge = false;
	bool canUseAbility;
    bool isFlashing;

	int savedFriend;

	TerrainState stillOnWall;

	public BoxCollider2D bx;
	//receives the childs from Player object in the inspector
	Transform[] badBoy = new Transform[3];

	//ability enable at the moment
	public Abilities currentAbility;

	//audio clip array
	public AudioClip[] audioClip;

	void Start ()
	{
		Benny.instance = this;
		savedFriend = 0;
		defaultVelocity = rb.velocity;
		canUseAbility = true;
		canJump = true;
		int startSound = Random.Range (1, 4);
		if (startSound == 1)
			PlaySound (0);
		else if (startSound == 2)
			PlaySound (1);
		else {
			PlaySound (2);
		}

		//box collider defaults variables are stored in variables for later be restored
		bx = GetComponent<BoxCollider2D> ();
		offsetDefault = bx.offset;
		offsetSizeDefault = bx.size;
		//receives the childs into the badboy array
		for (int i = 0; i < 3; i++) {
			badBoy [i] = this.gameObject.transform.GetChild (i);
		}

		//only for Android: The game will automatically start on Landscape and cannot be rotated
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = true;

		currentAbility = Abilities.None;
		isFlashing = false;

		//playOutOfLockerAnim
		StartCoroutine ("ChangeCostume", "OutOfLocker");

	}


	void Update ()
	{
		stillOnWall = CheckSideHit ();
		dashingTime += Time.deltaTime;

		if (isDashing == false || isHiding == false) {
			//Moves forward every frame
			Move (direction);
		}
			
		if (wallSliding == false) {
			anim.SetBool ("WallSlide", false);
		}	

		//bind animator parameter "YVelocity" to the actual rigibody vertical velocity;
		anim.SetFloat ("YVelocity", rb.velocity.y);

		TerrainState sideHit = CheckSideHit ();

		//if benny touches a wall
		if (sideHit == TerrainState.Wall) {

			if (rb.velocity.y < 0 && (OnTerrain (raycastOffset) != TerrainState.Floor && OnTerrain (-raycastOffset) != TerrainState.Floor)) {
				//wall sliding is set to true, velocity downwards decreases and the animation for wall sliding begins
				wallSliding = true; 
				vel = rb.velocity;
				vel.x = 0;
				vel.y = -slidingSpeed;
				rb.velocity = vel;
			}

			if (wallSliding == true) {
				anim.SetBool ("WallSlide", true);
				Ability.instance.ChangeCostumeSprites (currentAbility, ClickState.Disabled);
				canUseAbility = false;
			}
			//bool check to see if the pointer/finger is currently on a UI object.	
			//if while wall sliding the player presses the jump input, it jumps and switches to jump animation
			#if UNITY_EDITOR
			if (Input.GetKeyDown (KeyCode.J)) {
				#else
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
				
				if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) 


				#endif
				{
					if (canJump == true) {
						//Jump in the opposite direction that you're moving
						anim.SetTrigger ("Jump");
						anim.SetBool ("enableJump", canJump);

						if (wallSliding && (OnTerrain (raycastOffset) != TerrainState.Floor && OnTerrain (-raycastOffset) != TerrainState.Floor)) {
							Jump (wallSliding, -direction); //change directions	
						} else {
							Jump (wallSliding, direction); //normal jump
							StartCoroutine ("WaitForWallJump");
						}
						Ability.instance.ChangeCostumeSprites (currentAbility, ClickState.Disabled);
						canUseAbility = false;
					} else {
						anim.SetBool ("enableJump", false);
						if (isFlashing == false) {
							Ability.instance.ChangeCostumeSprites (currentAbility, ClickState.Up);
						}
						canUseAbility = true;
					}
				}
			}

			//flips the direction of Benny when touching a wall
			Flip (-direction);

			//===============================================================================================

			//if touching the floor and a wall at the same time, and ihe vertical velocity is 0 or less, the jump animation stops, the direction changes and the wall stuck animation starts
			if (OnTerrain (raycastOffset) == TerrainState.Floor || OnTerrain (-raycastOffset) == TerrainState.Floor) {
				wallSliding = false;

				anim.SetBool ("IsGrounded", true);
				anim.SetBool ("WallSlide", false);
				Flip (direction);
				anim.SetBool ("WallStuck", true);
				Ability.instance.ChangeCostumeSprites (currentAbility, ClickState.Disabled);
				canUseAbility = false;
			} else {
				anim.SetBool ("WallStuck", false);
			}

			//==============================================================================================



		} else {

			//if its not touching a wall

			anim.SetBool ("WallStuck", false);
			//================================================================
			wallSliding = false;
			//=============================================================

			//if touching the floor, falling animation ends and if jump input is pressed, benny jumps and jumping animation starts
			if (OnTerrain (raycastOffset) == TerrainState.Floor || OnTerrain (-raycastOffset) == TerrainState.Floor) {
				anim.SetBool ("IsGrounded", true);
				if (isDashing != true && isHiding != true) {
					if (isFlashing == false) {
						Ability.instance.ChangeCostumeSprites (currentAbility, ClickState.Up);
					}
					canUseAbility = true;
				}
				#if UNITY_EDITOR
				if (Input.GetKeyDown (KeyCode.J)) {
					#else
				if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {

				if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) 
					#endif
					{
						if (canJump == true) {
							anim.SetTrigger ("Jump");
							anim.SetBool ("enableJump", canJump);
							Jump (wallSliding, raycastOffset);

						} else {
							anim.SetBool ("enableJump", false);
						}

					}

				} 
			} 
			//if not touching floor, and it is falling, and it is touching a wall, it will force it to fall while playind the falling animation
			else {
				anim.SetBool ("IsGrounded", false);
			}
			//flips the scale to the other direction
			Flip (direction);

		}
 

		//when the current dashing time reaches the dashing time limit benny stops dashing, velocity returns to normal and the box collider default values are restored
		if (dashingTime > dashSecondsTime && extendedLedge == false && currentAbility == Abilities.Dash) {
			if (isFlashing == false) {
				Ability.instance.ChangeCostumeSprites (currentAbility, ClickState.Up);
			}
			canUseAbility = true;
			anim.SetBool ("GroundSlide", false);
			isDashing = false;
			canJump = true;
			dashingTime = 0f;
			bx.size = offsetSizeDefault;
			bx.offset = offsetDefault;
			vel = rb.velocity;
			vel.x = direction * movementSpeed;
			rb.velocity = vel;
		}

	}

	//============================================================================================================================================================================
	//========== END OF VOID UPDATE() ============================================================================================================================================
	//============================================================================================================================================================================

	//check what the player is touching from the side
	TerrainState CheckSideHit ()
	{
		//origin for raycast is at pivot
		Vector3 origin = transform.position;
		origin.y += sideCheckHeightFix;

		//Shoots raycast to the direction Benny is facing
		RaycastHit2D hitInfo = Physics2D.Raycast (origin, new Vector2 (direction, 0), 1.7f);

		//returns what benny is touching
		if (hitInfo.collider == null) {
			return TerrainState.None;
		} else {
			if (hitInfo.collider.gameObject.tag.Equals ("Floor")) {
				return TerrainState.Floor;
			} else if (hitInfo.collider.gameObject.tag.Equals ("Wall")) {
				return TerrainState.Wall;
			} else {
				return TerrainState.Other;
			}
		}
	}

	//Method to jump, based on the direction and if it is wallSliding
	void Jump (bool wallSliding, float dir)
	{
		if (wallSliding == true) {
			direction = dir;
		}
		vel = rb.velocity;
		vel.y = jumpHeight;
		rb.velocity = vel;
		PlaySound (7);
	}

	//Method to Dash ability
	void Dash ()
	{
		vel = rb.velocity;
		vel.x = direction * dashingSpeed;
		rb.velocity = vel;

		float offsetX = bx.size.x;
		offsetDash = bx.size;
		offsetDash.y = offsetX;
		bx.size = offsetDash;

		Vector2 offsetPos = bx.offset;
		offsetPos.y = 1.3f;
		bx.offset = offsetPos;	
	}

	//Method to Hide ability
	void NinjaHide ()
	{
		//3 is to freeze all position and rotation
		ChangeConstraints (3);
		canJump = false;
		rb.velocity = Vector2.zero;
		PlaySound (5);
	}

	//changes the constraints of the rigidbody2D
	void ChangeConstraints (int option)
	{
		//each number will enter the switch in their respective case and freeze or unfreeze constraints
		switch (option) {
		//unfreezes everything
		case 0:
			rb.constraints = RigidbodyConstraints2D.None;
			break;
		//freezes position in Y
		case 1:
			rb.constraints = RigidbodyConstraints2D.FreezePositionY;
			break;
		//freezes rotation
		case 2:
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			break;
		//freezes everything
		case 3:
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			break;
		}
	}

	//starts when it collides with another collider
	void OnTriggerEnter2D (Collider2D coll)
	{
		//if touches a CSM or Friend it destroys it, if it is a CSM the level is loaded again. Later this will be for level complete screen
		if (coll.gameObject.GetComponentInParent<Friend> () != null) {

			Friend.inst.savedFriends [savedFriend] = coll.gameObject.tag;
			savedFriend++;
			Friend.inst.CelebrationAnim (coll.gameObject.tag);
			coll.enabled = false;
		} else if (coll.gameObject.tag.Equals ("CSM") || coll.gameObject.tag.StartsWith ("Cage")) {
			//put anim of benny punching here
			anim.SetTrigger ("cageTouch");
			//waits till animation of benny punching finishes and then starts CSM or Cage breaking
			if (coll.gameObject.tag.Equals ("CSM")) {
				PlaySound (8);
				StartCoroutine ("PlayWinningSound");
				StartCoroutine ("WaitForAnim", 0);		
			} else {
				switch (coll.gameObject.tag) {
				case "Cage1":
					Prison.inst.DestroyAnim (0);
					break;
				case "Cage2":
					Prison.inst.DestroyAnim (1);
					break;
				case "Cage3":
					Prison.inst.DestroyAnim (2);
					break;
				}
			}

		} 
		//sets the ability of the costume to equip to true and changes costume and disables the others
		else if (coll.gameObject.GetComponent<Costume> () != null) {
			if (coll.gameObject.tag.Equals ("Ninja") && currentAbility != Abilities.Hide) {
				currentAbility = Abilities.Hide;
				Costume.instance.PlayCostumeSound ();
				Ability.instance.ChangeCostumeSprites (currentAbility, ClickState.Up);
				canUseAbility = true;
				//anim change to ninja
				anim.SetTrigger ("NinjaTransform");
				StartCoroutine ("ChangeCostume", "Ninja");
				PlaySound (10);

			} else if (coll.gameObject.tag.Equals ("Warrior") && currentAbility != Abilities.Dash) {
				currentAbility = Abilities.Dash;
				Costume.instance.PlayCostumeSound ();
				Ability.instance.ChangeCostumeSprites (currentAbility, ClickState.Up);
				canUseAbility = true;
				//anim change to warrior
				anim.SetTrigger ("WarriorTransform");
				StartCoroutine ("ChangeCostume", "Warrior");
				PlaySound (11);

			} else if (coll.gameObject.tag.Equals ("Benny") && currentAbility != Abilities.None) {
				currentAbility = Abilities.None;
				Costume.instance.PlayCostumeSound ();
				Ability.instance.ChangeCostumeSprites (currentAbility, ClickState.Up);
				canUseAbility = true;
				//anim change to benny
				anim.SetTrigger ("BennyTransform");
				StartCoroutine ("ChangeCostume", "Benny");
				PlaySound (9);
			}
			//when player collides with the switch, the switch animation is played, the door opens and the bool variable which will be used later is set to true
		} else if (coll.gameObject.tag.Equals ("Switch")) {
			Switch.inst.SwitchTrigger ();
			if (isSwitchTriggered == false) {
				Switch.inst.PlayAlarmSound ();
			}
			isSwitchTriggered = true;
			Door.inst.OpenDoor ();
		} else if (coll.gameObject.tag.Equals ("DashArea")) {
			extendedLedge = true;
		} else if (coll.gameObject.tag.Equals ("Fan")) {
			Destroy (this.gameObject);
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		} else if (coll.gameObject.tag.Equals ("Highlight")) {
			Ability.instance.ChangeCostumeSprites (currentAbility, ClickState.Highlight);
			StartCoroutine ("FlashingButton");
		}
	}

	public void Restart ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	void OnTriggerExit2D (Collider2D coll)
	{
		if (coll.gameObject.tag.Equals ("DashArea")) {
			extendedLedge = false;
		}
	}

	//if the player collides with a closed door and if the switch has not been opened the player will just turn around
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag.Equals ("Door")) {
			if (isSwitchTriggered != true) {
				Flip (-direction);
				direction *= -1;
			} 
		}
	}

	void PlaySound (int clip)
	{
		//play audio source based on array.
		AudioSource audio = GetComponent<AudioSource> ();
		audio.clip = audioClip [clip];
		audio.PlayOneShot (audio.clip);
	}

	public void ForceDash ()
	{
		if (canUseAbility == true) {
			if (OnTerrain (raycastOffset) == TerrainState.Floor || OnTerrain (-raycastOffset) == TerrainState.Floor) {
				//calls dash, dashing time starts set to 0
				Dash ();
				PlaySound (16);
				Ability.instance.ChangeCostumeSprites (currentAbility, ClickState.Down);
				canJump = false;
				isDashing = true;
				dashingTime = 0f;
				anim.SetBool ("GroundSlide", true);
			}	
		}

	}

	public void ForceHide ()
	{
		if (canUseAbility == true) {
			if (OnTerrain (raycastOffset) == TerrainState.Floor || OnTerrain (-raycastOffset) == TerrainState.Floor) {
				isHiding = !isHiding;
				if (isHiding) {
					Ability.instance.ChangeCostumeSprites (currentAbility, ClickState.Down);
					NinjaHide ();
					anim.SetBool ("Hide", true);	
				} else {
					if (isFlashing == false) {
						Ability.instance.ChangeCostumeSprites (currentAbility, ClickState.Up);
					}
					canUseAbility = true;
					anim.SetBool ("Hide", false);
					StartCoroutine ("WaitForJump");
				}
			}
		}

	}

	//////////////////////////////////////////////////////////
	///COROUTINES
	//////////////////////////////////////////////////////////

	IEnumerator WaitForJump ()
	{
		rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
		rb.velocity = defaultVelocity;
		canJump = false;
		anim.SetBool ("enableJump", false);
		yield return new WaitForSeconds (0.1f);
		canJump = true;
		//restore constraints to default
		ChangeConstraints (2);
	}

	IEnumerator WaitForWallJump ()
	{
		canJump = false;
		while (wallSliding == false && stillOnWall == TerrainState.Wall) {
			//Do nothing and wait
			yield return null;
		}
		canJump = true;
	}

	IEnumerator ChangeCostume (string costumeTag)
	{
		ChangeConstraints (3);
		yield return new WaitForSeconds (0.7f);
		switch (costumeTag) {
		case "Ninja":
			badBoy [0].gameObject.SetActive (false);
			badBoy [2].gameObject.SetActive (false);
			badBoy [1].gameObject.SetActive (true);
			break;
		case "Warrior":
			badBoy [0].gameObject.SetActive (false);
			badBoy [1].gameObject.SetActive (false);
			badBoy [2].gameObject.SetActive (true);
			break;
		case "Benny":
			badBoy [2].gameObject.SetActive (false);
			badBoy [1].gameObject.SetActive (false);
			badBoy [0].gameObject.SetActive (true);
			break;
		case "OutOfLocker":
			yield return new WaitForSeconds (0.3f);
			break;
		}
		ChangeConstraints (2);
	}

	IEnumerator WaitForAnim (int objectTag)
	{
		ChangeConstraints (3);
		yield return new WaitForSeconds (0.05f);
		ChangeConstraints (2);
		if (objectTag == 0) {
			CSM.instance.ActivateDestruction ();
		}
	}

	IEnumerator PlayWinningSound ()
	{
		yield return new WaitForSeconds (0.9f);
		int winSound = Random.Range (19, 22);
		PlaySound (winSound);
	}

	IEnumerator FlashingButton ()
	{
		isFlashing = true;
		yield return new WaitForSeconds (1.0f);
		isFlashing = false;
	}


}

public enum Abilities
{
	Dash,
	Hide,
	None
}


