using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuryController : MonoBehaviour
{
    private float _damage = 20;
    public float GetSetDamage { get { return _damage; } set { _damage = value; } }

    public void SliderUpdate(Slider losingPoints, Slider winningPoints, bool bothLoses)
    {
        if (!bothLoses)
        {
            losingPoints.value -= GetSetDamage;
            winningPoints.value += GetSetDamage;
        }
        else
        {
            losingPoints.value -= GetSetDamage;
            winningPoints.value -= GetSetDamage;
        }        
    }
}
