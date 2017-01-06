using UnityEngine;

public class PlayerShape {

    public enum Shape { RECTANGLE, SQUARE, TRIANGLE, DEATH }
    public Shape CurrentShape;

    public void Instantiate(GameObject player)
    {
        mPlayerInitialPosition = player.transform.position;

        //Gameobjects
        mSquareShape = player.transform.FindChild(mSquareObjectName).gameObject;
        mTriangleShape = player.transform.FindChild(mTriangleObjectName).gameObject;
        mRectangleShape = player.transform.FindChild(mRectangleObjectName).gameObject;

        //Particle Systems
        mPlayerDeathParticles = player.transform.FindChild(mDeathParticlesName).GetComponent<ParticleSystem>();

        //Boundaries
        mLeftBoundary = GameObject.Find(mLeftBoundaryName);
    }

    public void ChangeShape(Shape shape)
    {
        if (CurrentShape != shape && CurrentShape != Shape.DEATH)
        {
            DisableCurrentShape();
            switch (shape)
            {
                case Shape.SQUARE:
                    mSquareShape.SetActive(true);
                    break;
                case Shape.RECTANGLE:
                    mLeftBoundary.transform.position = new Vector3(-6.117f,
                                                                   mLeftBoundary.transform.position.y,
                                                                   mLeftBoundary.transform.position.z);
                    mRectangleShape.SetActive(true);
                    break;
                case Shape.TRIANGLE:
                    mTriangleShape.SetActive(true);
                    break;
                default:
                    break;
            }
            CurrentShape = shape;
        }
    }

    public void EnableParticles()
    {
        mSquareShape.GetComponentInChildren<ParticleSystem>().Play();
        mRectangleShape.GetComponentInChildren<ParticleSystem>().Play();
        mTriangleShape.GetComponentInChildren<ParticleSystem>().Play();
    }

    public void DisableParticles()
    {
        mSquareShape.GetComponentInChildren<ParticleSystem>().Stop();
        mRectangleShape.GetComponentInChildren<ParticleSystem>().Stop();
        mTriangleShape.GetComponentInChildren<ParticleSystem>().Stop();
    }

    public void Respawn()
    {
        mPlayerObject.transform.position = mPlayerInitialPosition;
        DisableCurrentShape();
        mSquareShape.SetActive(true);
        CurrentShape = Shape.SQUARE;
        mPlayerRigidBody.isKinematic = false;
    }

    public void InstantDeath()
    {
        Death(false);
    }

    public void CheckDeath(Shape shape)
    {
        Death(false, shape);
    }

    private void Death(bool checkShape, Shape shape = Shape.SQUARE)
    {
        if (CurrentShape != Shape.DEATH)
        {
            if (checkShape && CurrentShape == shape)
            {
                Debug.Log("Currentshape = " + CurrentShape + " Shape = " + shape);
                return;
            }

            mPlayerRigidBody.isKinematic = true;
            DisableCurrentShape();

            //Default is Square Shape Material
            Material material = mSquareShape.transform.GetComponent<MeshRenderer>().material;
            switch (CurrentShape)
            {
                case Shape.RECTANGLE:
                    material = mRectangleShape.transform.GetComponent<MeshRenderer>().material;
                    break;
                case Shape.TRIANGLE:
                    material = mTriangleShape.transform.GetComponentInChildren<MeshRenderer>().material;
                    break;
                default:
                    break;
            }

            CurrentShape = Shape.DEATH;
            mPlayerDeathParticles.GetComponent<ParticleSystemRenderer>().material = material;
            mPlayerDeathParticles.Play();

            GamePlayManager.Instance.GameOver();
        }
    }

    private void DisableCurrentShape()
    {
        switch (CurrentShape)
        {
            case Shape.SQUARE:
                mSquareShape.SetActive(false);
                break;
            case Shape.RECTANGLE:
                mLeftBoundary.transform.position = new Vector3(-5.5f,
                                                                  mLeftBoundary.transform.position.y,
                                                                  mLeftBoundary.transform.position.z);
                mRectangleShape.SetActive(false);
                break;
            case Shape.TRIANGLE:
                mTriangleShape.SetActive(false);
                break;
            case Shape.DEATH:
                mPlayerDeathParticles.Stop();
                mPlayerDeathParticles.Clear();
                break;
            default:
                break;
        }
    }

    //Player Objects
    public GameObject mPlayerObject;
    public Rigidbody mPlayerRigidBody;

    //Prefabs
    private GameObject mSquareShape;
    private GameObject mTriangleShape;
    private GameObject mRectangleShape;

    //Death Particles
    private ParticleSystem mPlayerDeathParticles;

    //Resource Path Strings
    private string mSquareObjectName = "SquareMesh";
    private string mTriangleObjectName = "TriangleMesh";
    private string mRectangleObjectName = "RectangleMesh";
    private string mDeathParticlesName = "DeathParticles";

    //LeftBoundary Gameobject
    //Needed for when the shape is changed to a rectangle
    private GameObject mLeftBoundary;
    private const string mLeftBoundaryName = "LeftBoundary";

    //Reset Player
    private Vector3 mPlayerInitialPosition;
}
