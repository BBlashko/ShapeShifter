using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject TrianglePrefab;
    public GameObject SquarePrefab;
    public GameObject RectanglePrefab;

    public GameObject mPlayerLeftBoundaryCollider;

    public enum PlayerShape
    {
        SQUARE,
        RECTANGLE,
        TRIANGLE
    };

    void Start()
    {
        //player object
        mPlayerShapeObject = Instantiate(SquarePrefab);
        mPlayerShapeObject.transform.parent = gameObject.transform;
        mPlayerShapeObject.transform.position = gameObject.transform.position;
        mPlayerShapeRigidBody = gameObject.GetComponent<Rigidbody>();
        mPlayerShapeParticleSystem = mPlayerShapeObject.GetComponentInChildren<ParticleSystem>();

        //IMPORTANT: get this collider before creating others?
        //TRIGGER BOX COLLIDER
        mPlayerBottomCollider = gameObject.GetComponent<BoxCollider>();

        //set default square mesh collider NON-TRIGGER
        mPlayerMeshCollider = gameObject.AddComponent<BoxCollider>();
        mHorizontalGravity = gameObject.AddComponent<ConstantForce>();
    }

    void Destroy() { }

    void FixedUpdate()
    {
        CheckKeyInputs();
        if (mIsBursting)
        {
            CheckHorizontalSpeed();
        }

        Debug.Log(mPlayerShapeRigidBody.velocity);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.transform.tag == mGroundTag)
        {
            Debug.Log("Collision Stayed");
            mPlayerShapeRigidBody.useGravity = false;
            mPlayerShapeRigidBody.velocity = mGroundSpeed;
            mIsJumping = false;
            mCanDoubleJump = true;
            mPlayerShapeParticleSystem.Play();
        }

        if (collisionInfo.gameObject.transform.tag == mLeftBoundaryTag && !mIsJumping)
        {
            mIsInDefaultPosition = true;
        }
        else
        {
            mIsInDefaultPosition = false;
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.transform.tag == mGroundTag)
        {
            mPlayerShapeRigidBody.useGravity = true;
            mPlayerShapeParticleSystem.Stop();
            mPlayerShapeParticleSystem.Clear();
        }

        if (collisionInfo.gameObject.transform.tag == mLeftBoundaryTag)
        {
            mIsInDefaultPosition = false;
        }
    }

    private void CheckHorizontalSpeed()
    {
        if (mPlayerShapeRigidBody.velocity.x < 4.0f)
        {
            mPlayerShapeRigidBody.useGravity = true;
        }
        if (mPlayerShapeRigidBody.velocity.x <= 0.0f)
        {
            mPlayerShapeRigidBody.velocity = mDefaultZeroSpeed;
            DisableHorizontalGravity();
            mIsBursting = false;
            if (!mIsJumping)
            {
                mPlayerShapeRigidBody.useGravity = false;
                mPlayerShapeRigidBody.velocity = mGroundSpeed;
            }
        }
    }

    private void EnableHorizontalGravity()
    {
        mPlayerShapeRigidBody.useGravity = false;
        mHorizontalGravity.force = mHorizontalForceSpeed;
    }

    private void DisableHorizontalGravity()
    {
        mPlayerShapeRigidBody.useGravity = true;
        mHorizontalGravity.force = mDefaultZeroSpeed;
    }

    private void CheckKeyInputs()
    {
        //jumping
        if (!mIsBursting)
        {
            if (Input.GetKeyDown("w") && (!mIsJumping || mCanDoubleJump))
            {
                //jump
                if (mIsJumping)
                {
                    mCanDoubleJump = false;
                }
                else
                {
                    mIsJumping = true;
                }

                if (mCurrentShape == PlayerShape.RECTANGLE)
                {
                    mPlayerShapeRigidBody.velocity = mRectangleJumpSpeed;
                }
                else
                {
                    mPlayerShapeRigidBody.velocity = mJumpSpeed;
                }
                Debug.Log("jumped!");
            }
            else if (Input.GetKeyDown("a") && mCurrentShape != PlayerShape.SQUARE)
            {
                //square
                ChangeShape(PlayerShape.SQUARE);
            }
            else if (Input.GetKeyDown("d"))
            {
                //triangle
                if (mCurrentShape != PlayerShape.TRIANGLE)
                {
                    ChangeShape(PlayerShape.TRIANGLE);
                }
                mPlayerShapeRigidBody.velocity = mTriangleBurstSpeed;
                EnableHorizontalGravity();
                mIsBursting = true;
            }
            else if (Input.GetKeyDown("s"))
            {
                //rectangle
                if (mCurrentShape != PlayerShape.RECTANGLE)
                {
                    ChangeShape(PlayerShape.RECTANGLE);
                }
                mPlayerShapeRigidBody.velocity = mRectangleFallSpeed;
            }
        }
    }

    private void ChangeBoxCollider(bool isRectangle)
    {
        mPlayerShapeRigidBody.isKinematic = true;
        if (isRectangle)
        {
            mPlayerMeshCollider.size = new Vector3(2.25f, 0.5f, 1.0f);
            mPlayerMeshCollider.center = new Vector3(0.0f,
                                                     -mRectangleYOffset,
                                                     0.0f);
            mPlayerLeftBoundaryCollider.transform.position = new Vector3(-6.625f, 0.0f, 0.0f);
        }
        else
        {
            mPlayerMeshCollider.size = new Vector3(1.0f, 1.0f, 1.0f);
            mPlayerMeshCollider.center = new Vector3(0.0f,
                                                     0.0f,
                                                     0.0f);
            mPlayerLeftBoundaryCollider.transform.position = new Vector3(-6.0f, 0.0f, 0.0f);
        }
        mPlayerShapeRigidBody.isKinematic = false;
    }

    private void ChangeShape(PlayerShape shape)
    {
        Vector3 currentPosition = mPlayerShapeObject.transform.position;
        if (mCurrentShape == PlayerShape.RECTANGLE)
        {
            currentPosition.y += mRectangleYOffset;
        }
        else if (shape == PlayerShape.RECTANGLE)
        {
            currentPosition.y -= mRectangleYOffset;
        }

        Destroy(mPlayerShapeObject);
        switch (shape)
        {
            case PlayerShape.SQUARE:
                //change shape
                mPlayerShapeObject = Instantiate(SquarePrefab);
                //update shape collider size
                ChangeBoxCollider(false);
                mCurrentShape = PlayerShape.SQUARE;
                break;
            case PlayerShape.RECTANGLE:
                //change shape
                mPlayerShapeObject = Instantiate(RectanglePrefab);
                //update shape collider size
                ChangeBoxCollider(true);
                mCurrentShape = PlayerShape.RECTANGLE;
                break;
            case PlayerShape.TRIANGLE:
                //change shape
                mPlayerShapeObject = Instantiate(TrianglePrefab);
                //update shape collider size
                ChangeBoxCollider(false);
                mCurrentShape = PlayerShape.TRIANGLE;
                break;
            default:
                break;
        }
        mPlayerShapeObject.transform.parent = gameObject.transform;
        mPlayerShapeObject.transform.position = currentPosition;
        mPlayerShapeParticleSystem = mPlayerShapeObject.GetComponentInChildren<ParticleSystem>();
        if (mIsJumping)
        {
            mPlayerShapeParticleSystem.Stop();
            mPlayerShapeParticleSystem.Clear();
        }
        if (!mIsInDefaultPosition && !mIsJumping)
        {
            mPlayerShapeRigidBody.velocity = mGroundSpeed;
        }
    }

    //defaults
    //player object
    private GameObject mPlayerShapeObject;
    private Rigidbody mPlayerShapeRigidBody;
    private ParticleSystem mPlayerShapeParticleSystem;
    private PlayerShape mCurrentShape = PlayerShape.SQUARE;
    private Vector2 mDefaultPosition = new Vector2(-6.0f, -2.0f);
    private float mRectangleYOffset = 0.265f;

    //player box colliders
    private BoxCollider mPlayerMeshCollider;
    private BoxCollider mPlayerBottomCollider;
    private BoxCollider mPlayerDeathCollider;

    private ConstantForce mHorizontalGravity;

    //jumping and collisions
    private const string mPlayerShapeObjectTag = "PlayerShape";
    private const string mGroundTag = "Ground";
    private const string mLeftBoundaryTag = "LeftBoundary";
    private bool mIsJumping = false;
    private bool mIsBursting = false;
    private bool mCanDoubleJump = true;
    private bool mIsInDefaultPosition = false;

    private Vector3 mDefaultZeroSpeed = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 mGroundSpeed = new Vector3(-4.33f, 0.0f, 0.0f);
    private Vector3 mJumpSpeed = new Vector3(0.0f, 9.5f, 0.0f);
    private Vector3 mRectangleJumpSpeed = new Vector3(0.0f, 5.0f, 0.0f);
    private Vector3 mRectangleFallSpeed = new Vector3(0.0f, -10.0f, 0.0f);
    private Vector3 mTriangleBurstSpeed = new Vector3(20.0f, 0.0f, 0.0f);
    private Vector3 mHorizontalForceSpeed = new Vector3(-22.0f, 0.0f, 0.0f);
}