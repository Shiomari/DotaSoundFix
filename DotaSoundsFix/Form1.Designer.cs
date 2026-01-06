namespace DotaSoundsFix
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
            folderBrowserDialog1 = new FolderBrowserDialog();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            selectFolderButton = new Button();
            selectFolderLabel = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            createFixedVsndevtsButton = new Button();
            checkOldVoSoundsScriptButton = new Button();
            tableLayoutPanel5 = new TableLayoutPanel();
            fixSoundsCodecButton = new Button();
            setCs2PathButton = new Button();
            fixEmptySoundsButton = new Button();
            tableLayoutPanel4 = new TableLayoutPanel();
            fixNotIncludingSoundsButton = new Button();
            checkNotIncludingSoundsButton = new Button();
            getFixedWavFilesButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1063, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(selectFolderButton, 0, 0);
            tableLayoutPanel2.Controls.Add(selectFolderLabel, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(1057, 35);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // selectFolderButton
            // 
            selectFolderButton.Location = new Point(3, 3);
            selectFolderButton.Name = "selectFolderButton";
            selectFolderButton.Size = new Size(104, 29);
            selectFolderButton.TabIndex = 0;
            selectFolderButton.Text = "Select Folder";
            selectFolderButton.UseVisualStyleBackColor = true;
            selectFolderButton.Click += selectFolderButton_Click;
            // 
            // selectFolderLabel
            // 
            selectFolderLabel.AutoSize = true;
            selectFolderLabel.Dock = DockStyle.Fill;
            selectFolderLabel.Location = new Point(113, 0);
            selectFolderLabel.Name = "selectFolderLabel";
            selectFolderLabel.Size = new Size(941, 35);
            selectFolderLabel.TabIndex = 1;
            selectFolderLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel6, 0, 3);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel5, 0, 2);
            tableLayoutPanel3.Controls.Add(fixEmptySoundsButton, 0, 0);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 44);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 4;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1057, 403);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.AutoSize = true;
            tableLayoutPanel6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(createFixedVsndevtsButton, 1, 0);
            tableLayoutPanel6.Controls.Add(checkOldVoSoundsScriptButton, 0, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(0, 105);
            tableLayoutPanel6.Margin = new Padding(0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Size = new Size(1057, 298);
            tableLayoutPanel6.TabIndex = 4;
            // 
            // createFixedVsndevtsButton
            // 
            createFixedVsndevtsButton.Location = new Point(298, 3);
            createFixedVsndevtsButton.Name = "createFixedVsndevtsButton";
            createFixedVsndevtsButton.Size = new Size(289, 29);
            createFixedVsndevtsButton.TabIndex = 3;
            createFixedVsndevtsButton.Text = "Create Fixed Vsndevts";
            createFixedVsndevtsButton.UseVisualStyleBackColor = true;
            createFixedVsndevtsButton.Click += createFixedVsndevts_Click;
            // 
            // checkOldVoSoundsScriptButton
            // 
            checkOldVoSoundsScriptButton.Location = new Point(3, 3);
            checkOldVoSoundsScriptButton.Name = "checkOldVoSoundsScriptButton";
            checkOldVoSoundsScriptButton.Size = new Size(289, 29);
            checkOldVoSoundsScriptButton.TabIndex = 1;
            checkOldVoSoundsScriptButton.Text = "Check Old VO Sounds Script";
            checkOldVoSoundsScriptButton.UseVisualStyleBackColor = true;
            checkOldVoSoundsScriptButton.Click += checkOldVoSoundsScriptButton_Click;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.AutoSize = true;
            tableLayoutPanel5.ColumnCount = 3;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(getFixedWavFilesButton, 2, 0);
            tableLayoutPanel5.Controls.Add(fixSoundsCodecButton, 1, 0);
            tableLayoutPanel5.Controls.Add(setCs2PathButton, 0, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(0, 70);
            tableLayoutPanel5.Margin = new Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(1057, 35);
            tableLayoutPanel5.TabIndex = 3;
            // 
            // fixSoundsCodecButton
            // 
            fixSoundsCodecButton.Location = new Point(298, 3);
            fixSoundsCodecButton.Name = "fixSoundsCodecButton";
            fixSoundsCodecButton.Size = new Size(289, 29);
            fixSoundsCodecButton.TabIndex = 3;
            fixSoundsCodecButton.Text = "Fix Sounds Codec";
            fixSoundsCodecButton.UseVisualStyleBackColor = true;
            fixSoundsCodecButton.Click += fixSoundsCodecButton_Click;
            // 
            // setCs2PathButton
            // 
            setCs2PathButton.Location = new Point(3, 3);
            setCs2PathButton.Name = "setCs2PathButton";
            setCs2PathButton.Size = new Size(289, 29);
            setCs2PathButton.TabIndex = 1;
            setCs2PathButton.Text = "Set CS2 Path (only first launch)";
            setCs2PathButton.UseVisualStyleBackColor = true;
            setCs2PathButton.Click += setCs2PathButton_Click;
            // 
            // fixEmptySoundsButton
            // 
            fixEmptySoundsButton.Location = new Point(3, 3);
            fixEmptySoundsButton.Name = "fixEmptySoundsButton";
            fixEmptySoundsButton.Size = new Size(289, 29);
            fixEmptySoundsButton.TabIndex = 0;
            fixEmptySoundsButton.Text = "Fix Empty Sounds";
            fixEmptySoundsButton.UseVisualStyleBackColor = true;
            fixEmptySoundsButton.Click += fixEmptySoundsButton_Click;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(fixNotIncludingSoundsButton, 1, 0);
            tableLayoutPanel4.Controls.Add(checkNotIncludingSoundsButton, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(0, 35);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(1057, 35);
            tableLayoutPanel4.TabIndex = 2;
            // 
            // fixNotIncludingSoundsButton
            // 
            fixNotIncludingSoundsButton.Location = new Point(298, 3);
            fixNotIncludingSoundsButton.Name = "fixNotIncludingSoundsButton";
            fixNotIncludingSoundsButton.Size = new Size(289, 29);
            fixNotIncludingSoundsButton.TabIndex = 3;
            fixNotIncludingSoundsButton.Text = "Fix Not Including Sounds";
            fixNotIncludingSoundsButton.UseVisualStyleBackColor = true;
            fixNotIncludingSoundsButton.Click += fixNotIncludingSoundsButton_Click;
            // 
            // checkNotIncludingSoundsButton
            // 
            checkNotIncludingSoundsButton.Location = new Point(3, 3);
            checkNotIncludingSoundsButton.Name = "checkNotIncludingSoundsButton";
            checkNotIncludingSoundsButton.Size = new Size(289, 29);
            checkNotIncludingSoundsButton.TabIndex = 1;
            checkNotIncludingSoundsButton.Text = "Check Not Including Sounds";
            checkNotIncludingSoundsButton.UseVisualStyleBackColor = true;
            checkNotIncludingSoundsButton.Click += checkNotIncludingSoundsButton_Click;
            // 
            // getFixedWavFilesButton
            // 
            getFixedWavFilesButton.Location = new Point(593, 3);
            getFixedWavFilesButton.Name = "getFixedWavFilesButton";
            getFixedWavFilesButton.Size = new Size(289, 29);
            getFixedWavFilesButton.TabIndex = 4;
            getFixedWavFilesButton.Text = "Get Fixed Wav Files";
            getFixedWavFilesButton.UseVisualStyleBackColor = true;
            getFixedWavFilesButton.Click += getFixedWavFilesButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1063, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion


        private FolderBrowserDialog folderBrowserDialog1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button selectFolderButton;
        private Label selectFolderLabel;
        private TableLayoutPanel tableLayoutPanel3;
        private Button fixEmptySoundsButton;
        private Button checkNotIncludingSoundsButton;
        private TableLayoutPanel tableLayoutPanel4;
        private Button fixNotIncludingSoundsButton;
        private TableLayoutPanel tableLayoutPanel5;
        private Button fixSoundsCodecButton;
        private Button setCs2PathButton;
        private TableLayoutPanel tableLayoutPanel6;
        private Button createFixedVsndevtsButton;
        private Button checkOldVoSoundsScriptButton;
        private Button getFixedWavFilesButton;
    }
}
