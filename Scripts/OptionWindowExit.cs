using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionWindowExit : MonoBehaviour
{
private void optionend()
{
    GManager.instance.OptionIsOn = false;
    this.gameObject.SetActive(false);
}
}
