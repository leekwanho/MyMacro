using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Net.Json;

namespace MacroProject
{
    


    public partial class MainWindow : Form
    {

        static public ArrayList chihwanList=new ArrayList();//치환할 문자열 저장할 리스트
        
        static bool shift = false;//#입력시 쉬프트 눌르고 해야되서 쉬프트 눌린거 확인용

        static public bool f4=false;//F4 눌릴시 변환 이벤트 시작
        static int chihwanMax=0;//리스트에서 before의 최대길이 얻어 저장할 변수
        static public int chihwanCount = 0;//#이후 입력되는 문자 길이. 이게 위의 최대값 넘어가면 변환없이 종료
        static public string resultMunja = "";//F4이후 입력되는 문자 저장 배열

        static RunForm rf;//실행시 나오는 런폼


        //키보드 후킹
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        [DllImport("user32.dll")]
        static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);






        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        const int WH_KEYBOARD_LL = 13;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101; 

        private LowLevelKeyboardProc _proc = hookProc;

        private static IntPtr hhook = IntPtr.Zero;






        public MainWindow()
        {
            InitializeComponent();

            this.FormClosing += Form1_FormClosing;
            this.notifyIcon1.DoubleClick += notifyIcon1_DoubleClick;
            this.ExitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;

        }





        /*****************후킹시작********************/
        public void SetHook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_KEYBOARD_LL, _proc, hInstance, 0);
        }

        public static void UnHook()
        {
            UnhookWindowsHookEx(hhook);
        }



        private static IntPtr hookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            
            int vkCode = Marshal.ReadInt32(lParam);
            

            
            //쉬프트 눌린거 처리. 영어 대소문자 때문에
            if (vkCode.ToString() == "160" && wParam == (IntPtr)WM_KEYDOWN)            
                shift = true;
            
            else if (vkCode.ToString() == "160" && wParam == (IntPtr)WM_KEYUP)
                shift = false;
            

            //F4눌렸을때 처리
            if (vkCode.ToString() == "115" && wParam == (IntPtr)WM_KEYDOWN)
            {
                f4 = true;//F4 눌린걸로
                chihwanCount = 0;//최대길이만큼 입력할때까지 안되면 그냥 F4 필요할때 입력할수 있게
                resultMunja = "";//다시 리스트와 비교하기위해 초기화
                rf.reset();

                return (IntPtr)1;
            }




            //입력 문자열이 리스트의 최대 문자보다 커지면 없는걸로 간주. f4해제
            if (chihwanCount >= chihwanMax)
            {
                f4 = false;
            }





            /*********F4 입력됫을떄 이벤트 발생**********/
            if (f4 && wParam == (IntPtr)WM_KEYDOWN)
            {
                KeysConverter kc = new KeysConverter();
                string keyChar = kc.ConvertToString(vkCode);




                //쉬프트 눌렷을땐 대문자
                if (vkCode >= 65 && vkCode <= 90 && shift && wParam == (IntPtr)WM_KEYDOWN)
                {
                    //tb.AppendText(keyChar.ToUpper());
                    resultMunja += keyChar.ToUpper();
                    chihwanCount++;
                }
                //A~Z까지일때 일반적으로는 소문자
                else if (vkCode >= 65 && vkCode <= 90 && wParam == (IntPtr)WM_KEYDOWN)
                {
                    //tb.AppendText(keyChar.ToLower());
                    resultMunja += keyChar.ToLower();
                    chihwanCount++;
                }
                //백스페이스시 지움
                else if (vkCode == 08 && wParam == (IntPtr)WM_KEYDOWN)
                {
                    //tb.AppendText(keyChar);
                    if (chihwanCount > 0)
                    {
                        resultMunja = resultMunja.Substring(0, resultMunja.Length - 1);
                        chihwanCount--;
                    }
                }

                //방향키는 별도로 입력해야됨. 안하면 리스트뷰에서 방향이동 불가능.
                else if (vkCode == 37 && wParam == (IntPtr)WM_KEYDOWN)
                {
                    SendKeys.Send("{LEFT}");
                    return (IntPtr)1;
                }
                else if (vkCode == 39 && wParam == (IntPtr)WM_KEYDOWN)
                {
                    SendKeys.Send("{RIGHT}");
                    return (IntPtr)1;
                }
                else if (vkCode == 38 && wParam == (IntPtr)WM_KEYDOWN)
                {
                    SendKeys.Send("{UP}");
                    return (IntPtr)1;
                }
                else if (vkCode == 40 && wParam == (IntPtr)WM_KEYDOWN)
                {
                    SendKeys.Send("{DOWN}");
                    return (IntPtr)1;
                }


                //알파벳 아니면 일반문자
                else if (vkCode >= 48 && vkCode <= 57 && wParam == (IntPtr)WM_KEYDOWN)
                {
                    //tb.AppendText(keyChar);
                    resultMunja += keyChar;
                    chihwanCount++;
                }

                rf.renew(resultMunja);//실행폼에 입력된 메시지 보냄



                //리스트와 비교해서 동일한게 잇는지 확인
                foreach (ChihwanClass c in chihwanList)
                {
                    if (resultMunja.Equals(c.getBeforeText()))//리스트와 비교해 있을시 이벤트 실행
                    {
                        SendKeys.Send(" ");//새로 입력해서 커서 없앤후
                        for (int i = 0; i < c.getDeleteText(); i++)//입력했던거 다지우고
                            SendKeys.Send("{BACKSPACE}");


                        Application.DoEvents();

                        Clipboard.SetText(c.getHanwonAfterText());
                        SendKeys.SendWait("^v");//새로 입력후

                        Application.DoEvents();

                        Clipboard.Clear();


                        //초기화
                        chihwanCount = 0;
                        resultMunja = "";
                        f4 = false;

                        rf.list1Reset();//전체 리스트 보여줌

                        return (IntPtr)1;//성공했을때 마지막 키입력 포함 안되게
                    }
                }



                //탭 눌렸을때 처리.
                if (vkCode.ToString() == "9" && wParam == (IntPtr)WM_KEYDOWN)
                {
                    rf.Activate();//런폼 활성화
                    rf.tab();//탭입력된거 인식
                    return (IntPtr)1;
                }
                
            }
            /*********F4 입력됫을떄 이벤트 발생 끝**********/



            



            
            return CallNextHookEx(hhook, code, (int)wParam, lParam);
        }
        /*****************후킹끝********************/






        // 트레이의 종료 메뉴를 눌렀을때
        void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //트레이아이콘 없앰
            notifyIcon1.Visible = false;
            //프로세스 종료
            System.Environment.Exit(1);
        }


        //트레이 아이콘을 더블클릭 했을시 호출
        void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Visible = true; // 폼의 표시
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal; // 최소화를 멈춘다 
            this.Activate(); // 폼을 활성화 시킨다
        }

        //폼이 종료 되려 할때 호출
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true; // 종료 이벤트를 취소 시킨다
            this.Visible = false; // 폼을 표시하지 않는다;
        }



        //리스트 아이템 추가
        private void list_add_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm(this);
            addForm.Show();
        }


        //선택된 아이템 삭제
        private void list_delete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView.Items)
            {
                if (lvi.Selected)
                    listView.Items.Remove(lvi);
            }
        }
        

        //선택된 단일 아이템 수정
        private void list_modify_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count != 1)//한개만 선택되어 있어야 함
                MessageBox.Show("리스트중 한개만 선택되어 있어야 합니다.");
            else
            {
                string before = listView.SelectedItems[0].Text;//이전, 이후, 포지션 뽑아옴
                string after = listView.SelectedItems[0].SubItems[1].Text;
                int position = listView.SelectedItems[0].Index;

                listView.SelectedItems[0].Text = "";//아이템 비어있게 함. 수정시 중복방지
                listView.SelectedItems[0].SubItems[1].Text = "";

                AddForm addForm = new AddForm(this, before, after, position);//폼실행
                addForm.Show();

            }
        }


        //선택된 아이템 위로
        private void btn_up_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count >= 1)
            {
                for (int i =0 ; i < listView.SelectedItems.Count ; i++)
                {

                    ListViewItem item = listView.SelectedItems[i];
                    int index = 0;
                    index = listView.SelectedItems[i].Index - 1;

                    if (index != -1)
                    {
                        listView.Items.RemoveAt(listView.SelectedIndices[i]);
                        listView.Items.Insert(index, item);
                        listView.Items[index].Selected = true;
                        listView.Items[index].Focused = true;
                    }
                    else
                        break;
                }
            }

        }

        //선택된 아이템 아래로
        private void btn_down_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count >= 1)
            {
                for (int i = 0; i < listView.SelectedItems.Count; i++)
                {

                    ListViewItem item = listView.SelectedItems[i];
                    int index = 0;
                    index = listView.SelectedItems[i].Index + 1;

                    if (index != listView.Items.Count)
                    {
                        listView.Items.RemoveAt(listView.SelectedIndices[i]);
                        listView.Items.Insert(index, item);
                        listView.Items[index].Selected = true;
                        listView.Items[index].Focused = true;
                    }
                    else
                        break;
                }
            }
        }




        //리스트뷰 컬럼 클릭시 아이템 정렬
        private void listView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            for (int i = 0; i < 2; i++)
            {
                listView.Columns[i].Text = listView.Columns[i].Text.Replace(" ▼", "");
                listView.Columns[i].Text = listView.Columns[i].Text.Replace(" ▲", "");
            }

            if (this.listView.Sorting == SortOrder.Ascending || listView.Sorting == SortOrder.None)
            {
                this.listView.ListViewItemSorter = new ListViewItemComparer(e.Column, "desc");
                listView.Sorting = SortOrder.Descending;
                listView.Columns[e.Column].Text = listView.Columns[e.Column].Text + " ▼";
            }

            else
            {
                this.listView.ListViewItemSorter = new ListViewItemComparer(e.Column, "asc");
                listView.Sorting = SortOrder.Ascending;
                listView.Columns[e.Column].Text = listView.Columns[e.Column].Text + " ▲";
            }

            listView.Sort();
        }


        //작동 시작
        private void start_Click(object sender, EventArgs e)
        {

            SetHook();//후킹시작
            list_add.Enabled = false;//후킹중일땐 리스트 추가삭제 불가능하게
            list_delete.Enabled = false;
            start.Enabled = false;//이중작동 방지
            stop.Enabled = true;//닫기폼 작동가능하게

            this.Visible = false; // 폼을 표시하지 않는다;



            //리스트에 잇는거 가져오기
            foreach (ListViewItem lvi in listView.Items)
            {
                string beforeWon = lvi.SubItems[0].Text;//리스트 아이템 가져옴
                string beforeWon_s = new Sep().Seperate(beforeWon);//자모음 분해
                string beforeWon_y = new Hanyung().yung(beforeWon_s);//분해된거 영어로

                string afterWon = lvi.SubItems[1].Text;//바뀔 문자
                string afterWon_s = new Sep().Seperate(afterWon);//자모음 분해
                string afterWon_y = new Hanyung().yung(afterWon_s);//분해된거 영어로

                int deleteLength = new DeleteLengthClass().deleteLength(beforeWon);//몇개 지워야되는가

                chihwanList.Add(new ChihwanClass(beforeWon, afterWon, beforeWon_y, afterWon_y, deleteLength));
                
            }


            //리스트에서 before의 최대길이 얻음
            foreach (ChihwanClass c in chihwanList)
            {
                if (chihwanMax < c.getBeforeText().Length)
                    chihwanMax = c.getBeforeText().Length;
            }
            
            rf = new RunForm(chihwanList);//실행폼 실행
            rf.Show();
            

        }




        private void stop_Click(object sender, EventArgs e)
        {
            UnHook();//후킹종료
            list_add.Enabled = true;//다시 가능하게
            list_delete.Enabled = true;
            start.Enabled = true;//다시 작동 가능하게
            stop.Enabled = false;//닫기폼은 불가능하게

            rf.Close();//자식폼 닫기

            chihwanList.Clear();//리스트비움
        }




        SaveFileDialog saveDlg = new SaveFileDialog();

        //저장버튼클릭. 리스트 별도파일 저장
        private void listSave_Click(object sender, EventArgs e)
        {
            saveDlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {

                JsonArrayCollection array = new JsonArrayCollection();
                JsonObjectCollection roop;

                //리스트대로 json 작성. 클래스는 추후 작동시에 자동 작성됨
                foreach (ListViewItem lvi in listView.Items)
                {
                    roop = new JsonObjectCollection();
                    roop.Add(new JsonStringValue("beforeWon", lvi.SubItems[0].Text));
                    roop.Add(new JsonStringValue("afterWon", lvi.SubItems[1].Text));
                    array.Add(roop);
                }


                //저장
                StreamWriter streamWriter = new StreamWriter(saveDlg.FileName, false, Encoding.Unicode);
                streamWriter.WriteLine(array.ToString());
                streamWriter.Close();

            }
            
        }




        //저장된 리스트 불러오기
        OpenFileDialog openDlg = new OpenFileDialog();
        private void listOpen_Click(object sender, EventArgs e)
        {
            openDlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            //불러오기
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                //불러와서 텍스트 저장
                string text="";
                string file = openDlg.FileName;
                try
                {
                    text = File.ReadAllText(file);
                }
                catch (IOException)
                {
                }
                

                //리스트뷰 초기화. 덪붙이는게 아닌 아예 새롭게 작성함
                listView.Items.Clear();


                //json파싱
                JsonTextParser parser = new JsonTextParser();
                JsonObject configObj = parser.Parse(text);

                JsonArrayCollection items = (JsonArrayCollection)configObj;

                foreach (JsonObjectCollection item in items)
                {
                    //불러온뒤
                    String beforeWon = (String)item["beforeWon"].GetValue();
                    String afterWon = (String)item["afterWon"].GetValue();

                    //리스트뷰에 작성
                    listView.BeginUpdate();

                    ListViewItem lvi = new ListViewItem(beforeWon);
                    lvi.SubItems.Add(afterWon);
                    listView.Items.Add(lvi);

                    listView.EndUpdate();
                                                            
                }

            }

        }
        
    }
    
}
