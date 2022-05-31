using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLSC.Source
{
    [System.Serializable]
    public struct Line
    {
        public int m_LineNumber;
        public string m_Text;

        public Line(int p_LineNumber, string p_Text)
        {
            this.m_LineNumber = p_LineNumber;
            this.m_Text = p_Text;
        }

        public Line(Line p_Line)
        {
            m_LineNumber = p_Line.m_LineNumber;
            m_Text = p_Line.m_Text;
        }
    }

    public abstract class MLSC_Source : MonoBehaviour
    {
        [SerializeField] protected List<Line> m_Sources;
        [SerializeField] protected bool m_isReading;
        [SerializeField] public List<MLSC_Token> m_Tokens;
        [SerializeField] public List<MLSC_Code> m_Codes;

        public int Count => m_Sources.Count;

        public string this[int m_Index]
        {
            get
            {
                int l_TargetIndex = 0;
                if (m_Index < 0)
                    l_TargetIndex = m_Sources.Count - m_Index;
                else
                    l_TargetIndex = m_Index;

                return m_Sources[l_TargetIndex].m_Text;
            }
        }

        public void RemoveAt(int p_Index)
        {
            m_Sources.RemoveAt(p_Index);
        }

        public void TrimAt(int p_Index)
        {
            var l_data = m_Sources[p_Index];
            l_data.m_Text = l_data.m_Text.Trim();
            m_Sources[p_Index] = l_data;
        }

        public List<Line> At()
        {
            return m_Sources;
        }

        public abstract void MakeSource();
    }
}
