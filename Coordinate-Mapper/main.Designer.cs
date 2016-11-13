namespace Coordinate_Mapper
{
    partial class main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.btn_selectFile = new System.Windows.Forms.Button();
            this.btn_runScript = new System.Windows.Forms.Button();
            this.lbl_selectedFilelbl = new System.Windows.Forms.Label();
            this.lbl_selectedFile = new System.Windows.Forms.Label();
            this.tb_results = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_selectFile
            // 
            this.btn_selectFile.Location = new System.Drawing.Point(13, 13);
            this.btn_selectFile.Name = "btn_selectFile";
            this.btn_selectFile.Size = new System.Drawing.Size(75, 23);
            this.btn_selectFile.TabIndex = 0;
            this.btn_selectFile.Text = "Select File";
            this.btn_selectFile.UseVisualStyleBackColor = true;
            this.btn_selectFile.Click += new System.EventHandler(this.btn_selectFile_Click);
            // 
            // btn_runScript
            // 
            this.btn_runScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_runScript.Location = new System.Drawing.Point(249, 13);
            this.btn_runScript.Name = "btn_runScript";
            this.btn_runScript.Size = new System.Drawing.Size(75, 23);
            this.btn_runScript.TabIndex = 1;
            this.btn_runScript.Text = "Run";
            this.btn_runScript.UseVisualStyleBackColor = true;
            this.btn_runScript.Click += new System.EventHandler(this.btn_runScript_Click);
            // 
            // lbl_selectedFilelbl
            // 
            this.lbl_selectedFilelbl.AutoSize = true;
            this.lbl_selectedFilelbl.Location = new System.Drawing.Point(13, 43);
            this.lbl_selectedFilelbl.Name = "lbl_selectedFilelbl";
            this.lbl_selectedFilelbl.Size = new System.Drawing.Size(71, 13);
            this.lbl_selectedFilelbl.TabIndex = 2;
            this.lbl_selectedFilelbl.Text = "Selected File:";
            // 
            // lbl_selectedFile
            // 
            this.lbl_selectedFile.AutoSize = true;
            this.lbl_selectedFile.Location = new System.Drawing.Point(90, 43);
            this.lbl_selectedFile.Name = "lbl_selectedFile";
            this.lbl_selectedFile.Size = new System.Drawing.Size(35, 13);
            this.lbl_selectedFile.TabIndex = 3;
            this.lbl_selectedFile.Text = "label2";
            this.lbl_selectedFile.TextChanged += new System.EventHandler(this.lbl_selectedFile_TextChanged);
            // 
            // tb_results
            // 
            this.tb_results.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_results.Location = new System.Drawing.Point(13, 60);
            this.tb_results.Multiline = true;
            this.tb_results.Name = "tb_results";
            this.tb_results.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_results.Size = new System.Drawing.Size(311, 239);
            this.tb_results.TabIndex = 4;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 311);
            this.Controls.Add(this.tb_results);
            this.Controls.Add(this.lbl_selectedFile);
            this.Controls.Add(this.lbl_selectedFilelbl);
            this.Controls.Add(this.btn_runScript);
            this.Controls.Add(this.btn_selectFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(352, 350);
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Coordinate Mapper";
            this.Load += new System.EventHandler(this.main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_selectFile;
        private System.Windows.Forms.Button btn_runScript;
        private System.Windows.Forms.Label lbl_selectedFilelbl;
        private System.Windows.Forms.Label lbl_selectedFile;
        private System.Windows.Forms.TextBox tb_results;
    }
}

