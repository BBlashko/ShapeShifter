using UnityEngine;

public class PlayerShape {

    public enum Shape { RECTANGLE, SQUARE, TRIANGLE, DEATH }
    public Shape CurrentShape;

    public void Instantiate(GameObject player)
    {
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

    public void Spawn()
    {
        //TODO: Add ability to respawn player at a default location
    }

    public void Death()
    {
        if (CurrentShape != Shape.DEATH)
        {
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
                break;
            default:
                break;
        }
    }

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
   
}
