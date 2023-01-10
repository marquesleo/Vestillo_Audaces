namespace Vestillo.Lib
{
    partial class frmProcessamento
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
            this.prgProcessando = new System.Windows.Forms.ProgressBar();
            this.lblProcessamento = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // prgProcessando
            // 
            this.prgProcessando.Location = new System.Drawing.Point(35, 35);
            this.prgProcessando.MarqueeAnimationSpeed = 40;
            this.prgProcessando.Name = "prgProcessando";
            this.prgProcessando.Size = new System.Drawing.Size(438, 34);
            this.prgProcessando.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prgProcessando.TabIndex = 0;
            // 
            // lblProcessamento
            // 
            this.lblProcessamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessamento.Location = new System.Drawing.Point(35, 8);
            this.lblProcessamento.Name = "lblProcessamento";
            this.lblProcessamento.Size = new System.Drawing.Size(438, 24);
            this.lblProcessamento.TabIndex = 1;
            this.lblProcessamento.Text = "Carregando..";
            this.lblProcessamento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmProcessamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 95);
            this.Controls.Add(this.lblProcessamento);
            this.Controls.Add(this.prgProcessando);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmProcessamento";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmProcessamento";
            this.Load += new System.EventHandler(this.frmProcessamento_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblProcessamento;
        public System.Windows.Forms.ProgressBar prgProcessando;
    }
}