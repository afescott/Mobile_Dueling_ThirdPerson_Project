using UnityEngine;
using System.Collections;
using Cinemachine;
using UnityEngine.SceneManagement;

public class WarriorAnimationDemoFREE : MonoBehaviour 
{
	public Animator animator;

    public Joystick joystick;

  
    public CinemachineFreeLook camera;


	private int count = 0;

	float rotationSpeed = 30;
	Vector3 inputVec;
	Vector3 targetDirection;
    private Vector3 startPos; //mouse slide movement start pos
    private Vector3 endPos; //mouse slide movement end pos
    public float zDistance = 30.0f;//z distance
   
	//Warrior types
	public enum Warrior{Karate, Ninja, Brute, Sorceress, Knight, Mage, Archer, TwoHanded, Swordsman, Spearman, Hammer, Crossbow};
	public Warrior warrior;

	public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;
    public float clampAngle = 80.0f;
    public float inputSensitivity = 150.0f;


	//private void 
	void Start()
    {
		}


	
	void Update()
	{



        for (int i = 0; i < Input.touchCount; i++)
        {
			Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);

            var input = Input.touches[i];


		

			//camera.m_XAxis.Update(Time.deltaTime);

			//camera.UpdateCameraState(Vector3.up, Time.deltaTime);


			//camera.m_XAxis.Reset();
   //         camera.m_YAxis.Reset();


			var z1 = joystick.Horizontal;
            var x1 = joystick.Vertical;

			if (input.position.x > 0f && input.position.x < 390f) //left side of screen 
            {

               

                var z = 0f;
                var x = 0f;

                //Apply inputs to animator
                animator.SetFloat("Input X", z1);
                animator.SetFloat("Input Z", -(x1));

                if (x1 != 0 || z1 != 0) // figure out what
                {

                    if (x1 > 0)
                    {
                        x = 1;
                    }
                    else
                    if (x1 < 0)
                    {
                        x = -1;
                    }

                    if (z1 > 0)
                    {
                        z = 1;
                    }

                    else if (z1 < 0)
                    {
                        z = -1;
                    }

                    //set that character is moving
                    animator.SetBool("Moving", true);
                    count += 1;

                }
                else
                {
                    //character is not moving
                    animator.SetBool("Moving", false);
                }


                inputVec = new Vector3(x, 0, z);



			}
			else if (input.position.x > 398f && input.position.x < 900f) //left side of screen 
                //check if 
            {



				
				//camera.m_YAxis.m_InputAxisValue += input.position.y;
    //            camera.m_XAxis.m_InputAxisValue += input.position.x;



				//var mouseX = input.position.x;
				//var mouseY = input.position.y;


				//rotY += finalInputX * inputSensitivity * Time.deltaTime;
				//rotX += finalInputZ * inputSensitivity * Time.deltaTime;

				//rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

				//Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
				//transform.rotation = localRotation;




			}

			

            Debug.DrawLine(Vector3.zero, touchPosition, Color.red);

            var touchPHASE = Input.touches[i].phase;


		}


		if (Input.GetMouseButtonDown(0))
        {
            //get start mouse position
            Vector3 mousePos = Input.mousePosition * -1.0f;
            mousePos.z = zDistance; //add z distance

            startPos = Camera.main.ScreenToWorldPoint(mousePos);

            //Print start Pos for debugging
            //Debug.Log(startPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //get release mouse position
            Vector3 mousePos = Input.mousePosition * -1.0f;
            mousePos.z = zDistance; //add z distance

            // convert mouse position to world position
            endPos = Camera.main.ScreenToWorldPoint(mousePos);
            endPos.z = Camera.main.nearClipPlane; //removing this breaks stuff,no idea why though

            //Print start Pos for debugging
            //Debug.Log(endPos);

            Vector3 throwDir = (startPos - endPos).normalized;//get throw direction based on start and end pos


			//this.objecter.GetComponent<Rigidbody>().AddForce(throwDir * (startPos - endPos).sqrMagnitude);//add force to throw direction*magnitude

        }

		if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0); // Reset scene on pressing R
        }




		//horizontal > 0 right

		//vertical < 0 down

		

		//Get input from controls
		//float z = Input.GetAxisRaw("Horizontal");
		//float x = -(Input.GetAxisRaw("Vertical"));
		//inputVec = new Vector3(x, 0, z);

		////Apply inputs to animator
		//animator.SetFloat("Input X", z);
		//animator.SetFloat("Input Z", -(x));


		//// 

		//if (x != 0 || z != 0)  // figure out what
		//{
		//	//set that character is moving
		//	animator.SetBool("Moving", true);
		//}
		//else
		//{
		//	//character is not moving
		//	animator.SetBool("Moving", false);
		//}

		if (Input.GetButtonDown("Fire1"))
		{
			animator.SetTrigger("Attack1Trigger");
			if (warrior == Warrior.Brute)
				StartCoroutine (COStunPause(1.2f));
			else if (warrior == Warrior.Sorceress)
				StartCoroutine (COStunPause(1.2f));
			else
				StartCoroutine (COStunPause(.6f));
		}

		//update character position and facing
		UpdateMovement();
	}

	public IEnumerator COStunPause(float pauseTime)
	{
		yield return new WaitForSeconds(pauseTime);
	}

	//converts control input vectors into camera facing vectors
	void GetCameraRelativeMovement()
	{  
		Transform cameraTransform = Camera.main.transform;

		// Forward vector relative to the camera along the x-z plane   
		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;

		// Right vector relative to the camera
		// Always orthogonal to the forward vector
		Vector3 right= new Vector3(forward.z, 0, -forward.x);

		//directional inputs
		//float v = Input.GetAxisRaw("Vertical");
		//float h = Input.GetAxisRaw("Horizontal");

        var h = joystick.Horizontal;
        float v = joystick.Vertical;

		// Target direction relative to the camera
		targetDirection = h * right + v * forward;
	}

	//face character along input direction
	void RotateTowardMovementDirection()  
	{
		if(inputVec != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);
		}
	}

	void UpdateMovement()
	{
		//get movement input from controls
			Vector3 motion = inputVec;

            //reduce input for diagonal movement
            motion *= (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.z) == 1) ? 0.7f : 1;

            RotateTowardMovementDirection();
            GetCameraRelativeMovement();
	}

	//Placeholder functions for Animation events
	void Hit()
	{
	}

	void FootR()
	{
	}

	void FootL()
	{
	}

	void OnGUI () 
	{
		if (GUI.Button (new Rect (25, 85, 100, 30), "Attack1")) 
		{
			animator.SetTrigger("Attack1Trigger");

			//if character is Brute or Sorceress
			if (warrior == Warrior.Brute || warrior == Warrior.Sorceress)
			{
				StartCoroutine (COStunPause(1.2f));
			}
			else
			{
				StartCoroutine (COStunPause(.6f));
			}
		}
	}
}