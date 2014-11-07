using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MacroProject
{
    public partial class AddForm : Form
    {

        MainWindow mainWindow;
        bool access = true;//리스트 중복 안되게 하기위한 변수

        //수정으로 들어왔을떄 체크 변수. 몇번째 포지션인지 확인후 그 포지션 수정
        int position=-1;
        string before_modify;
        string after_modify;
        bool modify = true;

        public AddForm(MainWindow mv)
        {//일반 삽입
            InitializeComponent();
            mainWindow = mv;
        }

        public AddForm(MainWindow mv, string before, string after, int posi)
        {//수정용. 수정될 값들 및 포지션 받아옴
            InitializeComponent();
            mainWindow = mv;
            before_text.Text += before;
            after_text.Text += after;
            before_modify = before;
            after_modify = after;
            position = posi;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String before=before_text.Text;
            String after=after_text.Text;

            access = true;//우선 확인 클릭시 삽입 가능하게 설정

            foreach (ListViewItem lvi in mainWindow.listView.Items)
            {
                if (before.Equals(lvi.SubItems[0].Text))
                {//기존 리스트에 이미 값이 존재하면 불가능하게
                    access = false;
                    MessageBox.Show("이미 리스트에 존재하는 값입니다.");
                    break;
                }

            }


            if (!before.Equals("") && access)
            {//before값이 중복되지 않은 값으로 입력되있을때만

                if (position == -1)//일반 삽입일시
                {
                    ListView lv = mainWindow.listView;

                    lv.BeginUpdate();

                    ListViewItem lvi = new ListViewItem(before);//아이템 삽입작업
                    lvi.SubItems.Add(after);
                    lv.Items.Add(lvi);

                    lv.EndUpdate();

                }
                else//수정일시
                {
                    ListView lv = mainWindow.listView;

                    lv.Items[position].Text = before;//텍스트 수정후
                    lv.Items[position].SubItems[1].Text = after;
                    modify = false;

                    Close();//창 닫음
                }
            }



        }


        private void AddForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (position != -1 && modify)//수정이고 폼 그냥 닫을시 기존값 대입
            {
                mainWindow.listView.Items[position].Text = before_modify;
                mainWindow.listView.Items[position].SubItems[1].Text = after_modify;
            }
        }

    }
}
