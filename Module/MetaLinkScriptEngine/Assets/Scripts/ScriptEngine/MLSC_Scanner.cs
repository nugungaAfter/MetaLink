using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLSC.Source;
using MLSC.Scanner;

namespace MLSC.Scanner
{
    public struct MLSC_Keyword
    {
        public MLSC_TokenKind Kind;
        public string Text;

        public MLSC_Keyword(MLSC_TokenKind p_Kind, string p_Text)
        {
            this.Text = p_Text;
            this.Kind = p_Kind;
        }
    }
    public static class MLSC_ScannerExtendClass
    {
        public static MLSC_Keyword Search(this MLSC_Keyword[] ary, string text)
        {
            foreach (var keyword in ary)
                if (keyword.Text == text)
                    return keyword;
            return new MLSC_Keyword(MLSC_TokenKind.A_0x00_Others, "");
        }

        public static bool isLetter(this char p_Ch)
            => char.IsLetter(p_Ch);

        public static bool isDigit(this char p_Ch)
            => char.IsDigit(p_Ch);

        public static bool isSpace(this char p_Ch)
            => char.IsWhiteSpace(p_Ch);

        public static bool isOperator(this char p_Ch)
        {
            bool isOperator1 = p_Ch >= 33 && p_Ch <= 47;
            bool isOperator2 = p_Ch >= 58 && p_Ch <= 64;
            bool isOperator3 = p_Ch >= 91 && p_Ch <= 96;
            bool isOperator4 = p_Ch >= 123 && p_Ch <= 126;
            return isOperator1 || isOperator2 || isOperator3 || isOperator4;
        }

        public static bool isOperator2(this string p_Operator)
        {
            if (p_Operator.Length != 2)
                return false;
            return
                new List<string>{ "++", "--", "+=", "==", "!=", "<=", ">=", "+=","-=","*=","/=",":="}.Contains(p_Operator);
        }
    }
}

namespace MLSC
{
    public class MLSC_Scanner
    {
        private MLSC_Keyword[] m_KeywordTable = {
            new MLSC_Keyword(MLSC_TokenKind.A_0x21_, "!"),                  new MLSC_Keyword(MLSC_TokenKind.A_0x22_, "\""),
            new MLSC_Keyword(MLSC_TokenKind.A_0x23_, "#"),                  new MLSC_Keyword(MLSC_TokenKind.A_0x24_, "$"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x25_, "%"),                  new MLSC_Keyword(MLSC_TokenKind.A_0x26_, "&"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x27_, "\'"),                 new MLSC_Keyword(MLSC_TokenKind.A_0x28_, "("),
            new MLSC_Keyword(MLSC_TokenKind.A_0x29_, ")"),                  new MLSC_Keyword(MLSC_TokenKind.A_0x2A_, "*"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x2B_, "+"),                  new MLSC_Keyword(MLSC_TokenKind.A_0x2C_, ","),
            new MLSC_Keyword(MLSC_TokenKind.A_0x2D_, "-"),                  new MLSC_Keyword(MLSC_TokenKind.A_0x2E_, "."),
            new MLSC_Keyword(MLSC_TokenKind.A_0x2F_, "/"),                  new MLSC_Keyword(MLSC_TokenKind.A_0x3A_, ":"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x3B_, ";"),                  new MLSC_Keyword(MLSC_TokenKind.A_0x3C_, "<"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x3D_, "="),                  new MLSC_Keyword(MLSC_TokenKind.A_0x3E_, ">"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x3F_, "?"),                  new MLSC_Keyword(MLSC_TokenKind.A_0x40_, "@"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x5B_, "["),                  new MLSC_Keyword(MLSC_TokenKind.A_0x5C_, "\\"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x5D_, "]"),                  new MLSC_Keyword(MLSC_TokenKind.A_0x5E_, "^"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x5F_, "_"),                  new MLSC_Keyword(MLSC_TokenKind.A_0x60_, "`"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x7B_, "{"),                  new MLSC_Keyword(MLSC_TokenKind.A_0x7C_, "|"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x7D_, "}"),                  new MLSC_Keyword(MLSC_TokenKind.A_0x7E_, "~"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x80_Public, "Public"),       new MLSC_Keyword(MLSC_TokenKind.A_0x81_Private, "Private"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x82_Object, "Object"),       new MLSC_Keyword(MLSC_TokenKind.A_0x83_IF, "if"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x84_ELIF, "elif"),           new MLSC_Keyword(MLSC_TokenKind.A_0x85_ELSE, "else"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x86_FOR, "for"),             new MLSC_Keyword(MLSC_TokenKind.A_0x87_IN, "in"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x88_Exit, "exit"),           new MLSC_Keyword(MLSC_TokenKind.A_0x89_Option, "option"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x90_Var, "var"),             new MLSC_Keyword(MLSC_TokenKind.A_0x91_Digit, "digit"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x92_String, "string"),       new MLSC_Keyword(MLSC_TokenKind.A_0x93_Bool, "bool"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x94_NOT, "not"),             new MLSC_Keyword(MLSC_TokenKind.A_0x95_AND, "and"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x96_OR, "or"),               new MLSC_Keyword(MLSC_TokenKind.A_0x97_INC, "++"),
            new MLSC_Keyword(MLSC_TokenKind.A_0x98_DEC, "--"),              new MLSC_Keyword(MLSC_TokenKind.A_0x99_Function, "function"),
            new MLSC_Keyword(MLSC_TokenKind.A_0xA0_Set, "="),               new MLSC_Keyword(MLSC_TokenKind.A_0xA0_Set, ":="),
            new MLSC_Keyword(MLSC_TokenKind.A_0xA1_Equal, "=="),            new MLSC_Keyword(MLSC_TokenKind.A_0xA2_NotEqual, "!="),
            new MLSC_Keyword(MLSC_TokenKind.A_0xA3_LessEqual, "<="),        new MLSC_Keyword(MLSC_TokenKind.A_0xA4_GreatEqual, ">="),
            new MLSC_Keyword(MLSC_TokenKind.A_0xA5_PLUSEqual, "+="),        new MLSC_Keyword(MLSC_TokenKind.A_0xA6_MINUSEqual, "-="),
            new MLSC_Keyword(MLSC_TokenKind.A_0xA7_MultiEqual, "*="),       new MLSC_Keyword(MLSC_TokenKind.A_0xA8_DIVIEqual, "/="),
            new MLSC_Keyword(MLSC_TokenKind.A_0xA9_Return, "return"),       new MLSC_Keyword(MLSC_TokenKind.A_0xAA_Break, "break"),
        };

