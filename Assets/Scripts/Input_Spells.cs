using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Input_Spells : MonoBehaviour
{
    public Image handTime;
    public Image waterTime;
    public Image barrierTime;

    public GameObject Hand;
    public GameObject Water;
    public GameObject Barrier;

    public GameObject SummoningMenu;
    public GameObject handCanvas;
    public GameObject waterCanvas;
    public GameObject barrierCanvas;

    private Spells spells;
    private MainManager manager;


    private void Awake()
    {
        spells = new Spells();
        manager = FindAnyObjectByType<MainManager>();
    }

    private void OnEnable()
    {
        spells.SpellCast.Enable();

        spells.SpellCast.Hand.performed += Hand_performed;
        spells.SpellCast.Water.performed += Water_performed;
        spells.SpellCast.Barrier.performed += Barrier_performed;
    }

    private void OnDisable()
    {
        spells.SpellCast.Disable();

        spells.SpellCast.Hand.performed -= Hand_performed;
        spells.SpellCast.Water.performed -= Water_performed;
        spells.SpellCast.Barrier.performed -= Barrier_performed;
    }

    private void Hand_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (manager.hasStones && manager.inHandZone && !handTime.gameObject.activeSelf)
        {
            SummoningMenu.gameObject.SetActive(true);
            handCanvas.gameObject.SetActive(true);
            handTime.gameObject.SetActive(true);
            Hand.gameObject.SetActive(true);
        }
    }
    private void Water_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (manager.hasStones && manager.inWaterZone && !waterTime.gameObject.activeSelf )
        {
            SummoningMenu.gameObject.SetActive(true);
            waterCanvas.gameObject.SetActive(true);
            waterTime.gameObject.SetActive(true);
            Water.gameObject.SetActive(true);
            
        }
    }
    private void Barrier_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (manager.hasStones && manager.inBarrierZone && !barrierTime.gameObject.activeSelf)
        {
            SummoningMenu.gameObject.SetActive(true);
            barrierCanvas.gameObject.SetActive(true);
            barrierTime.gameObject.SetActive(true);
            Barrier.gameObject.SetActive(true);
        }
    }
}
