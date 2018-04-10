﻿using UnityEngine;
using goedle_sdk;

public class ObjectInteraction : MonoBehaviour
{

    private bool inTrigger = false;
    private Renderer rend;

    [SerializeField] private Canvas interactionCanvas;
    [SerializeField] private Canvas crosshair;

    [SerializeField] private Material defaultMat; //used to store the default and the outiline materials.
    [SerializeField] private Material outlinedMat;

    private enum Interactables {naming, construction,exit}
    [SerializeField] private Interactables interactable;

    public SceneLoader sceneLoader;
    public CursorLock cursor;
    public AudioClip clip;
    public string sceneToLoad;


    // Use this for initialization
    void Start()
    {
        interactionCanvas.enabled = false;
        //apply the default material
        rend = GetComponent<Renderer>();
        rend.material = defaultMat;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inTrigger == true)
        {
            InteractWithObject();
        }
    }


    private void InteractWithObject()
    {
        if (interactable == Interactables.naming)
        {
            GameManager.currentLevel = GameManager.Levels.moleculeNaming;
            cursor.UnLockCursor();
            GoedleAnalytics.instance.track("choose.quiz",sceneToLoad);
            MoleculeManager.instance.buildStrategyNamingQueue(MoleculeManager.instance._naming_strategy);
            sceneLoader.LoadScene(sceneToLoad);
        }
        else if (interactable == Interactables.construction)
        {
            GameManager.currentLevel = GameManager.Levels.moleculeConstruction;
            cursor.UnLockCursor();
            GoedleAnalytics.instance.track("choose.quiz", sceneToLoad);
            MoleculeManager.instance.buildStrategyConstructionQueue(MoleculeManager.instance._construction_strategy);
            sceneLoader.LoadScene(sceneToLoad);
        }
        else if (interactable == Interactables.exit)
        {
            GameManager.instance.ExitSimulation();
            cursor.UnLockCursor();
            sceneLoader.LoadScene(sceneToLoad);
        }
        SoundManager.instance.PlaySingle(clip);
    }

    #region trigger with player

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GoedleAnalytics.instance.track("play");

            rend.material = outlinedMat;
            interactionCanvas.enabled = true;
            inTrigger = true;
            crosshair.GetComponent<Crosshair>().DisableCrosshair();

        }
    }

    public void OnTriggerExit(Collider other)
    {
        rend.material = defaultMat;
        interactionCanvas.enabled = false;
        inTrigger = false;
        crosshair.GetComponent<Crosshair>().EnableCrosshair();
    }

    #endregion

}