using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLSC
{
    public enum MLSC_TokenKind
    {
        A_0x00_Others, // 기타
        A_0x01_Letter, // 영문자
        A_0x02_Digit, // 숫자
        A_0x02_Ident, // 식별자
        A_0x02_ValueInt, // 정수 값
        A_0x02_ValueDbl, // 실수 값
        A_0x02_ValueStr, // 문자 값
        A_0x02_ValueBol, // 논리 값
        A_0x21_ = '!', // 느낌표
        A_0x22_ = '\"', // 큰 따옴표
        A_0x23_ = '#', // 샵
        A_0x24_ = '$', // 달러 기호
        A_0x25_ = '%', // 퍼센트 기호
        A_0x26_ = '&', // 엔퍼센트 기호
        A_0x27_ = '\'', // 작은 따옴표
        A_0x28_ = '(', // 여는 소괄호
        A_0x29_ = ')', // 닫는 소괄호
        A_0x2A_ = '*', // 곱셈 기호
        A_0x2B_ = '+', // 더하기 기호
        A_0x2C_ = ',', // 콤마, 쉼표
        A_0x2D_ = '-', // 대시, 빼기 기호
        A_0x2E_ = '.', // 닷, 마침표
        A_0x2F_ = '/', // 슬래시
        A_0x3A_ = ':', // 콜론
        A_0x3B_ = ';', // 세미콜론
        A_0x3C_ = '<', // 여는 꺽은 괄호
        A_0x3D_ = '=', // 같다 기호
        A_0x3E_ = '>', // 닫는 꺽은 괄호
        A_0x3F_ = '?', // 물음표
        A_0x40_ = '@', // 골뱅이 기호, 이메일 기호
        A_0x5B_ = '[', // 여는 대괄호
        A_0x5C_ = '\\', // 역 슬래시
        A_0x5D_ = ']', // 닫는 대괄호
        A_0x5E_ = '^', // 지붕 뭐시기
        A_0x5F_ = '_', // 언더바, 밑줄
        A_0x60_ = '`', // 글래이브
        A_0x7B_ = '{', // 여는 중괄호
        A_0x7C_ = '|', // 세로줄
        A_0x7D_ = '}', // 닫는 중괄호
        A_0x7E_ = '~', // 물결표
        A_0x80_Public = 130,
        A_0x81_Private,
        A_0x82_Object,
        A_0x83_IF,
        A_0x84_ELIF,
        A_0x85_ELSE,
        A_0x86_FOR,
        A_0x87_IN,
        A_0x88_Exit,
        A_0x89_Option,
        A_0x90_Var,
        A_0x91_Digit,
        A_0x92_String,
        A_0x93_Bool,
        A_0x94_NOT,
        A_0x95_AND,
        A_0x96_OR,
        A_0x97_INC,
        A_0x98_DEC,
        A_0x99_Function,
        A_0xA0_Set,
        A_0xA1_Equal,
        A_0xA2_NotEqual,
        A_0xA3_LessEqual,
        A_0xA4_GreatEqual,
        A_0xA5_PLUSEqual,
        A_0xA6_MINUSEqual,
        A_0xA7_MultiEqual,
        A_0xA8_DIVIEqual,
        A_0xA9_Return,
        A_0xAA_Break,
    }

    [System.Serializable]
    public class MLSC_Token
    {
        public MLSC_TokenKind m_Kind;
        public string m_Text;
        public int m_FileLine;

        public MLSC_Token()
        {
            m_Kind = MLSC_TokenKind.A_0x00_Others;
            m_Text = "";
        }

        public MLSC_Token(MLSC_TokenKind p_Kind, string p_Text, int p_Line)
        {
            m_Kind = p_Kind;
            m_Text = p_Text;
            m_FileLine = p_Line;
        }
    }
}
