using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MLSC.Source;
using DesignPattern;

namespace MLSC
{
    public class MLSC_ScriptEngine : DesignPattern_Singleton<MLSC_ScriptEngine>
    {
        [Header("각 인터프리터 처리계")]
        [SerializeField] private MLSC_Preprocesser m_Preprocesser;
        [SerializeField] private MLSC_Scanner m_Scanner;
        [SerializeField] private MLSC_Parser m_Parser;

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

            m_Sources = Resources.FindObjectsOfTypeAll<MLSC_Source>();

            foreach (MLSC_Source source in m_Sources)
            {
                source.MakeSource();

                m_Preprocesser.Preprocessing(source);
                m_Scanner.Scanning(source);
                m_Parser.Parsing(source);
                m_Parser.Convert(source);
            }
        }
    }
}
