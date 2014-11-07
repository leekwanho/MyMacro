using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MacroProject;


class Hanyung//분리된 한글 영문으로 변환
{

    string result="";//최종 출력 결과 문자열

    public Hanyung(){}

    public string yung(string han)//변환 메인 메소드
    {

        string[] hanBase = {"ㄱ", "ㄲ", "ㄴ", "ㄷ", "ㄸ", "ㄹ", "ㅁ", "ㅂ", "ㅃ", "ㅅ", "ㅆ"
                            , "ㅇ", "ㅈ", "ㅉ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ", "ㅏ", "ㅐ", "ㅑ"
                            , "ㅒ", "ㅓ", "ㅔ", "ㅕ", "ㅖ", "ㅗ", "ㅘ", "ㅙ", "ㅚ", "ㅛ", "ㅜ"
                            ,"ㅝ", "ㅞ", "ㅟ", "ㅠ", "ㅡ", "ㅢ", "ㅣ", "ㄳ", "ㄵ", "ㄶ"
                            , "ㄺ", "ㄻ", "ㄼ", "ㄽ", "ㄾ", "ㄿ", "ㅀ", "ㅄ"};
        string[] yungBase = {"r", "R", "s", "e", "E", "f", "a", "q", "Q", "t", "T"
                            , "d", "w", "W", "c", "z", "x", "v", "g", "k", "o", "i"
                            , "O", "j", "p", "u", "P", "h", "hk", "ho", "hl", "y", "n"
                            , "nj", "np", "nl", "b", "m", "ml", "l", "rt", "sw", "sg"
                            , "fr", "fa", "fq", "ft", "fx", "fv", "fg", "qt"};



        bool success;//변환 실패시 기존문자열 대입위한 확인용

        for(int i=0;i<han.Length;i++){

            success = true;//우선 성공

            for (int j = 0; j < hanBase.Length; j++)
            {
                if (han[i].ToString().Equals(hanBase[j].ToString()))//매개변수 문자값이 한글에 있다면
                {
                    result += yungBase[j];//영문값으로 대입

                    success = false;//변환 성공햇으니 기존 문자값대입 필요없음
                    break;//빠져나옴
                }
            }
            if(success)
                result += han[i];//위에서 못찾았다면 기존문자 대입
                
        }

        return result;

    }


}
