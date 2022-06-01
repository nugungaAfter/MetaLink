using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLSC
{
    public enum MLSC_SymbolKind
    {
        noId, varId, fncId, paraId, ObjId
    }

    public class MLSC_Symbol : MLSC_StorageItem
    {
        public MLSC_Symbol(string p_ItemName, MLSC_StorageTable p_Table)
            : base(p_ItemName, p_Table)
        {

        }

        public void Clear()
        {

        }
    }
}
