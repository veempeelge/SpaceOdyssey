using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateText : MonoBehaviour
{
    public TMP_Text valueTXT;
    public void UpdateTextValue(float _value)
    {
        valueTXT.text = _value.ToString() + " / 5 ";
    }
}