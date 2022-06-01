using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStruct;

namespace MLSC.Source
{
    [System.Serializable]
    public class Line
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

    public abstract class MLSC_Source : MonoBehaviour, IEnumerable
    {
        [SerializeField] protected bool m_isMakeSource;
        [SerializeField] protected List<Line> m_Sources;
        [SerializeField] public Deque<MLSC_Token> m_Tokens;
        [SerializeField] public Deque<MLSC_Token> m_BackUp;
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
            m_Sources[p_Index].m_Text = m_Sources[p_Index].m_Text.Trim();
        }
        public void CommentRemoveAt(int p_Index)
        {
            int i = m_Sources[p_Index].m_Text.IndexOf('#');
            if(i > 0)
                m_Sources[p_Index].m_Text = m_Sources[p_Index].m_Text[0..i];
        }
        public IEnumerator GetEnumerator()
        {
            return m_Sources.GetEnumerator();
        }
        public void RestoreToken() => m_Tokens = (Deque<MLSC_Token>)m_BackUp.Clone();
        public abstract void MakeSource();
    }
}
