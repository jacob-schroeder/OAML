
namespace FriendChatApp.Forms.Tools
{
    partial class AppOptionsForm
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
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SaveFormBtn = new System.Windows.Forms.Button();
            this.CancelFormBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataKeyMethod = new System.Windows.Forms.ComboBox();
            this.userDataKeyBtn = new System.Windows.Forms.Button();
            this.dataKeyPath = new System.Windows.Forms.TextBox();
            this.signKeyMethod = new System.Windows.Forms.ComboBox();
            this.userSignKeyBtn = new System.Windows.Forms.Button();
            this.signKeyPath = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(159, 137);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(9, 47);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(126, 20);
            this.textBox2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port";
            // 
            // SaveFormBtn
            // 
            this.SaveFormBtn.Location = new System.Drawing.Point(496, 307);
            this.SaveFormBtn.Name = "SaveFormBtn";
            this.SaveFormBtn.Size = new System.Drawing.Size(75, 23);
            this.SaveFormBtn.TabIndex = 1;
            this.SaveFormBtn.Text = "Save";
            this.SaveFormBtn.UseVisualStyleBackColor = true;
            this.SaveFormBtn.Click += new System.EventHandler(this.SaveFormBtn_Click);
            // 
            // CancelFormBtn
            // 
            this.CancelFormBtn.Location = new System.Drawing.Point(577, 307);
            this.CancelFormBtn.Name = "CancelFormBtn";
            this.CancelFormBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelFormBtn.TabIndex = 2;
            this.CancelFormBtn.Text = "Cancel";
            this.CancelFormBtn.UseVisualStyleBackColor = true;
            this.CancelFormBtn.Click += new System.EventHandler(this.CancelFormBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.signKeyMethod);
            this.groupBox2.Controls.Add(this.userSignKeyBtn);
            this.groupBox2.Controls.Add(this.signKeyPath);
            this.groupBox2.Controls.Add(this.dataKeyMethod);
            this.groupBox2.Controls.Add(this.userDataKeyBtn);
            this.groupBox2.Controls.Add(this.dataKeyPath);
            this.groupBox2.Location = new System.Drawing.Point(12, 155);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(640, 146);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data and Signature Keys";
            // 
            // dataKeyMethod
            // 
            this.dataKeyMethod.FormattingEnabled = true;
            this.dataKeyMethod.Location = new System.Drawing.Point(507, 34);
            this.dataKeyMethod.Name = "dataKeyMethod";
            this.dataKeyMethod.Size = new System.Drawing.Size(121, 21);
            this.dataKeyMethod.TabIndex = 11;
            // 
            // userDataKeyBtn
            // 
            this.userDataKeyBtn.Location = new System.Drawing.Point(339, 32);
            this.userDataKeyBtn.Name = "userDataKeyBtn";
            this.userDataKeyBtn.Size = new System.Drawing.Size(75, 23);
            this.userDataKeyBtn.TabIndex = 10;
            this.userDataKeyBtn.Text = "Select";
            this.userDataKeyBtn.UseVisualStyleBackColor = true;
            this.userDataKeyBtn.Click += new System.EventHandler(this.userDataKeyBtn_Click);
            // 
            // dataKeyPath
            // 
            this.dataKeyPath.Location = new System.Drawing.Point(6, 34);
            this.dataKeyPath.Name = "dataKeyPath";
            this.dataKeyPath.Size = new System.Drawing.Size(327, 20);
            this.dataKeyPath.TabIndex = 9;
            // 
            // signKeyMethod
            // 
            this.signKeyMethod.FormattingEnabled = true;
            this.signKeyMethod.Location = new System.Drawing.Point(507, 91);
            this.signKeyMethod.Name = "signKeyMethod";
            this.signKeyMethod.Size = new System.Drawing.Size(121, 21);
            this.signKeyMethod.TabIndex = 14;
            // 
            // userSignKeyBtn
            // 
            this.userSignKeyBtn.Location = new System.Drawing.Point(339, 89);
            this.userSignKeyBtn.Name = "userSignKeyBtn";
            this.userSignKeyBtn.Size = new System.Drawing.Size(75, 23);
            this.userSignKeyBtn.TabIndex = 13;
            this.userSignKeyBtn.Text = "Select";
            this.userSignKeyBtn.UseVisualStyleBackColor = true;
            this.userSignKeyBtn.Click += new System.EventHandler(this.userSignKeyBtn_Click);
            // 
            // signKeyPath
            // 
            this.signKeyPath.Location = new System.Drawing.Point(6, 91);
            this.signKeyPath.Name = "signKeyPath";
            this.signKeyPath.Size = new System.Drawing.Size(327, 20);
            this.signKeyPath.TabIndex = 12;
            // 
            // AppOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(663, 342);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.CancelFormBtn);
            this.Controls.Add(this.SaveFormBtn);
            this.Controls.Add(this.groupBox1);
            this.Name = "AppOptionsForm";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.AppOptionsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button SaveFormBtn;
        private System.Windows.Forms.Button CancelFormBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox dataKeyMethod;
        private System.Windows.Forms.Button userDataKeyBtn;
        private System.Windows.Forms.TextBox dataKeyPath;
        private System.Windows.Forms.ComboBox signKeyMethod;
        private System.Windows.Forms.Button userSignKeyBtn;
        private System.Windows.Forms.TextBox signKeyPath;
    }
}