﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Parte del objeto game manager, este script cambia el comportamiento del juego
 * según si el jugador es mayor de 13 años (puede disparar) o no.
 */

public class AdultManager : MonoBehaviour
{
    public static bool adult; //Por defecto eres mayor de 13, este valor cambia con la interfaz del menu principal

    void Start()
    {
        //Transmite si eres mayor de 13 o no a la horda de aliens y al jugador para cambiar su comportamiento
        FindObjectOfType<PlayerMov>().adult = adult;
        FindObjectOfType<Horde>().adult = adult;
    }
}
