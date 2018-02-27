namespace ASM31
{
    partial class AcctMgr
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.acctList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtAcctName = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtPWHash = new System.Windows.Forms.TextBox();
            this.btnPwdChange = new System.Windows.Forms.Button();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.chkActivated = new System.Windows.Forms.CheckBox();
            this.cbxAccessLvl = new System.Windows.Forms.ComboBox();
            this.cbxMembership = new System.Windows.Forms.ComboBox();
            this.cbxPMembership = new System.Windows.Forms.ComboBox();
            this.dpExpiry = new System.Windows.Forms.DateTimePicker();
            this.txtTollEdit = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.txtLastIP = new System.Windows.Forms.TextBox();
            this.txtLastMac = new System.Windows.Forms.TextBox();
            this.txtLastSvr = new System.Windows.Forms.TextBox();
            this.txtQues = new System.Windows.Forms.TextBox();
            this.txtAns = new System.Windows.Forms.TextBox();
            this.btnChars = new System.Windows.Forms.Button();
            this.btnBAN = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtHasExpiry = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtToll = new System.Windows.Forms.TextBox();
            this.btnDateClear = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.acctList);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.groupBox1.Location = new System.Drawing.Point(15, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 533);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Accounts";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnRefresh.Location = new System.Drawing.Point(9, 493);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(213, 33);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // acctList
            // 
            this.acctList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.acctList.FormattingEnabled = true;
            this.acctList.ItemHeight = 16;
            this.acctList.Location = new System.Drawing.Point(9, 21);
            this.acctList.Name = "acctList";
            this.acctList.ScrollAlwaysVisible = true;
            this.acctList.Size = new System.Drawing.Size(216, 452);
            this.acctList.Sorted = true;
            this.acctList.TabIndex = 0;
            this.acctList.SelectedValueChanged += new System.EventHandler(this.acctList_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(278, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Record ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(278, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Account Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(278, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Password Hash:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(278, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "Email Address:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(278, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 23);
            this.label6.TabIndex = 6;
            this.label6.Text = "Access Level:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(278, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 23);
            this.label7.TabIndex = 7;
            this.label7.Text = "Membership:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(278, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Activated:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(278, 207);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 23);
            this.label8.TabIndex = 9;
            this.label8.Text = "Prev Membership:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(278, 234);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(133, 23);
            this.label9.TabIndex = 10;
            this.label9.Text = "Expiry Date:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(278, 261);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(133, 23);
            this.label10.TabIndex = 11;
            this.label10.Text = "Toll (Virtual Cash):";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(278, 288);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(133, 23);
            this.label11.TabIndex = 12;
            this.label11.Text = "Balance:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(278, 315);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(133, 23);
            this.label12.TabIndex = 13;
            this.label12.Text = "Last IP Addr Used:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(242, 342);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(169, 23);
            this.label13.TabIndex = 14;
            this.label13.Text = "Last Mac Addr Used:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(278, 369);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(133, 23);
            this.label14.TabIndex = 15;
            this.label14.Text = "Last Server Played:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(278, 396);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(133, 23);
            this.label15.TabIndex = 16;
            this.label15.Text = "Secret Question:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(278, 423);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(133, 23);
            this.label16.TabIndex = 17;
            this.label16.Text = "Secret Answer:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAcctName
            // 
            this.txtAcctName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAcctName.Location = new System.Drawing.Point(417, 45);
            this.txtAcctName.Name = "txtAcctName";
            this.txtAcctName.Size = new System.Drawing.Size(139, 22);
            this.txtAcctName.TabIndex = 18;
            this.txtAcctName.TextChanged += new System.EventHandler(this.general_TextChanged);
            // 
            // txtId
            // 
            this.txtId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtId.Location = new System.Drawing.Point(417, 18);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(90, 22);
            this.txtId.TabIndex = 19;
            this.txtId.TextChanged += new System.EventHandler(this.general_TextChanged);
            // 
            // txtPWHash
            // 
            this.txtPWHash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPWHash.Location = new System.Drawing.Point(417, 72);
            this.txtPWHash.Name = "txtPWHash";
            this.txtPWHash.ReadOnly = true;
            this.txtPWHash.Size = new System.Drawing.Size(325, 22);
            this.txtPWHash.TabIndex = 20;
            this.txtPWHash.TextChanged += new System.EventHandler(this.general_TextChanged);
            // 
            // btnPwdChange
            // 
            this.btnPwdChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPwdChange.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnPwdChange.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnPwdChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPwdChange.Location = new System.Drawing.Point(755, 69);
            this.btnPwdChange.Name = "btnPwdChange";
            this.btnPwdChange.Size = new System.Drawing.Size(92, 29);
            this.btnPwdChange.TabIndex = 21;
            this.btnPwdChange.Text = "Change";
            this.btnPwdChange.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnPwdChange.UseVisualStyleBackColor = true;
            this.btnPwdChange.Click += new System.EventHandler(this.btnPwdChange_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Location = new System.Drawing.Point(417, 99);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(220, 22);
            this.txtEmail.TabIndex = 22;
            this.txtEmail.TextChanged += new System.EventHandler(this.general_TextChanged);
            // 
            // chkActivated
            // 
            this.chkActivated.AutoSize = true;
            this.chkActivated.Location = new System.Drawing.Point(417, 126);
            this.chkActivated.Name = "chkActivated";
            this.chkActivated.Size = new System.Drawing.Size(78, 21);
            this.chkActivated.TabIndex = 23;
            this.chkActivated.Text = "Inactive";
            this.chkActivated.UseVisualStyleBackColor = true;
            this.chkActivated.CheckedChanged += new System.EventHandler(this.chkActivated_CheckedChanged);
            // 
            // cbxAccessLvl
            // 
            this.cbxAccessLvl.FormattingEnabled = true;
            this.cbxAccessLvl.Location = new System.Drawing.Point(417, 148);
            this.cbxAccessLvl.Name = "cbxAccessLvl";
            this.cbxAccessLvl.Size = new System.Drawing.Size(163, 24);
            this.cbxAccessLvl.TabIndex = 24;
            this.cbxAccessLvl.SelectedIndexChanged += new System.EventHandler(this.general_SelectedIndexChanged);
            // 
            // cbxMembership
            // 
            this.cbxMembership.FormattingEnabled = true;
            this.cbxMembership.Location = new System.Drawing.Point(417, 177);
            this.cbxMembership.Name = "cbxMembership";
            this.cbxMembership.Size = new System.Drawing.Size(139, 24);
            this.cbxMembership.TabIndex = 25;
            this.cbxMembership.SelectedIndexChanged += new System.EventHandler(this.general_SelectedIndexChanged);
            // 
            // cbxPMembership
            // 
            this.cbxPMembership.FormattingEnabled = true;
            this.cbxPMembership.Location = new System.Drawing.Point(417, 206);
            this.cbxPMembership.Name = "cbxPMembership";
            this.cbxPMembership.Size = new System.Drawing.Size(139, 24);
            this.cbxPMembership.TabIndex = 26;
            this.cbxPMembership.SelectedIndexChanged += new System.EventHandler(this.general_SelectedIndexChanged);
            // 
            // dpExpiry
            // 
            this.dpExpiry.Location = new System.Drawing.Point(417, 234);
            this.dpExpiry.Name = "dpExpiry";
            this.dpExpiry.Size = new System.Drawing.Size(249, 22);
            this.dpExpiry.TabIndex = 27;
            this.dpExpiry.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // txtTollEdit
            // 
            this.txtTollEdit.Location = new System.Drawing.Point(417, 262);
            this.txtTollEdit.Name = "txtTollEdit";
            this.txtTollEdit.ShortcutsEnabled = false;
            this.txtTollEdit.Size = new System.Drawing.Size(139, 22);
            this.txtTollEdit.TabIndex = 28;
            this.txtTollEdit.TextChanged += new System.EventHandler(this.general_TextChanged);
            this.txtTollEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTollEdit_KeyPress);
            // 
            // btnApply
            // 
            this.btnApply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnApply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Location = new System.Drawing.Point(562, 260);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(104, 30);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Update Toll";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // txtBalance
            // 
            this.txtBalance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBalance.Location = new System.Drawing.Point(417, 289);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.ReadOnly = true;
            this.txtBalance.Size = new System.Drawing.Size(139, 22);
            this.txtBalance.TabIndex = 30;
            this.txtBalance.TextChanged += new System.EventHandler(this.general_TextChanged);
            // 
            // txtLastIP
            // 
            this.txtLastIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLastIP.Location = new System.Drawing.Point(417, 316);
            this.txtLastIP.Name = "txtLastIP";
            this.txtLastIP.ReadOnly = true;
            this.txtLastIP.Size = new System.Drawing.Size(139, 22);
            this.txtLastIP.TabIndex = 31;
            // 
            // txtLastMac
            // 
            this.txtLastMac.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLastMac.Location = new System.Drawing.Point(417, 343);
            this.txtLastMac.Name = "txtLastMac";
            this.txtLastMac.ReadOnly = true;
            this.txtLastMac.Size = new System.Drawing.Size(139, 22);
            this.txtLastMac.TabIndex = 32;
            // 
            // txtLastSvr
            // 
            this.txtLastSvr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLastSvr.Location = new System.Drawing.Point(417, 370);
            this.txtLastSvr.Name = "txtLastSvr";
            this.txtLastSvr.ReadOnly = true;
            this.txtLastSvr.Size = new System.Drawing.Size(44, 22);
            this.txtLastSvr.TabIndex = 33;
            // 
            // txtQues
            // 
            this.txtQues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQues.Location = new System.Drawing.Point(417, 397);
            this.txtQues.Name = "txtQues";
            this.txtQues.Size = new System.Drawing.Size(430, 22);
            this.txtQues.TabIndex = 34;
            this.txtQues.TextChanged += new System.EventHandler(this.general_TextChanged);
            // 
            // txtAns
            // 
            this.txtAns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAns.Location = new System.Drawing.Point(417, 423);
            this.txtAns.Name = "txtAns";
            this.txtAns.Size = new System.Drawing.Size(430, 22);
            this.txtAns.TabIndex = 35;
            this.txtAns.TextChanged += new System.EventHandler(this.general_TextChanged);
            // 
            // btnChars
            // 
            this.btnChars.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnChars.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnChars.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChars.Location = new System.Drawing.Point(417, 451);
            this.btnChars.Name = "btnChars";
            this.btnChars.Size = new System.Drawing.Size(137, 31);
            this.btnChars.TabIndex = 36;
            this.btnChars.Text = "Characters";
            this.btnChars.UseVisualStyleBackColor = true;
            // 
            // btnBAN
            // 
            this.btnBAN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBAN.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnBAN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnBAN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBAN.ForeColor = System.Drawing.Color.Maroon;
            this.btnBAN.Location = new System.Drawing.Point(252, 503);
            this.btnBAN.Name = "btnBAN";
            this.btnBAN.Size = new System.Drawing.Size(159, 31);
            this.btnBAN.TabIndex = 37;
            this.btnBAN.Text = "BAN Account";
            this.btnBAN.UseVisualStyleBackColor = true;
            this.btnBAN.Click += new System.EventHandler(this.btnBAN_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUndo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnUndo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndo.Location = new System.Drawing.Point(432, 503);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(159, 31);
            this.btnUndo.TabIndex = 38;
            this.btnUndo.Text = "Undo";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackgroundImage = global::ASM31.Properties.Resources.button_exit;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(792, 495);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(54, 47);
            this.btnExit.TabIndex = 40;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtHasExpiry
            // 
            this.txtHasExpiry.ForeColor = System.Drawing.Color.Maroon;
            this.txtHasExpiry.Location = new System.Drawing.Point(672, 234);
            this.txtHasExpiry.Name = "txtHasExpiry";
            this.txtHasExpiry.Size = new System.Drawing.Size(105, 23);
            this.txtHasExpiry.TabIndex = 41;
            this.txtHasExpiry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.ForeColor = System.Drawing.Color.OliveDrab;
            this.btnUpdate.Location = new System.Drawing.Point(612, 503);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(159, 31);
            this.btnUpdate.TabIndex = 43;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtToll
            // 
            this.txtToll.Location = new System.Drawing.Point(675, 264);
            this.txtToll.Name = "txtToll";
            this.txtToll.ReadOnly = true;
            this.txtToll.ShortcutsEnabled = false;
            this.txtToll.Size = new System.Drawing.Size(172, 22);
            this.txtToll.TabIndex = 44;
            // 
            // btnDateClear
            // 
            this.btnDateClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDateClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnDateClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDateClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDateClear.Location = new System.Drawing.Point(783, 231);
            this.btnDateClear.Name = "btnDateClear";
            this.btnDateClear.Size = new System.Drawing.Size(63, 29);
            this.btnDateClear.TabIndex = 45;
            this.btnDateClear.Text = "Clear";
            this.btnDateClear.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDateClear.UseVisualStyleBackColor = true;
            this.btnDateClear.Click += new System.EventHandler(this.btnDateClear_Click);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(278, 72);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(133, 23);
            this.label17.TabIndex = 3;
            this.label17.Text = "Password Hash:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AcctMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 552);
            this.Controls.Add(this.btnDateClear);
            this.Controls.Add(this.txtToll);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtHasExpiry);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnUndo);
            this.Controls.Add(this.btnBAN);
            this.Controls.Add(this.btnChars);
            this.Controls.Add(this.txtAns);
            this.Controls.Add(this.txtQues);
            this.Controls.Add(this.txtLastSvr);
            this.Controls.Add(this.txtLastMac);
            this.Controls.Add(this.txtLastIP);
            this.Controls.Add(this.txtBalance);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.txtTollEdit);
            this.Controls.Add(this.dpExpiry);
            this.Controls.Add(this.cbxPMembership);
            this.Controls.Add(this.cbxMembership);
            this.Controls.Add(this.cbxAccessLvl);
            this.Controls.Add(this.chkActivated);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnPwdChange);
            this.Controls.Add(this.txtPWHash);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.txtAcctName);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AcctMgr";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Account Manager";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListBox acctList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtAcctName;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtPWHash;
        private System.Windows.Forms.Button btnPwdChange;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.CheckBox chkActivated;
        private System.Windows.Forms.ComboBox cbxAccessLvl;
        private System.Windows.Forms.ComboBox cbxMembership;
        private System.Windows.Forms.ComboBox cbxPMembership;
        private System.Windows.Forms.DateTimePicker dpExpiry;
        private System.Windows.Forms.TextBox txtTollEdit;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.TextBox txtLastIP;
        private System.Windows.Forms.TextBox txtLastMac;
        private System.Windows.Forms.TextBox txtLastSvr;
        private System.Windows.Forms.TextBox txtQues;
        private System.Windows.Forms.TextBox txtAns;
        private System.Windows.Forms.Button btnChars;
        private System.Windows.Forms.Button btnBAN;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label txtHasExpiry;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtToll;
        private System.Windows.Forms.Button btnDateClear;
        private System.Windows.Forms.Label label17;
    }
}