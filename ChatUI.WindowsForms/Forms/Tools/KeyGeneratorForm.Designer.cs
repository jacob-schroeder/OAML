
namespace FriendChatApp.Forms.Tools
{
    partial class KeyGeneratorForm
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
            this.generateDataKeysBtn = new System.Windows.Forms.Button();
            this.generateSignKeysBtn = new System.Windows.Forms.Button();
            this.dataKeyTypes = new System.Windows.Forms.ComboBox();
            this.signKeyTypes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // generateDataKeysBtn
            // 
            this.generateDataKeysBtn.Location = new System.Drawing.Point(12, 12);
            this.generateDataKeysBtn.Name = "generateDataKeysBtn";
            this.generateDataKeysBtn.Size = new System.Drawing.Size(140, 23);
            this.generateDataKeysBtn.TabIndex = 0;
            this.generateDataKeysBtn.Text = "Generate Data Key(s)";
            this.generateDataKeysBtn.UseVisualStyleBackColor = true;
            this.generateDataKeysBtn.Click += new System.EventHandler(this.generateDataKeysBtn_Click);
            // 
            // generateSignKeysBtn
            // 
            this.generateSignKeysBtn.Location = new System.Drawing.Point(12, 62);
            this.generateSignKeysBtn.Name = "generateSignKeysBtn";
            this.generateSignKeysBtn.Size = new System.Drawing.Size(140, 23);
            this.generateSignKeysBtn.TabIndex = 1;
            this.generateSignKeysBtn.Text = "Generate Sign. Key(s)";
            this.generateSignKeysBtn.UseVisualStyleBackColor = true;
            this.generateSignKeysBtn.Click += new System.EventHandler(this.generateSignKeysBtn_Click);
            // 
            // dataKeyTypes
            // 
            this.dataKeyTypes.FormattingEnabled = true;
            this.dataKeyTypes.Location = new System.Drawing.Point(158, 14);
            this.dataKeyTypes.Name = "dataKeyTypes";
            this.dataKeyTypes.Size = new System.Drawing.Size(140, 21);
            this.dataKeyTypes.TabIndex = 2;
            // 
            // signKeyTypes
            // 
            this.signKeyTypes.FormattingEnabled = true;
            this.signKeyTypes.Location = new System.Drawing.Point(158, 64);
            this.signKeyTypes.Name = "signKeyTypes";
            this.signKeyTypes.Size = new System.Drawing.Size(140, 21);
            this.signKeyTypes.TabIndex = 3;
            // 
            // KeyGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 97);
            this.Controls.Add(this.signKeyTypes);
            this.Controls.Add(this.dataKeyTypes);
            this.Controls.Add(this.generateSignKeysBtn);
            this.Controls.Add(this.generateDataKeysBtn);
            this.Name = "KeyGeneratorForm";
            this.Text = "Key Generator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button generateDataKeysBtn;
        private System.Windows.Forms.Button generateSignKeysBtn;
        private System.Windows.Forms.ComboBox dataKeyTypes;
        private System.Windows.Forms.ComboBox signKeyTypes;
    }
}