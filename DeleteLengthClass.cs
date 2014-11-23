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

        public int deleteLength(string s)
        {
            munja = s;

            result += munja.Length;//우선 문자길이만큼 지움
            
            char[] charArr = munja.ToCharArray();//결정해야할건 마지막문자가 한글인지 영어인지
            char lastMunja=charArr[charArr.Length-1];

            
            //한글일떄는 한번더 지워야함
            if (char.GetUnicodeCategory(lastMunja) == System.Globalization.UnicodeCategory.OtherLetter)
            {
                result++;
            }
            return result;
        }

        
    }
}
