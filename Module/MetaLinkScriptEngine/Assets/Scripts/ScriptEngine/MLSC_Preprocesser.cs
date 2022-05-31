using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLSC.Source;

namespace MLSC
{
    public class MLSC_Preprocesser
    {
        private bool isEmptyLine(string p_Line)
        {
            foreach (var l_ch in p_Line)
                if (!char.IsWhiteSpace(l_ch))
                    return false;
            return true;
        }

        private bool isCommentLine(string p_Line)
        {
            return p_Line[0] == '#';
        }

        // 공백 라인과 주석 라인 삭제 및 trim 처리
        public void Preprocessing(MLSC_Source p_Source)
        {
            List<int> l_RemoveLine = new List<int>();
            for (int l_index = 0; l_index < p_Source.Count; l_index++)
            {
                if (isEmptyLine(p_Source[l_index]))
                {
                    l_RemoveLine.Add(l_index);
                    continue;
                }

                p_Source.TrimAt(l_index);

                if (isCommentLine(p_Source[l_index]))
                    l_RemoveLine.Add(l_index);
            }

            l_RemoveLine.Reverse();
            foreach (var l_index in l_RemoveLine)
            {
                p_Source.RemoveAt(l_index);
            }
        }
    }
}