using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Collections.Specialized;

namespace MacroProject
{
    public partial class RunForm : Form
    {

        //폼 리사이즈, 이동 위한 변수
        private bool dragging;
        private bool resizing;
        private bool resizingUp;
        private bool resizingDown;
        private bool resizingLeft;
        private bool resizingRight;
        private int border = 4;
        private Size previousSize;
        private Point previousLocation;
        private Point offset;

        ArrayList list;//넘겨받을 치환클래스 리스트
        string message;//F4 눌렸을때 넘겨받을 메시지

        int messageLength1;//들어온 메시지와 기존 메시지 비교할때 기존메시지 비교할 길이 저장할 변수
        bool illchi1 = true;//메시지 비교해 일치하는게 없으면 전체 리스트 띄움. 그 일치여부 저장할 변수
        int messageLength2;
        bool illchi2 = true;
        
        public RunForm(ArrayList al)
        {
            InitializeComponent();
            list = al;
            
        }


        public void reset()
        {
            list1Reset();
            list2Reset();
        }

        
        //실행시 사용자에게 리스트 보여주기 위해 리스트 작성
        private void RunForm_Load(object sender, EventArgs e)
        {
            reset();
        }


        //리스트1 전체 보여주기
        public void list1Reset()
        {
            listView1.Items.Clear();

            foreach (ChihwanClass al in list)
            {
                listView1.BeginUpdate();


                ListViewItem lvi = new ListViewItem(al.getHanwonBeforeText());
                lvi.SubItems.Add(al.getHanwonAfterText());
                listView1.Items.Add(lvi);

                listView1.EndUpdate();
            }
        }

        //리스트2 전체 보여주기
        public void list2Reset()
        {

            listView2.Items.Clear();

            foreach (ChihwanClass al in list)
            {
                listView2.BeginUpdate();

                listView2.Items.Add(al.getHanwonAfterText());

                listView2.EndUpdate();
            }
        }






        //메시지 전달받아 리스트와 대조 후 일치한값 보여주거나 일치한게 없으면 전체 보여줌
        public void renew(string s)
        {
            message = s;
            
            listView1.Items.Clear();
            listView2.Items.Clear();

            illchi1 = true;//우선 일치하는게 없다고 가정
            illchi2 = true;

            string messageYung = new Hanyung().yung(message);

            foreach (ChihwanClass al in list)
            {
                /*******************리스트1 검색**********************/
                if (message.Length >= al.getBeforeText().Length)
                    messageLength1 = al.getBeforeText().Length;
                else
                    messageLength1 = message.Length;

                //메시지와 맞는게 잇으면 그거 보여줌
                if (al.getBeforeText().Substring(0, messageLength1).Equals(message))
                {
                    listView1.BeginUpdate();

                    ListViewItem lvi = new ListViewItem(al.getHanwonBeforeText());
                    lvi.SubItems.Add(al.getHanwonAfterText());
                    listView1.Items.Add(lvi);

                    listView1.EndUpdate();
                    illchi1 = false;//일치한거 있으니 전체리스트 보여줄필요 없음
                }



                /*******************리스트2 검색**********************/
                if (message.Length >= al.getAfterText().Length)
                    messageLength2 = al.getAfterText().Length;
                else
                    messageLength2 = message.Length;

                //메시지와 맞는게 잇으면 그거 보여줌
                if (al.getAfterText().Substring(0, messageLength2).Equals(messageYung))
                {
                    listView2.BeginUpdate();

                    listView2.Items.Add(al.getHanwonAfterText());

                    listView2.EndUpdate();
                    illchi2 = false;//일치한거 있으니 전체리스트 보여줄필요 없음
                }

            }


            //일치한게 없으면 리스트 전체 보여줌
            if (illchi1)
            {
                list1Reset();
            }

        }





        bool tabOn = false;//탭 눌린거 확인용
        int index = 0;
        
        //탭키 눌리면 인덱스 최상위로
        public void tab(){
            tabOn = true;
            index = 0;
        }

        //리스트뷰에서 키 눌릴때. 이동 및 엔터키 구현
        private void listView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (tabOn)//사용자가 실수로 런폼 클릭후 엔터치는거 방지용
            {
                if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) && index > listView2.Items.Count - 1)
                    index = 0;

