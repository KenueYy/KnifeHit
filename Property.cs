using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Property : MonoBehaviour
{
    public static Property instance;
    public int COINS_PER_HIT_LOG;
    public int COINS_PER_HIT_APPLE;
    public Property()
    {
        instance = this;
    }
}
