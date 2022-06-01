using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MLSC
{
    [Serializable]
    public class ProgramExitHandle : Exception
    {
        public ProgramExitHandle()
        {

        }
    }

    public static class MLSC_ErrorHandler
    {
        public static void Error_Msg(string txt)
        {
            Debug.LogError(txt);
            throw new ProgramExitHandle();
        }
    }
}