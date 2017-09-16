using UnityEngine;

public class PlayerMovement : PlayerShape {

    public static PlayerMovement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerMovement();
            }
            return instance;
        }
    }

    public void Update()
    {
        //Check Position for death
        if (mPlayerObject.transform.position.y < -5.2f /*|| mPlayerObject.transform.position.x < -6.0f*/)
        {
            InstantDeath();
        }

        //Player burst checks
        if (mPlayerRigidBody.velocity.x <= 0.5f && mIsBursting)
        {
            mIsBursting = false;
            SetGravity(true);
        }
        else if (mIsBursting)
        {
            mPlayerRigidBody.AddForce(mHorizontalForce);
        }
        
        if (mIsGrounded && !mParticlesEnabled)
        {
            EnableParticles();
            mParticlesEnabled = true;
        }
        else if (!mIsGrounded && mParticlesEnabled)
        {
            DisableParticles();
            mParticlesEnabled = false;
        }
    }

    public void SetGravity(bool b)
    {
        mPlayerRigidBody.useGravity = b;
    }

    #region Player Controlled Movements
    public void EnableDefaultShape()
    {
        ChangeShape(Shape.SQUARE);
    }

    public void Jump()
    {
        if (mIsGrounded || mAllowedDoubleJump)
        {
            switch(CurrentShape)
            {
                case Shape.SQUARE:
                    mPlayerRigidBody.velocity = mSquareJumpVelocity;
                    break;
                case Shape.RECTANGLE:
                    mPlayerRigidBody.velocity = mRectangleJumpVelocity;
                    break;
                case Shape.TRIANGLE:
                    EnableDefaultShape();
                    mPlayerRigidBody.velocity = mSquareJumpVelocity;
                    break;
            }
            if (!mIsGrounded && mAllowedDoubleJump)
            {
                mAllowedDoubleJump = false;
            }
            mIsGrounded = false;
        }
    }

    //Triangular right "Burst" acceleration
    public void Burst()
    {
        if ((mIsAgainstLeftBoundary || GamePlayManager.Instance.PlayingLevel) && !mIsBursting)
        {
            ChangeShape(Shape.TRIANGLE);
            mIsBursting = true;
            SetGravity(false);
            mPlayerRigidBody.velocity = mBurstVelocity;
        }
    }

    //Rectangle down "Drop" acceleration
    public void Drop()
    {
        ChangeShape(Shape.RECTANGLE);
        mPlayerRigidBody.velocity = mDropVelocity;
    }
#endregion

    #region Collision
    public void GroundCollisionStay(Collision collisionInfo)
    {
        mIsGrounded = true;
        mAllowedDoubleJump = true;

        //Moving players finished burst towards the left boundary
        if (!mIsAgainstLeftBoundary && !mIsBursting && !GamePlayManager.Instance.PlayingLevel)
        {
            GameObject collisionObject = collisionInfo.gameObject;

            float xSpeed;
            if (collisionObject.GetComponentInParent<GroundScroller>() != null)
            {
                xSpeed = collisionObject.GetComponentInParent<GroundScroller>().Velocity.x;
            }
            else
            {
                xSpeed = collisionObject.GetComponentsInParent<LevelScroller>()[0].Velocity.x;
            }
            
            mPlayerRigidBody.MovePosition(mPlayerObject.transform.position + Vector3.right * xSpeed * Time.deltaTime);
        }
    }

    public void GroundCollisionExit(Collision collisionInfo)
    {
        mIsGrounded = false;

        //used to fix a gravity glitch when exiting the ground, or platform box colliders
        mPlayerRigidBody.velocity += new Vector3(0.0f, -0.1f, 0.0f);
        mPlayerRigidBody.useGravity = true;
    }
    #endregion

    #region Booleans Getters/setters
    public bool IsAgainstLeftBoundary
    {
        get { return mIsAgainstLeftBoundary; }
        set { mIsAgainstLeftBoundary = value; }
    }

    public bool IsBursting
    {
        get { return mIsBursting; }
        set { mIsBursting = value; }
    }
    #endregion

    public MonoBehaviour MyMonoBehaviour
    {
        get { return mMonoBehaviour; }
        set { mMonoBehaviour = value; }
    }

    private PlayerMovement()
    {
        mPlayerObject = GameObject.Find("Player");
        mPlayerRigidBody = mPlayerObject.GetComponent<Rigidbody>();
        Instantiate(mPlayerObject);
    }

    private static PlayerMovement instance;
    private bool mParticlesEnabled = false;

    //Boundary Booleans
    private bool mIsAgainstLeftBoundary = false;

    //Jump variables
    private Vector3 mSquareJumpVelocity = new Vector3(0.0f, 9.5f, 0.0f);
    private Vector3 mRectangleJumpVelocity = new Vector3(0.0f, 5.0f, 0.0f);
    private bool mIsGrounded = false;
    private bool mAllowedDoubleJump = true;

    //Drop variables
    private Vector3 mDropVelocity = new Vector3(0.0f, -15.0f, 0.0f);

    //Burst variables
    private Vector3 mBurstVelocity = new Vector3(20.0f, 0.0f, 0.0f);
    private Vector3 mHorizontalForce = new Vector3(-22.0f, 0.0f, 0.0f);
    private bool mIsBursting = false;

    //MonoBehavior Instance
    private MonoBehaviour mMonoBehaviour;

}
