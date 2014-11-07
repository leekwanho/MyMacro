using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace MacroProject
{
    //ListView 의 특정 컬럼의 값을 기반으로 정렬을 수행토록 정의
    class ListViewItemComparer : IComparer
    {
        private int col;
        public string sort = "asc";

        public ListViewItemComparer()
        {
            col = 0;
        }

        public ListViewItemComparer(int column, string sort)
        {
            col = column;
            this.sort = sort;
        }

        public int Compare(object x, object y)
        {
            if (sort == "asc")
                return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            else
                return String.Compare(((ListViewItem)y).SubItems[col].Text, ((ListViewItem)x).SubItems[col].Text);
        }
    }
}
