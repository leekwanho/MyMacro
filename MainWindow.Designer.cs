namespace MacroProject
{
    partial class MainWindow
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.stop = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.before = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.after = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.start = new System.Windows.Forms.Button();
            this.list_add = new System.Windows.Forms.Button();
            this.list_delete = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menu = new System.Windows.Forms.ToolStripMenuItem();
            this.listSave = new System.Windows.Forms.ToolStripMenuItem();
            this.listOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_up = new System.Windows.Forms.Button();
            this.btn_down = new System.Windows.Forms.Button();
            this.list_modify = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stop
            // 
            this.stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stop.Enabled = false;
            this.stop.Location = new System.Drawing.Point(450, 263);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(75, 23);
            this.stop.TabIndex = 0;
            this.stop.Text = "중지";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.before,
            this.after});
            this.listView.FullRowSelect = true;
            this.listView.Location = new System.Drawing.Point(12, 27);
            this.listView.Name = "listView";
            this.listView.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.listView.Size = new System.Drawing.Size(351, 259);
            this.listView.TabIndex = 1;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView_ColumnClick);
            // 
            // before
            // 
            this.before.Text = "기존문자";
            this.before.Width = 151;
            // 
            // after
            // 
            this.after.Text = "치환할 문자";
            this.after.Width = 184;
            // 
            // start
            // 
            this.start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.start.Location = new System.Drawing.Point(369, 263);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 2;
            this.start.Text = "작동";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // list_add
            // 
            this.list_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.list_add.Location = new System.Drawing.Point(434, 77);
            this.list_add.Name = "list_add";
            this.list_add.Size = new System.Drawing.Size(75, 23);
            this.list_add.TabIndex = 3;
            this.list_add.Text = "추가";
            this.list_add.UseVisualStyleBackColor = true;
            this.list_add.Click += new System.EventHandler(this.list_add_Click);
            // 
            // list_delete
            // 
            this.list_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.list_delete.Location = new System.Drawing.Point(434, 124);
            this.list_delete.Name = "list_delete";
            this.list_delete.Size = new System.Drawing.Size(75, 23);
            this.list_delete.TabIndex = 4;
            this.list_delete.Text = "삭제";
            this.list_delete.UseVisualStyleBackColor = true;
            this.list_delete.Click += new System.EventHandler(this.list_delete_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(537, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menu
            // 
            this.menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listSave,
            this.listOpen});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(41, 20);
            this.menu.Text = "메뉴";
            // 
            // listSave
            // 
            this.listSave.Name = "listSave";
            this.listSave.Size = new System.Drawing.Size(124, 22);
            this.listSave.Text = "저장";
            this.listSave.Click += new System.EventHandler(this.listSave_Click);
            // 
            // listOpen
            // 
            this.listOpen.Name = "listOpen";
            this.listOpen.Size = new System.Drawing.Size(124, 22);
            this.listOpen.Text = "불러오기";
            this.listOpen.Click += new System.EventHandler(this.listOpen_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ExitToolStripMenuItem.Text = "종료";
            // 
            // btn_up
            // 
            this.btn_up.Location = new System.Drawing.Point(374, 103);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(27, 23);
            this.btn_up.TabIndex = 7;
            this.btn_up.Text = "↑";
            this.btn_up.UseVisualStyleBackColor = true;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // btn_down
            // 
            this.btn_down.Location = new System.Drawing.Point(374, 150);
            this.btn_down.Name = "btn_down";
            this.btn_down.Size = new System.Drawing.Size(27, 23);
            this.btn_down.TabIndex = 8;
            this.btn_down.Text = "↓";
            this.btn_down.UseVisualStyleBackColor = true;
            this.btn_down.Click += new System.EventHandler(this.btn_down_Click);
            // 
            // list_modify
            // 
            this.list_modify.Location = new System.Drawing.Point(434, 172);
            this.list_modify.Name = "list_modify";
            this.list_modify.Size = new System.Drawing.Size(75, 23);
            this.list_modify.TabIndex = 9;
            this.list_modify.Text = "수정";
            this.list_modify.UseVisualStyleBackColor = true;
            this.list_modify.Click += new System.EventHandler(this.list_modify_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 306);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.start);
            this.Controls.Add(this.list_modify);
            this.Controls.Add(this.btn_down);
            this.Controls.Add(this.list_delete);
            this.Controls.Add(this.btn_up);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.list_add);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "MyMacro";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button stop;
        public System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader before;
        private System.Windows.Forms.ColumnHeader after;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button list_add;
        private System.Windows.Forms.Button list_delete;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listSave;
        private System.Windows.Forms.ToolStripMenuItem listOpen;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.Button btn_down;
        private System.Windows.Forms.Button list_modify;

    }
}

