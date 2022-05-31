using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLSC.Source
{
    public class MLSC_SourceFile : MLSC_Source
    {
        public string m_FilePath;

        public override void MakeSource()
        {
            if (m_isMakeSource)
                return;

            if (m_FilePath.Contains("./"))
                m_FilePath = m_FilePath.Replace("./", Application.dataPath + "/");

            if (!m_FilePath.Contains(".src"))
                m_FilePath += ".src";

            if (base.m_Sources == null)
                base.m_Sources = new List<Line>();

            try
            {
                string[] l_source = System.IO.File.ReadAllLines(m_FilePath);

                for (int l_lineNum = 0; l_lineNum < l_source.Length; l_lineNum++)
                    base.m_Sources.Add(new Line(l_lineNum, l_source[l_lineNum]));

                m_isMakeSource = true;
            }
            catch (System.Exception)
            {
                Debug.LogError("파일 열기 실패");
            }
        }

        private void Awake()
        {
            MakeSource();
        }
    }
}

