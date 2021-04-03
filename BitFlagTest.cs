using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitys.BitFlag;

public class BitFlagTest : MonoBehaviour
{
    
    enum Flags
    {
        ONE = 0x1,
        Two = 0x01,
        Three = 0x001,
        Four = 0x0001,
        Five = 0x00001
    }

    private BitFlag<Flags> bitFlag = new BitFlag<Flags>();

    // Start is called before the first frame update
    void Start()
    {
        bitFlag.Set(Flags.Four);

        Debug.Log("HasFlag : " +  bitFlag.HasFlag(Flags.Four));
    }

}
