﻿namespace ShaderAttack
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            OpenProcess = new Button();
            ServiceSwitch = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // OpenProcess
            // 
            OpenProcess.Location = new Point(12, 12);
            OpenProcess.Name = "OpenProcess";
            OpenProcess.Size = new Size(776, 34);
            OpenProcess.TabIndex = 0;
            OpenProcess.Text = "附加进程";
            OpenProcess.UseVisualStyleBackColor = true;
            OpenProcess.Click += OpenProcess_Click;
            // 
            // ServiceSwitch
            // 
            ServiceSwitch.Enabled = false;
            ServiceSwitch.Location = new Point(12, 52);
            ServiceSwitch.Name = "ServiceSwitch";
            ServiceSwitch.Size = new Size(776, 34);
            ServiceSwitch.TabIndex = 2;
            ServiceSwitch.Text = "启动服务";
            ServiceSwitch.UseVisualStyleBackColor = true;
            ServiceSwitch.Click += ServiceSwitch_Click;
            // 
            // label1
            // 
            label1.Location = new Point(12, 101);
            label1.Name = "label1";
            label1.Size = new Size(776, 70);
            label1.TabIndex = 3;
            label1.Text = "输入数字后按下回车即可修改生命，按“Q”一键满能量，按“Shift+Q”可解决部分安全软件导致修改失败的问题";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 180);
            Controls.Add(label1);
            Controls.Add(ServiceSwitch);
            Controls.Add(OpenProcess);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "EMSACheater-琴梨梨";
            ResumeLayout(false);
        }

        #endregion

        private Button OpenProcess;
        private Button ServiceSwitch;
        private Label label1;
    }
}