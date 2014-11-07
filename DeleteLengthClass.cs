using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroProject
{
    class DeleteLengthClass
    {
        string munja;//지울 길이가 얼마인지 알아내기위해 받을 변수
        int result = 0;//얼마나 백스페이스 눌러야되는지 결과값
        private string jongsung; 

        public int deleteLength(string s)
        {
            jongsung = "";
            munja = s;

            result += munja.Length;//우선 문자길이만큼 지움
            
            char[] charArr = munja.ToCharArray();//결정해야할건 마지막문자가 한글인지 영어인지
            char lastMunja=charArr[charArr.Length-1];

            
            //한글일떄
            if (char.GetUnicodeCategory(lastMunja) == System.Globalization.UnicodeCategory.OtherLetter)
            {

                char cWork = char.Parse(munja.Substring(munja.Length-1));

                devide(cWork);

                if (jongsung == " " || jongsung == "")//마지막 글자에 종성이 있다면 한번더 지워야됨
                {}//종성이 없어가 한글이 아니면 결과에 1더하지 않음
                else
                    result++;
                
            }

            return result;
        }




        private static string jongsungTable = " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";
        private static ushort UniCodeHanBase = 0xAC00;


        public void devide(char c한글자)
        {
            int jongsungIndex; // 초성,중성,종성의 인덱스
            ushort uTempCode = 0x0000;       // 임시 코드용
            //Char을 16비트 부호없는 정수형 형태로 변환 - Unicode
            uTempCode = Convert.ToUInt16(c한글자);
            // 캐릭터가 한글이 아닐 경우 처리
            if ((uTempCode < UniCodeHanBase) || (uTempCode > UniCodeHanBase))
            {
                jongsung = "";
            }
            // iUniCode에 한글코드에 대한 유니코드 위치를 담고 이를 이용해 인덱스 계산.
            int iUniCode = uTempCode - UniCodeHanBase;
            iUniCode = iUniCode % (21 * 28);
            iUniCode = iUniCode % 28;
            jongsungIndex = iUniCode;
            jongsung = new string(jongsungTable[jongsungIndex], 1);
        }








        

    }
}
