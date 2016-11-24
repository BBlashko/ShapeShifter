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
        ////Player burst checks
        if (mPlayerRigidBody.velocity.x <= 0.5f && mIsBursting)
        {
            mIsBursting = false;
            mPlayerRigidBody.useGravity = true;
        }
        else if (mIsBursting)
        {
            mPlayerRigidBody.AddForce(mHorizontalForce);
        }
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
        if (!mIsAgainstLeftBoundary && !mIsBursting)
        {
            GameObject collisionObject = collisionInfo.gameObject;
            float xSpeed = collisionObject.GetComponentInParent<GroundScroller>().GetVelocity().x;
            mPlayerRigidBody.MovePosition(mPlayerObject.transform.position + Vector3.right * xSpeed * Time.fixedDeltaTime);
        }
        if (mIsFalling)
        {
            mIsFalling = false;
        }
    }
    #endregion

    #region Booleans Getters/setters
    public bool IsAgainstLeftBoundary
    {
        get { return mIsAgainstLeftBoundary; }
        set { mIsAgainstLeftBoundary = value; }
    }

    public bool IsFalling
    {
        get { return mIsFalling; }
        set { mIsFalling = value; }
    }

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
