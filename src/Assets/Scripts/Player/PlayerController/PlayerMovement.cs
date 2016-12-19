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
        if (mPlayerObject.transform.position.y < -5.2f)
        {
            InstantDeath();
        }

        //Check if player is falling
        if (mPlayerRigidBody.velocity.y != 0 && !mIsFalling)
        {
            mIsFalling = true;
        }
        else if (mPlayerRigidBody.velocity.y == 0 && mIsFalling)
        {
            mIsFalling = false;
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
        
        if (!mIsFalling && !mParticlesEnabled)
        {
            EnableParticles();
            mParticlesEnabled = true;
        }
        else if (mIsFalling && mParticlesEnabled)
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
        if (!mIsFalling || mAllowedDoubleJump)
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
            if (mIsFalling)
            {
                mAllowedDoubleJump = false;
            }
            mIsFalling = true;
        }
    }

    //Triangular right "Burst" acceleration
    //TODO: Consider changing burst action to encorporate camera.
    public void Burst()
    {
        if (mIsAgainstLeftBoundary)
        {
            ChangeShape(Shape.TRIANGLE);
            mIsBursting = true;
            mPlayerRigidBody.useGravity = false;
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
    public void GroundCollision(Collision collisionInfo)
    {
        if (!mAllowedDoubleJump)
        {
            mAllowedDoubleJump = true;
        }
        if (mIsFalling)
        {
            mIsFalling = false;
        }

        //Moving players finished burst towards the left boundary
        if (!mIsAgainstLeftBoundary && !mIsBursting)
        {
            GameObject collisionObject = collisionInfo.gameObject;

            float xSpeed;

            GroundScroller gs;
            if ((gs = collisionObject.GetComponentInParent<GroundScroller>()) != null)
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
    #endregion

    #region Booleans Getters/setters
    public bool IsAgainstLeftBoundary
    {
        get { return mIsAgainstLeftBoundary; }
        set { mIsAgainstLeftBoundary = value; }
    }

    //public bool IsFalling
    //{
    //    get { return mIsFalling; }
    //    set { mIsFalling = value; }
    //}

    public bool AllowedDoubleJump
    {
        get { return mAllowedDoubleJump; }
        set { mAllowedDoubleJump = value; }
    }

    public bool IsBursting
    {
        get { return mIsBursting; }
        set { mIsBursting = value; }
    }
    #endregion

    private PlayerMovement()
    {
        mPlayerObject = GameObject.Find("Player");
        mPlayerRigidBody = mPlayerObject.GetComponent<Rigidbody>();
        Instantiate(mPlayerObject);
    }
    
    private static PlayerMovement instance;
    private GameObject mPlayerObject;
    private Rigidbody mPlayerRigidBody;
    private bool mParticlesEnabled = false;
    //Boundary Booleans
    private bool mIsAgainstLeftBoundary = false;

    //Jump variables
    private Vector3 mSquareJumpVelocity = new Vector3(0.0f, 9.5f, 0.0f);
    private Vector3 mRectangleJumpVelocity = new Vector3(0.0f, 5.0f, 0.0f);
    private bool mIsFalling = false;
    private bool mAllowedDoubleJump = true;

    //Drop variables
    private Vector3 mDropVelocity = new Vector3(0.0f, -15.0f, 0.0f);

    //Burst variables
    private Vector3 mBurstVelocity = new Vector3(20.0f, 0.0f, 0.0f);
    private Vector3 mHorizontalForce = new Vector3(-22.0f, 0.0f, 0.0f);
    private bool mIsBursting = false;

}
