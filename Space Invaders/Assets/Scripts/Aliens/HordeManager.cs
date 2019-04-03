﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Este script hace que als hordas sean cada vez más difíciles
public class HordeManager : MonoBehaviour
{
    [SerializeField] [Tooltip("Cuanto aumenta la velocida horizontal de la horda siguiente al matar a la anterior")] float speedIncrement = 10.0f;
    [SerializeField] [Tooltip("Cuanto aumenta la velocida vertical de la horda siguiente al matar a la anterior")] float downSpeedIncrement = 0.25f;

    [SerializeField] [Tooltip("Cuanto aumenta la vida de los aliens al matar a una horda completa")] int hpIncrement = 1;
    [SerializeField] [Tooltip("Cuanto aumenta la velocidad de disparo de los aliens ")]float shotSpeedIncrement = 0.5f;
    [SerializeField] [Tooltip("Cuanto aumenta el valor en puntos de los aliens")]float alienScoreValueIncrement = 10.0f;

    [SerializeField] [Tooltip("Cuantas vidas extras se le dan al jugador entre horda y horda")]int extraPlayerLives = 1;
    [SerializeField] [Tooltip("Cuantas vidas sele dan a las barreras entre horda y horda")] int extraBarriersHP = 2;

    [SerializeField] AlienMiniBoss alienMiniBoss;

    static Horde horde;
    PlayerMov player;
    [SerializeField] Text waveNumberUI;
    [SerializeField] Text playerLivesUI;

    [HideInInspector] public int waveNumber = 1;

    Barrier [] barriers;

    void Start()
    {
        horde = FindObjectOfType<Horde>();
        player = FindObjectOfType<PlayerMov>();
        barriers = FindObjectsOfType<Barrier>();
    }

    public void increaseHordeStats()
    {
        waveNumber++;
        waveNumberUI.text = "Oleada    " + waveNumber;

        //Se ajustan las propiedades de la nueva horda según en que oleada esté el jugador
        horde.speed += speedIncrement * waveNumber;
        horde.downSpeed += downSpeedIncrement * waveNumber;

        horde.alienShotSpeed += shotSpeedIncrement * waveNumber;
        horde.alienScoreValue += alienScoreValueIncrement * waveNumber;

        //A partir de la oleada 3 ni el jugador ni los aliens reciben puntos de vida extra
        if (waveNumber <= 3)
        {
            player.HP += extraPlayerLives;
            horde.alienHP += hpIncrement * waveNumber;
        }

        if (waveNumber >= 2) alienMiniBoss.gameObject.SetActive(true);

        //Se restauran las barreras y se les modifican los puntos de vida según el número de oleada
        foreach(Barrier barrier in barriers)
        {
            barrier.restore();
            barrier.findHorde();
            barrier.HP += extraBarriersHP * waveNumber;
        }
    }

    private void Update()
    {
        playerLivesUI.text =  player.HP + "    Vidas";
    }
}