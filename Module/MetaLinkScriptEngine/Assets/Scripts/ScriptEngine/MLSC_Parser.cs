using MLSC.Source;

namespace MLSC
{
    public partial class MLSC_Parser
    {
        /// <summary>
        /// ���� �Լ��� ��ġ
        /// </summary>
        private int mainFunc_p;
        /// <summary>
        /// ����� ����
        /// </summary>
        private int BlockCount;
        /// <summary>
        /// �ݺ��� ����
        /// </summary>
        private int LoopCount;
        /// <summary>
        /// �ӽ� �ɺ�
        /// </summary>
        private MLSC_Symbol TempSymbol;
        /// <summary>
        /// ���� ó������ ��ū
        /// </summary>
        private MLSC_Token Token;
        /// <summary>
        /// �ӽ÷� ����� �ִ� �ڵ�
        /// </summary>
        private string BufCode;
        private MLSC_Source m_Source;

        private void init()
        {
            mainFunc_p = -1;
            BlockCount = LoopCount = 0;
            MLSC_ScriptEngine.Storage.FlagSet("FuncDecl", false);
            MLSC_ScriptEngine.Storage.FlagSet("Explicit", false);
        }

        public void ConvertToCode(MLSC_Source p_Source)
        {
            m_Source = p_Source;
            init(); // �ʱ�ȭ

            var tokens = p_Source.m_Tokens;
            for (Token = tokens.Dequeue();
                !tokens.IsEmpty();
                Token = tokens.Dequeue()) // ��ū�� �ϳ��� ������
            {
                // ���࿡ ��ū�� �Լ��� ������Ʈ�� ���
                if(Token.m_Kind == MLSC_TokenKind.A_0x99_Function ||
                   Token.m_Kind == MLSC_TokenKind.A_0x82_Object)
                {
                    set_name(); // �̸��� �����ϰ�
                    enter(TempSymbol, // �ɺ� ���
                          Token.m_Kind == MLSC_TokenKind.A_0x99_Function ? MLSC_SymbolKind.fncId 
                                                                         : MLSC_SymbolKind.ObjId);
                }
            }

            p_Source.RestoreToken();

            push_intercode();
            
            tokens = p_Source.m_Tokens;
            for (Token = tokens.Dequeue();
                 !tokens.IsEmpty();
                 Token = tokens.Dequeue())
            {
                convert();
            }

            // main �Լ��� ������ ���� �Լ� ȣ�� �ڵ带 ���� 
            //set_startPc(1);                                        /* 1����� ���� ����  */
            if (mainFunc_p != -1)
            {
                //set_startPc(MLSC_Storage.m_Code.Count);                        /* main�������� ���� ���� */
                setCode(MLSC_TokenKind.A_0xAB_FunctionCall, mainFunc_p);
                setCode(MLSC_TokenKind.A_0x28_);
                setCode(MLSC_TokenKind.A_0x29_);
                push_intercode();
            }
        }
        void convert() { // ���� Ű���� �м�
            switch (Token.m_Kind)
            {
                case MLSC_TokenKind.A_0x89_Option:      optionSet(); break;  /* �ɼ� ����  */
                case MLSC_TokenKind.A_0x90_Var:         varDecl(); break;    /* ���� ���� */
                case MLSC_TokenKind.A_0x99_Function:    fncDecl(); break;    /* �Լ� ���� */
                case MLSC_TokenKind.A_0x86_FOR:
                    ++LoopCount;
                    convert_block_set(); setCode_End();
                    --LoopCount;
                    break;
                case MLSC_TokenKind.A_0x83_IF:
                    convert_block_set();                                // if
                    while (Token.m_Kind == MLSC_TokenKind.A_0x84_ELIF) { convert_block_set(); } // elif
                    if (Token.m_Kind == MLSC_TokenKind.A_0x85_ELSE) { convert_block_set(); } // else
                    setCode_End();                                      // end
                    break;
                case MLSC_TokenKind.A_0xAA_Break:
                    if (LoopCount <= 0)
                        MLSC_ScriptEngine.Error_Msg("�߸��� break�Դϴ�.");
                    setCode(Token.m_Kind); Token = m_Source.m_Tokens.Dequeue(); convert_rest();
                    break;
                case MLSC_TokenKind.A_0xA9_Return:
                    if (!MLSC_ScriptEngine.Storage.FlagCheck("FuncDecl"))
                        MLSC_ScriptEngine.Error_Msg("�߸��� return�Դϴ�.");
                    setCode(Token.m_Kind); Token = m_Source.m_Tokens.Dequeue(); convert_rest();
                    break;
                case MLSC_TokenKind.A_0x88_Exit:
                    setCode(Token.m_Kind); Token = m_Source.m_Tokens.Dequeue(); convert_rest();
                    break;
                case MLSC_TokenKind.A_0x7D_:
                    MLSC_ScriptEngine.Error_Msg("�߸��� end�Դϴ�.");       /* end�� �ܵ����� ���Ǵ� ���� ���� */
                    break;
                default: convert_rest(); break;
            }
        }
        void convert_block_set() // ��� ����
        {
            int patch_line;
            patch_line = setCode(Token.m_Kind, 0);
            Token = m_Source.m_Tokens.Dequeue();
            convert_rest();
            convert_block();                                            /* ��� ó�� */
            backPatch(patch_line, get_lineNo());        /* NO_FIX_ADRS�� ����(end�� ��ȣ) */
        }
        void convert_block()
        {
            MLSC_TokenKind k;
            ++BlockCount;                                      /* ��� ������ ���� �м� */
            for (k = Token.m_Kind;
                 k != MLSC_TokenKind.A_0x84_ELIF &&
                 k != MLSC_TokenKind.A_0x85_ELSE &&
                 k != MLSC_TokenKind.A_0x7D_ &&
                 !m_Source.m_Tokens.IsEmpty();
                 ) {
                convert();
            }
            --BlockCount;
        }
        void convert_rest() {
            int tblNbr;

            for (; ; )
            {
                if (Token.m_Kind == MLSC_TokenKind.A_0xFE_EOL) break;
                switch (Token.m_Kind)
                {      // �� �� Ű���尡 ���߿� ��Ÿ���� ���� ���� */
                    case MLSC_TokenKind.A_0x83_IF: case MLSC_TokenKind.A_0x84_ELIF: case MLSC_TokenKind.A_0x85_ELSE:
                    case MLSC_TokenKind.A_0x86_FOR: case MLSC_TokenKind.A_0xAA_Break: case MLSC_TokenKind.A_0x99_Function: case MLSC_TokenKind.A_0xA9_Return: case MLSC_TokenKind.A_0x88_Exit:
                    case MLSC_TokenKind.A_0x89_Option: case MLSC_TokenKind.A_0x90_Var: case MLSC_TokenKind.A_0x7D_:
                        MLSC_ScriptEngine.Error_Msg("�߸��� ����Դϴ�: " + Token.m_Text);
                        break;
                    case MLSC_TokenKind.A_0x02_Ident:                                              /* �Լ� ȣ��, ���� */
                        set_name();
                        //if ((tblNbr = searchName(tmpTb.name, 'F')) != -1)
                        //{    /* �Լ� ��� ����*/
                        //    if (tmpTb.name == "main") err_exit("main�Լ��� ȣ���� �� �����ϴ�.");
                        //    setCode(Fcall, tblNbr); continue;
                        //}
                        //if ((tblNbr = searchName(tmpTb.name, 'V')) == -1)
                        //{    /* ���� ��� ����  */
                        //    if (explicit_F) err_exit("���� ������ �ʿ��մϴ�: ", tmpTb.name);
                        //    tblNbr = enter(tmpTb, varId);                      /* �ڵ� ���� ��� */
                        //}
                        //if (is_localName(tmpTb.name, varId)) setCode(Lvar, tblNbr);
                        //else setCode(Gvar, tblNbr);
                        continue;
                    case MLSC_TokenKind.A_0x02_ValueInt: case MLSC_TokenKind.A_0x02_ValueDbl:                         /* ������ double������ ���� */
                        //setCode(Token.m_Kind, set_LITERAL(token.dblVal));
                        break;
                    case MLSC_TokenKind.A_0x02_ValueStr:
                        //setCode(token.kind, set_LITERAL(token.text));
                        break;
                    default:                                                   /* + - <= ��  */
                        setCode(Token.m_Kind);
                        break;
                }
                Token = m_Source.m_Tokens.Dequeue();
            }
            push_intercode();
            Token = m_Source.m_Tokens.Dequeue();
        }
        void optionSet()
        {
            setCode(MLSC_TokenKind.A_0x89_Option);                 /* �� ���� ������̹Ƿ� �ڵ� ��ȯ�� Option�� */
            setCode_rest();                                       /* ������ ������� ���� */
            Token = m_Source.m_Tokens.Dequeue();                  /* �� ���� ������ �����Ѵ� */
            if (Token.m_Kind == MLSC_TokenKind.A_0x02_ValueStr && Token.m_Text == "var")
                MLSC_ScriptEngine.Storage.FlagSet("explicit", true);
            else MLSC_ScriptEngine.Error_Msg("option������ �ٸ��� �ʽ��ϴ�.");
            Token = m_Source.m_Tokens.Dequeue();
            setCode_EofLine();
        }
        void varDecl()
        {
            setCode(MLSC_TokenKind.A_0x90_Var);                  /* �� ���� ������̹Ƿ� �ڵ� ��ȯ��  Var�� */
            setCode_rest();                                 /* �������� ������� ���� */
            for (; ; )
            {
                Token = m_Source.m_Tokens.Dequeue();
                var_namechk(Token);                                         /* �̸� �˻� */
                set_name(); set_aryLen();                          /* �迭�̸� ���� ���� */
                //enter(tmpTb, varId);                            /* �������(�ּҵ� ���) */
                if (Token.m_Kind != MLSC_TokenKind.A_0x2C_) break;                               /* ���� ���� */
            }
            setCode_EofLine();
        }
        void var_namechk(MLSC_Token tk)
        {
            if (tk.m_Kind != MLSC_TokenKind.A_0x02_Ident) 
                MLSC_ScriptEngine.Error_Msg(tk.m_Text + "�ĺ���");
            //if (is_localScope() && tk.text[0] == '$')
            //    err_exit("�Լ� �� var���𿡼��� $�� ���� �̸��� ������ �� �����ϴ�: ", tk.text);
            //if (searchName(tk.text, 'V') != -1)
            //    err_exit("�ĺ��ڰ� �ߺ��Ǿ����ϴ�: ", tk.text);
        }
        void set_name() {
            if (Token.m_Kind != MLSC_TokenKind.A_0x02_Ident)
                MLSC_ScriptEngine.Error_Msg("�ĺ��ڰ� �ʿ��մϴ�: " + Token.m_Text);
            //tmpTb.clear(); tmpTb.name = token.text;                         /* �̸� ���� */
            Token = m_Source.m_Tokens.Dequeue();
        }
        void set_aryLen() {
            //tmpTb.aryLen = 0;
            if (Token.m_Kind != MLSC_TokenKind.A_0x5B_) return;                                /* �迭�� �ƴϴ� */

            Token = m_Source.m_Tokens.Dequeue();
            if (Token.m_Kind != MLSC_TokenKind.A_0x02_ValueInt)
                MLSC_ScriptEngine.Error_Msg("�迭 ���̴� ��(+)�� ������ ������ �ּ���: " + Token.m_Text);
            //tmpTb.aryLen = (int)token.dblVal + 1;   /* var a[5]�� ÷��0~5�� ��ȿ�̹Ƿ� +1 */
            //token = chk_nextTkn(nextTkn(), ']');
            if (Token.m_Kind == MLSC_TokenKind.A_0x5B_) 
                MLSC_ScriptEngine.Error_Msg("������ �迭�� ������ �� �����ϴ�.");
        }
        void fncDecl() { }
        void backPatch(int line, int n) {
            // �ι�° �߰�ȣ ��ġ�� �ִ� ���� �����ؾ���.
            var temp = MLSC_Storage.m_Code[line];
            var temp2 = temp.Substring(temp.IndexOf(',') + 1);
            MLSC_Storage.m_Code[line] = temp2 + n;
        }
        void setCode(MLSC_TokenKind kind) {
            BufCode += (int)kind;
        }
        int setCode(MLSC_TokenKind kind, int addr) {
            setCode(kind);
            BufCode += "," + addr;
            return get_lineNo();
        }
        void setCode_rest() { }
        void setCode_End() { }
        void setCode_EofLine() { }
        void push_intercode() {
            MLSC_Storage.m_Code.Add(BufCode);
            BufCode = "";
        }
        bool is_localScope() => MLSC_ScriptEngine.Storage.FlagCheck("FuncDecl"); 
        int enter(MLSC_Symbol tb, MLSC_SymbolKind kind) { return -1; }
        int get_lineNo() => Token.m_FileLine;
    }
}