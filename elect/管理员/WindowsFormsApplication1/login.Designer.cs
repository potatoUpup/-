namespace WindowsFormsApplication1
{
    partial class login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.logoutbutton = new System.Windows.Forms.Button();
            this.loginbutton = new System.Windows.Forms.Button();
            this.PassWord = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.pwd_label = new System.Windows.Forms.Label();
            this.usernamelabel = new System.Windows.Forms.Label();
            this.message = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(585, 55);
            this.panel1.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(68, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(257, 39);
            this.label4.TabIndex = 1;
            this.label4.Text = "毕业设计双选系统";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(60, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // logoutbutton
            // 
            this.logoutbutton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.logoutbutton.BackColor = System.Drawing.Color.Transparent;
            this.logoutbutton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.logoutbutton.Location = new System.Drawing.Point(311, 281);
            this.logoutbutton.Name = "logoutbutton";
            this.logoutbutton.Size = new System.Drawing.Size(100, 45);
            this.logoutbutton.TabIndex = 26;
            this.logoutbutton.Text = "退出";
            this.logoutbutton.UseVisualStyleBackColor = false;
            this.logoutbutton.Click += new System.EventHandler(this.logoutbutton_Click);
            // 
            // loginbutton
            // 
            this.loginbutton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loginbutton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginbutton.Location = new System.Drawing.Point(138, 281);
            this.loginbutton.Name = "loginbutton";
            this.loginbutton.Size = new System.Drawing.Size(100, 45);
            this.loginbutton.TabIndex = 25;
            this.loginbutton.Text = "登录";
            this.loginbutton.UseVisualStyleBackColor = true;
            this.loginbutton.Click += new System.EventHandler(this.loginbutton_Click);
            // 
            // PassWord
            // 
            this.PassWord.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PassWord.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PassWord.Location = new System.Drawing.Point(217, 174);
            this.PassWord.Name = "PassWord";
            this.PassWord.PasswordChar = '*';
            this.PassWord.Size = new System.Drawing.Size(175, 23);
            this.PassWord.TabIndex = 24;
            this.PassWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PassWord_KeyDown);
            // 
            // username
            // 
            this.username.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.username.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.username.Location = new System.Drawing.Point(217, 113);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(175, 23);
            this.username.TabIndex = 23;
            // 
            // pwd_label
            // 
            this.pwd_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pwd_label.AutoSize = true;
            this.pwd_label.BackColor = System.Drawing.Color.Transparent;
            this.pwd_label.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pwd_label.Location = new System.Drawing.Point(145, 172);
            this.pwd_label.Name = "pwd_label";
            this.pwd_label.Size = new System.Drawing.Size(69, 25);
            this.pwd_label.TabIndex = 22;
            this.pwd_label.Text = "密码：";
            // 
            // usernamelabel
            // 
            this.usernamelabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.usernamelabel.AutoSize = true;
            this.usernamelabel.BackColor = System.Drawing.Color.Transparent;
            this.usernamelabel.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.usernamelabel.Location = new System.Drawing.Point(145, 113);
            this.usernamelabel.Name = "usernamelabel";
            this.usernamelabel.Size = new System.Drawing.Size(69, 25);
            this.usernamelabel.TabIndex = 21;
            this.usernamelabel.Text = "账号：";
            // 
            // message
            // 
            this.message.AutoSize = true;
            this.message.Location = new System.Drawing.Point(215, 360);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(0, 12);
            this.message.TabIndex = 27;
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.Controls.Add(this.message);
            this.Controls.Add(this.logoutbutton);
            this.Controls.Add(this.loginbutton);
            this.Controls.Add(this.PassWord);
            this.Controls.Add(this.username);
            this.Controls.Add(this.pwd_label);
            this.Controls.Add(this.usernamelabel);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "login";
            this.Text = "管理员登录界面";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button logoutbutton;
        private System.Windows.Forms.Button loginbutton;
        protected System.Windows.Forms.TextBox PassWord;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Label pwd_label;
        private System.Windows.Forms.Label usernamelabel;
        private System.Windows.Forms.Label message;
    }
}

