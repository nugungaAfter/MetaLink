using MLSC.Source;

namespace MLSC
{
    public partial class MLSC_Parser
    {
        /// <summary>
        /// 메인 함수의 위치
        /// </summary>
        private int mainFunc_p;
        /// <summary>
        /// 블록의 깊이
        /// </summary>
        private int BlockCount;
        /// <summary>
        /// 반복문 개수
        /// </summary>
        private int LoopCount;
        /// <summary>
        /// 임시 심볼
        /// </summary>
        private MLSC_Symbol TempSymbol;
        /// <summary>
        /// 현재 처리중인 토큰
        /// </summary>
        private MLSC_Token Token;
        /// <summary>
        /// 임시로 만들고 있는 코드
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
            init(); // 초기화

            var tokens = p_Source.m_Tokens;
            for (Token = tokens.Dequeue();
                !tokens.IsEmpty();
                Token = tokens.Dequeue()) // 토큰을 하나씩 가져옴
            {
                // 만약에 토큰이 함수나 오브젝트인 경우
                if(Token.m_Kind == MLSC_TokenKind.A_0x99_Function ||
                   Token.m_Kind == MLSC_TokenKind.A_0x82_Object)
                {
                    set_name(); // 이름을 동록하고
                    enter(TempSymbol, // 심볼 등록
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

            // main 함수가 있으면 메인 함수 호출 코드를 설정 
            //set_startPc(1);                                        /* 1행부터 실행 시작  */
            if (mainFunc_p != -1)
            {
                //set_startPc(MLSC_Storage.m_Code.Count);                        /* main에서부터 실행 시작 */
                setCode(MLSC_TokenKind.A_0xAB_FunctionCall, mainFunc_p);
                setCode(MLSC_TokenKind.A_0x28_);
                setCode(MLSC_TokenKind.A_0x29_);
                push_intercode();
            }
        }
        void convert() { // 선두 키워드 분석
            switch (Token.m_Kind)
            {
                case MLSC_TokenKind.A_0x89_Option:      optionSet(); break;  /* 옵션 설정  */
                case MLSC_TokenKind.A_0x90_Var:         varDecl(); break;    /* 변수 선언 */
                case MLSC_TokenKind.A_0x99_Function:    fncDecl(); break;    /* 함수 정의 */
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
                        MLSC_ScriptEngine.Error_Msg("잘못된 break입니다.");
                    setCode(Token.m_Kind); Token = m_Source.m_Tokens.Dequeue(); convert_rest();
                    break;
                case MLSC_TokenKind.A_0xA9_Return:
                    if (!MLSC_ScriptEngine.Storage.FlagCheck("FuncDecl"))
                        MLSC_ScriptEngine.Error_Msg("잘못된 return입니다.");
                    setCode(Token.m_Kind); Token = m_Source.m_Tokens.Dequeue(); convert_rest();
                    break;
                case MLSC_TokenKind.A_0x88_Exit:
                    setCode(Token.m_Kind); Token = m_Source.m_Tokens.Dequeue(); convert_rest();
                    break;
                case MLSC_TokenKind.A_0x7D_:
                    MLSC_ScriptEngine.Error_Msg("잘못된 end입니다.");       /* end가 단독으로 사용되는 일은 없다 */
                    break;
                default: convert_rest(); break;
            }
        }
        void convert_block_set() // 블록 세팅
        {
            int patch_line;
            patch_line = setCode(Token.m_Kind, 0);
            Token = m_Source.m_Tokens.Dequeue();
            convert_rest();
            convert_block();                                            /* 블록 처리 */
            backPatch(patch_line, get_lineNo());        /* NO_FIX_ADRS를 수정(end행 번호) */
        }
        void convert_block()
        {
            MLSC_TokenKind k;
            ++BlockCount;                                      /* 블록 끝까지 문을 분석 */
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
                {      // ↓ 이 키워드가 도중에 나타나는 일은 없다 */
                    case MLSC_TokenKind.A_0x83_IF: case MLSC_TokenKind.A_0x84_ELIF: case MLSC_TokenKind.A_0x85_ELSE:
                    case MLSC_TokenKind.A_0x86_FOR: case MLSC_TokenKind.A_0xAA_Break: case MLSC_TokenKind.A_0x99_Function: case MLSC_TokenKind.A_0xA9_Return: case MLSC_TokenKind.A_0x88_Exit:
                    case MLSC_TokenKind.A_0x89_Option: case MLSC_TokenKind.A_0x90_Var: case MLSC_TokenKind.A_0x7D_:
                        MLSC_ScriptEngine.Error_Msg("잘못된 기술입니다: " + Token.m_Text);
                        break;
                    case MLSC_TokenKind.A_0x02_Ident:                                              /* 함수 호출, 변수 */
                        set_name();
                        //if ((tblNbr = searchName(tmpTb.name, 'F')) != -1)
                        //{    /* 함수 등록 있음*/
                        //    if (tmpTb.name == "main") err_exit("main함수는 호출할 수 없습니다.");
                        //    setCode(Fcall, tblNbr); continue;
                        //}
                        //if ((tblNbr = searchName(tmpTb.name, 'V')) == -1)
                        //{    /* 변수 등록 없음  */
                        //    if (explicit_F) err_exit("변수 선언이 필요합니다: ", tmpTb.name);
                        //    tblNbr = enter(tmpTb, varId);                      /* 자동 변수 등록 */
                        //}
                        //if (is_localName(tmpTb.name, varId)) setCode(Lvar, tblNbr);
                        //else setCode(Gvar, tblNbr);
                        continue;
                    case MLSC_TokenKind.A_0x02_ValueInt: case MLSC_TokenKind.A_0x02_ValueDbl:                         /* 정수도 double형으로 저장 */
                        //setCode(Token.m_Kind, set_LITERAL(token.dblVal));
                        break;
                    case MLSC_TokenKind.A_0x02_ValueStr:
                        //setCode(token.kind, set_LITERAL(token.text));
                        break;
                    default:                                                   /* + - <= 등  */
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
            setCode(MLSC_TokenKind.A_0x89_Option);                 /* 이 행은 비실행이므로 코드 변환은 Option만 */
            setCode_rest();                                       /* 나머진 원래대로 저장 */
            Token = m_Source.m_Tokens.Dequeue();                  /* ↓ 변수 선언을 강제한다 */
            if (Token.m_Kind == MLSC_TokenKind.A_0x02_ValueStr && Token.m_Text == "var")
                MLSC_ScriptEngine.Storage.FlagSet("explicit", true);
            else MLSC_ScriptEngine.Error_Msg("option지정이 바르지 않습니다.");
            Token = m_Source.m_Tokens.Dequeue();
            setCode_EofLine();
        }
        void varDecl()
        {
            setCode(MLSC_TokenKind.A_0x90_Var);                  /* 이 행은 비실행이므로 코드 변환은  Var만 */
            setCode_rest();                                 /* 나머지는 원래대로 저장 */
            for (; ; )
            {
                Token = m_Source.m_Tokens.Dequeue();
                var_namechk(Token);                                         /* 이름 검사 */
                set_name(); set_aryLen();                          /* 배열이면 길이 설정 */
                //enter(tmpTb, varId);                            /* 변수등록(주소도 등록) */
                if (Token.m_Kind != MLSC_TokenKind.A_0x2C_) break;                               /* 선언 종료 */
            }
            setCode_EofLine();
        }
        void var_namechk(MLSC_Token tk)
        {
            if (tk.m_Kind != MLSC_TokenKind.A_0x02_Ident) 
                MLSC_ScriptEngine.Error_Msg(tk.m_Text + "식별자");
            //if (is_localScope() && tk.text[0] == '$')
            //    err_exit("함수 내 var선언에서는 $가 붙은 이름을 지정할 수 없습니다: ", tk.text);
            //if (searchName(tk.text, 'V') != -1)
            //    err_exit("식별자가 중복되었습니다: ", tk.text);
        }
        void set_name() {
            if (Token.m_Kind != MLSC_TokenKind.A_0x02_Ident)
                MLSC_ScriptEngine.Error_Msg("식별자가 필요합니다: " + Token.m_Text);
            //tmpTb.clear(); tmpTb.name = token.text;                         /* 이름 설정 */
            Token = m_Source.m_Tokens.Dequeue();
        }
        void set_aryLen() {
            //tmpTb.aryLen = 0;
            if (Token.m_Kind != MLSC_TokenKind.A_0x5B_) return;                                /* 배열이 아니다 */

            Token = m_Source.m_Tokens.Dequeue();
            if (Token.m_Kind != MLSC_TokenKind.A_0x02_ValueInt)
                MLSC_ScriptEngine.Error_Msg("배열 길이는 양(+)의 정수로 지정해 주세요: " + Token.m_Text);
            //tmpTb.aryLen = (int)token.dblVal + 1;   /* var a[5]는 첨자0~5가 유효이므로 +1 */
            //token = chk_nextTkn(nextTkn(), ']');
            if (Token.m_Kind == MLSC_TokenKind.A_0x5B_) 
                MLSC_ScriptEngine.Error_Msg("다차원 배열은 선언할 수 없습니다.");
        }
        void fncDecl() { }
        void backPatch(int line, int n) {
            // 두번째 중괄호 위치에 있는 값을 수정해야힘.
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