        private MLSC_TokenKind[] m_CharKeyWordTable = new MLSC_TokenKind[256];

        private int m_LineNum;
        private int m_LinePtr;

        private char? NextChar(MLSC_Source p_Source)
        {
            int l_LineNumLinmit = p_Source.Count;
            if (m_LineNum >= l_LineNumLinmit)
                return null;

            int l_LineLimit = p_Source[m_LineNum].Length;
            if (m_LinePtr == l_LineLimit)
            {
                m_LinePtr++;
                return '\n';
            }
            else if (m_LinePtr > l_LineLimit)
            {
                m_LineNum++;
                m_LinePtr = 0;
                if (m_LineNum >= l_LineNumLinmit)
                    return null;
            }

            return p_Source[m_LineNum][m_LinePtr++];
        }

        // 토큰의 추출
        public void Scanning(MLSC_Source p_Source)
        {
            m_LineNum = 0;
            m_LinePtr = 0;
            p_Source.m_Tokens ??= new List<MLSC_Token>();

            for (char? l_ch = NextChar(p_Source); l_ch != null; l_ch = NextChar(p_Source))
            {
                if (l_ch.Value.isSpace())
                    continue;

                string l_buf = ""; MLSC_TokenKind l_TokenKind;
                if (l_ch.Value.isDigit()) // 정수
                {
                    for (; l_ch != null && l_ch.Value.isDigit(); l_ch = NextChar(p_Source))
                        l_buf += l_ch.Value;

                    if (l_ch == '.')
                    {
                        for (; l_ch != null && (l_buf == "." || l_ch.Value.isDigit()); l_ch = NextChar(p_Source))
                            l_buf += l_ch.Value;
                        l_TokenKind = MLSC_TokenKind.A_0x02_ValueDbl;
                    }
                    else
                        l_TokenKind = MLSC_TokenKind.A_0x02_ValueInt;
                }
                else if (l_ch == '\'' || l_ch == '\"') // 문자열
                {
                    char l_endCh = l_ch.Value; l_ch = NextChar(p_Source);
                    for (; l_ch != null && l_ch.Value != l_endCh; l_ch = NextChar(p_Source))
                        l_buf += l_ch;
                    NextChar(p_Source); l_TokenKind = MLSC_TokenKind.A_0x02_ValueStr;
                }
                else if (l_ch.Value.isOperator()) // 연산자
                {
                    l_buf += l_ch.Value; char? l_operator2 = NextChar(p_Source);
                    if (l_operator2.Value.isOperator())
                    {
                        if ((l_buf + l_operator2).isOperator2())
                        {
                            l_TokenKind = m_KeywordTable.Search(l_buf + l_operator2).Kind;
                            l_buf += l_operator2;
                        }
                        else
                        {
                            p_Source.m_Tokens.Add(new MLSC_Token(m_KeywordTable.Search(l_buf).Kind, l_buf, m_LineNum));
                            l_buf = l_operator2.ToString(); l_TokenKind = m_KeywordTable.Search(l_operator2.ToString()).Kind;
                        }
                        NextChar(p_Source);
                    }
                    else
                        l_TokenKind = m_KeywordTable.Search(l_buf).Kind;
                }
                else // 식별자
                {
                    for (; l_ch != null && !(l_ch.Value.isOperator() || l_ch.Value.isSpace()); l_ch = NextChar(p_Source))
                        l_buf += l_ch;
                    MLSC_TokenKind l_Searchkind = m_KeywordTable.Search(l_buf).Kind;
                    l_TokenKind = l_Searchkind == MLSC_TokenKind.A_0x00_Others ? MLSC_TokenKind.A_0x02_Ident : l_Searchkind;
                }
                m_LinePtr--;
                p_Source.m_Tokens.Add(new MLSC_Token(l_TokenKind, l_buf, m_LineNum));
            }
        }
    }
}
