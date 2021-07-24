
namespace FriendChatApp.Forms.Friends
{
    partial class FriendConfigForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.DisplayNameInput = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PortInput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.HostInput = new System.Windows.Forms.TextBox();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataKeyMethod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.userDataKeyBtn = new System.Windows.Forms.Button();
            this.dataKeyPath = new System.Windows.Forms.TextBox();
            this.signKeyMethod = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.userSignKeyBtn = new System.Windows.Forms.Button();
            this.signKeyPath = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.DisplayNameInput);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 142);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Display";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Display Name";
            // 
            // DisplayNameInput
            // 
            this.DisplayNameInput.Location = new System.Drawing.Point(6, 31);
            this.DisplayNameInput.Name = "DisplayNameInput";
            this.DisplayNameInput.Size = new System.Drawing.Size(186, 20);
            this.DisplayNameInput.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.PortInput);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.HostInput);
            this.groupBox2.Location = new System.Drawing.Point(328, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(318, 142);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Network";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Port";
            // 
            // PortInput
            // 
            this.PortInput.Location = new System.Drawing.Point(6, 90);
            this.PortInput.Name = "PortInput";
            this.PortInput.Size = new System.Drawing.Size(186, 20);
            this.PortInput.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Host";
            // 
            // HostInput
            // 
            this.HostInput.Location = new System.Drawing.Point(6, 31);
            this.HostInput.Name = "HostInput";
            this.HostInput.Size = new System.Drawing.Size(186, 20);
            this.HostInput.TabIndex = 5;
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(490, 308);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveBtn.TabIndex = 2;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(571, 308);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.signKeyMethod);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.userSignKeyBtn);
            this.groupBox3.Controls.Add(this.signKeyPath);
            this.groupBox3.Controls.Add(this.dataKeyMethod);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.userDataKeyBtn);
            this.groupBox3.Controls.Add(this.dataKeyPath);
            this.groupBox3.Location = new System.Drawing.Point(12, 160);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(634, 138);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Keys";
            // 
            // dataKeyMethod
            // 
            this.dataKeyMethod.FormattingEnabled = true;
            this.dataKeyMethod.Location = new System.Drawing.Point(507, 44);
            this.dataKeyMethod.Name = "dataKeyMethod";
            this.dataKeyMethod.Size = new System.Drawing.Size(121, 21);
            this.dataKeyMethod.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "User Public Data Key";
            // 
            // userDataKeyBtn
            // 
            this.userDataKeyBtn.Location = new System.Drawing.Point(339, 42);
            this.userDataKeyBtn.Name = "userDataKeyBtn";
            this.userDataKeyBtn.Size = new System.Drawing.Size(75, 23);
            this.userDataKeyBtn.TabIndex = 6;
            this.userDataKeyBtn.Text = "Select";
            this.userDataKeyBtn.UseVisualStyleBackColor = true;
            this.userDataKeyBtn.Click += new System.EventHandler(this.userDataKeyBtn_Click);
            // 
            // dataKeyPath
            // 
            this.dataKeyPath.Location = new System.Drawing.Point(6, 44);
            this.dataKeyPath.Name = "dataKeyPath";
            this.dataKeyPath.Size = new System.Drawing.Size(327, 20);
            this.dataKeyPath.TabIndex = 5;
            // 
            // signKeyMethod
            // 
            this.signKeyMethod.FormattingEnabled = true;
            this.signKeyMethod.Location = new System.Drawing.Point(507, 104);
            this.signKeyMethod.Name = "signKeyMethod";
            this.signKeyMethod.Size = new System.Drawing.Size(121, 21);
            this.signKeyMethod.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "User Public Sign Key";
            // 
            // userSignKeyBtn
            // 
            this.userSignKeyBtn.Location = new System.Drawing.Point(339, 102);
            this.userSignKeyBtn.Name = "userSignKeyBtn";
            this.userSignKeyBtn.Size = new System.Drawing.Size(75, 23);
            this.userSignKeyBtn.TabIndex = 10;
            this.userSignKeyBtn.Text = "Select";
            this.userSignKeyBtn.UseVisualStyleBackColor = true;
            this.userSignKeyBtn.Click += new System.EventHandler(this.userSignKeyBtn_Click);
            // 
            // signKeyPath
            // 
            this.signKeyPath.Location = new System.Drawing.Point(6, 104);
            this.signKeyPath.Name = "signKeyPath";
            this.signKeyPath.Size = new System.Drawing.Size(327, 20);
            this.signKeyPath.TabIndex = 9;
            // 
            // FriendConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 343);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FriendConfigForm";
            this.Text = "Friend";
            this.Load += new System.EventHandler(this.FriendConfigForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DisplayNameInput;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox PortInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox HostInput;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button userDataKeyBtn;
        private System.Windows.Forms.TextBox dataKeyPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox dataKeyMethod;
        private System.Windows.Forms.ComboBox signKeyMethod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button userSignKeyBtn;
        private System.Windows.Forms.TextBox signKeyPath;
    }
}