                else if (e.KeyCode == Keys.Down && index < listView2.Items.Count - 1)
                    index++;

                else if (e.KeyCode == Keys.Up && index > 0)
                    index--;

                if (e.KeyCode == Keys.Enter)
                {
                    Application.DoEvents();
                    SendKeys.SendWait("%{TAB}");
                    Application.DoEvents();


                    int messageLengthTemp = message.Length;//입력된 메시지의 길이 저장 변수
                    int count = 0;//완성 메시지에서 몇번째부터 출력할지 결졍하는 인덱스


                    //완성 텍스트에서 앞의 한글자씩 때와서 자소 분리후
                    //입력된 메시지에서 분리한 글자 길이만큼 제거하고 다음글자 확인위해 카운트 증가
                    //입력된 메시지길이가 0보다 작아지는 순간
                    //제거는 끝난것이라 판단하고 루프 빠져나감
                    while (true)
                    {
                        //완성 메시지에서 앞글자부터 하나씩 자소분리후 받아옴
                        string temp = new Sep().Seperate(listView2.Items[index].SubItems[0].Text.Substring(count, 1));

                        if (messageLengthTemp > 0)//입력된 메시지가 아직 존재하면
                        {
                            messageLengthTemp -= temp.Length;//완성 메시지 따온 부분 길이만큼 뺌
                            count++;//다음글자로 이동위해 카운트 증가
                        }

                        else//입력된 문자가 모두 제거된 상태면 루프 빠져나감
                            break;


                        //한글일 경우 미완성일수도 있음
                        //완성본이 '한글' 일시 '하' 까지만 입력했을경우
                        //위에서 이미 messageLengthTemp가 2로 제거작업이 이루어졌고
                        //빼진 값은 3으로 -1이 되므로 만약 한글 완성을 안하고 작업했을경우
                        //그 완성안된 한글을 지우고 다시 입력하기위에 카운트 하나 줄인후
                        //루프 빠져나감.
                        //'한'이 완성됬을경우나(완성된 한글을 빼면 0 이상의 값이됨)
                        //알파벳, 숫자등일 경우는 1씩 빼지므로 -로 갈수 없음.
                        if (messageLengthTemp < 0)
                        {
                            SendKeys.SendWait("{BACKSPACE}");
                            count--;
                            break;
                        }


                    }


                    IDataObject ido = Clipboard.GetDataObject();
                    //viewBox.Text+=ido;

                    Application.DoEvents();

                    Clipboard.SetText(listView2.Items[index].SubItems[0].Text.Substring(count));                    
                    SendKeys.SendWait("^v");

                    Application.DoEvents();

                    Clipboard.Clear();

                    Clipboard.SetDataObject(ido);
                    
                    
                    if (ido.GetDataPresent(typeof(string).FullName))
                        Clipboard.SetText( (string)ido.GetData(typeof(string)) );
                    else if (ido.GetDataPresent(typeof(Image).FullName))
                        Clipboard.SetImage((Image)ido.GetData(typeof(Image)));
                    else if (ido.GetDataPresent(typeof(StringCollection).FullName))
                        Clipboard.SetFileDropList((StringCollection)ido.GetData(typeof(StringCollection)));
                    
                    
                    //초기화
                    tabOn = false;
                    MainWindow.f4 = false;
                }
            }
                
        }



        /*
         * 폼 리사이즈, 이동 위한 메소드
         * **/
        private void RunForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            offset = e.Location;
            if (resizing)
            {
                previousSize = this.Size;
                previousLocation = this.Location;
            }
        }
        private void RunForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
            resizing = false;
            resizingUp = false;
            resizingDown = false;
            resizingLeft = false;
            resizingRight = false;
        }
        private void RunForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                if (resizing)
                {
                    Point currentScreenPos = PointToScreen(e.Location);
                    if (resizingLeft)
                    {
                        if (resizingUp)
                        {
                            this.Width = previousLocation.X + previousSize.Width - currentScreenPos.X + offset.X;
                            this.Height = previousLocation.Y + previousSize.Height - currentScreenPos.Y + offset.Y;
                            this.Location = new Point
                            (previousLocation.X + previousSize.Width - this.Width,
                            previousLocation.Y + previousSize.Height - this.Height);
                        }
                        else if (resizingDown)
                        {
                            this.Width = previousLocation.X + previousSize.Width - currentScreenPos.X + offset.X;
                            this.Height = currentScreenPos.Y - previousLocation.Y + previousSize.Height - offset.Y;
                            this.Location = new Point
                            (previousLocation.X + previousSize.Width - this.Width,
                            previousLocation.Y);
                        }
                        else
                        {
                            this.Width = previousLocation.X + previousSize.Width - currentScreenPos.X + offset.X;
                            this.Location = new Point(previousLocation.X + previousSize.Width - this.Width, previousLocation.Y);
                        }
                    }
                    else if (resizingRight)
                    {
                        if (resizingUp)
                        {
                            this.Width = currentScreenPos.X - previousLocation.X + previousSize.Width - offset.X;
                            this.Height = previousLocation.Y + previousSize.Height - currentScreenPos.Y + offset.Y;
                            this.Location = new Point
                            (previousLocation.X,
                            previousLocation.Y + previousSize.Height - this.Height);
                        }
                        else if (resizingDown)
                        {
                            this.Width = currentScreenPos.X - previousLocation.X + previousSize.Width - offset.X;
                            this.Height = currentScreenPos.Y - previousLocation.Y + previousSize.Height - offset.Y;
                        }
                        else
                        {
                            this.Width = currentScreenPos.X - previousLocation.X + previousSize.Width - offset.X;
                        }
                    }
                    else if (resizingUp)
                    {
                        this.Height = previousLocation.Y + previousSize.Height - currentScreenPos.Y + offset.Y;
                        this.Location = new Point(previousLocation.X, previousLocation.Y + previousSize.Height - this.Height);
                    }
                    else if (resizingDown)
                    {
                        this.Height = currentScreenPos.Y - previousLocation.Y + previousSize.Height - offset.Y;
                    }
                }
                else
                {
                    Point currentScreenPos = PointToScreen(e.Location);
                    Location = new Point
                    (currentScreenPos.X - offset.X,
                    currentScreenPos.Y - offset.Y);
                }
            }
            else
            {
                if (e.X <= border)
                {
                    resizing = true;
                    resizingUp = false;
                    resizingDown = false;
                    resizingLeft = true;
                    resizingRight = false;

                    if (e.Y <= border)
                    {
                        this.Cursor = Cursors.SizeNWSE;
                        resizingUp = true;
                    }
                    else if (e.Y >= this.Height - border)
                    {
                        this.Cursor = Cursors.SizeNESW;
                        resizingDown = true;
                    }
                    else
                    {
                        this.Cursor = Cursors.SizeWE;
                    }
                }
                else if (e.X >= this.Width - border)
                {
                    resizing = true;
                    resizingUp = false;
                    resizingDown = false;
                    resizingLeft = false;
                    resizingRight = true;

                    if (e.Y <= border)
                    {
                        this.Cursor = Cursors.SizeNESW;
                        resizingUp = true;
                    }
                    else if (e.Y >= this.Height - border)
                    {
                        this.Cursor = Cursors.SizeNWSE;
                        resizingDown = true;
                    }
                    else
                    {
                        this.Cursor = Cursors.SizeWE;
                    }
                }
                else if (e.Y <= border)
                {
                    resizing = true;
                    resizingUp = true;
                    resizingLeft = false;
                    resizingRight = false;
                    this.Cursor = Cursors.SizeNS;
                }
                else if (e.Y >= this.Height - border)
                {
                    resizing = true;
                    resizingDown = true;
                    resizingLeft = false;
                    resizingRight = false;
                    this.Cursor = Cursors.SizeNS;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    resizing = false;
                    resizingUp = false;
                    resizingDown = false;
                    resizingLeft = false;
                    resizingRight = false;
                }
            }
        }
        /*리사이즈, 이동 메소드 끝*/


        //닫기클릭시 폼 닫음
        private void closeForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
