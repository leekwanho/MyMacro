using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MacroProject
{
    class ChihwanClass
    {
        private string hanwonBeforeText;
        private string hanwonAfterText;
        private string beforeText;
        private string afterText;
        private int deleteText;

        public ChihwanClass(string wonBefore, string wonAfter, string before, string after, int delete)
        {
            hanwonBeforeText = wonBefore;
            hanwonAfterText = wonAfter;
            beforeText = before;
            afterText = after;
            deleteText = delete;
        }

        public void setHanwonBeforeText(string s)
        {
            hanwonBeforeText = s;
        }
        public string getHanwonBeforeText()
        {
            return hanwonBeforeText;
        }

        public void setHanwonAfterText(string s)
        {
            hanwonAfterText = s;
        }
        public string getHanwonAfterText()
        {
            return hanwonAfterText;
        }

        public void setBeforeText(string s)
        {
            beforeText = s;
        }
        public string getBeforeText()
        {
            return beforeText;
        }

        public void setAfterText(string s)
        {
            afterText = s;
        }
        public string getAfterText()
        {
            return afterText;
        }

        public void setDeleteText(int s)
        {
            deleteText = s;
        }
        public int getDeleteText()
        {
            return deleteText;
        }

    }
}
