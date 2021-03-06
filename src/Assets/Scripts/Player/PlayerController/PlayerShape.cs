﻿using System.Collections;
using UnityEngine;

public class PlayerShape {

    public enum Shape { RECTANGLE, SQUARE, TRIANGLE, DEATH }
    public Shape CurrentShape;

    public void Instantiate(GameObject player)
    {
        mPlayerInitialPosition = player.transform.position;

        //Gameobjects
        mSquareShape = player.transform.Find(mSquareObjectName).gameObject;
        mTriangleShape = player.transform.Find(mTriangleObjectName).gameObject;
        mRectangleShape = player.transform.Find(mRectangleObjectName).gameObject;

        //Particle Systems
        mPlayerDeathParticles = player.transform.Find(mDeathParticlesName).GetComponent<ParticleSystem>();

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
        Respawn(mPlayerInitialPosition);
    }

    public void Respawn(Vector3 playerPosition)
    {
        mPlayerObject.transform.position = playerPosition;
        DisableCurrentShape();
        mSquareShape.SetActive(true);
        CurrentShape = Shape.SQUARE;
        mPlayerRigidBody.isKinematic = false;
    }

    public void InstantDeath()
    {
        Death(CurrentShape);
    }

    public void CheckDeath(Shape shape)
    {
        Death(shape);
    }

    public Shape GetCurrentShape()
    {
        return CurrentShape;
    }

    private void Death(Shape shape, bool checkShape = false)
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

            if (GamePlayManager.Instance.GetCurrentGame() != null && GamePlayManager.Instance.GetCurrentGame().LevelId == 0)
            {
                Debug.Log("Player died during tutorial");
                PlayerMovement.Instance.MyMonoBehaviour.StartCoroutine(PlayDeathAnimationAndRespawnPlayer());
            }
            else if (GamePlayManager.Instance.GetCurrentGame() != null)
            {
                GamePlayManager.Instance.GameOver();
            }
        }
    }

    private IEnumerator PlayDeathAnimationAndRespawnPlayer()
    {
        GamePlayManager.Instance.PauseGame();
        yield return new WaitForSeconds(2.0f);
        GamePlayManager.Instance.ResumeGame();
        GamePlayManager.Instance.TutorialRespawnPlayer();
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
