using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MLSC.Source;
using DesignPattern;
using DataStruct;

namespace MLSC
{
    public class MLSC_ScriptEngine : DesignPattern_Singleton<MLSC_ScriptEngine>
    {
        [Header("각 인터프리터 처리계")]
        [SerializeField] private MLSC_Preprocesser m_Preprocesser;
        [SerializeField] private MLSC_Scanner m_Scanner;
        [SerializeField] private MLSC_Parser m_Parser;
        [SerializeField] private MLSC_Storage m_Storage;

        [Header("소스 프로그램")]
        public MLSC_Source[] m_Sources;
        public List<MLSC_Token> m_Tokens;
        public List<MLSC_Code> m_Codes;

        protected override void Awake()
        {
            base.Awake(this);

            m_Preprocesser = new MLSC_Preprocesser();
            m_Scanner = new MLSC_Scanner();
            m_Parser = new MLSC_Parser();
            m_Storage = new MLSC_Storage();

            m_Sources = Resources.FindObjectsOfTypeAll<MLSC_Source>();

            try
            {
                foreach (MLSC_Source source in m_Sources)
                {
                    source.MakeSource();

                    m_Preprocesser.Preprocessing(source);
                    m_Scanner.Scanning(source);
                    m_Parser.ConvertToCode(source);
                }
            }
            catch (ProgramExitHandle)
            {
                Debug.Log("프로그램 정상 종료");
            }
        }

        public static void Error_Msg(string txt) => MLSC_ErrorHandler.Error_Msg(txt);
        public static MLSC_Storage Storage => Instance.m_Storage;
    }
}
