namespace BackOfficeFenicio
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnImportarArquivoCnpj = new System.Windows.Forms.Button();
            this.txtDadosLer = new System.Windows.Forms.TextBox();
            this.txt2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnImportarArquivoCnpj
            // 
            this.btnImportarArquivoCnpj.Location = new System.Drawing.Point(31, 37);
            this.btnImportarArquivoCnpj.Name = "btnImportarArquivoCnpj";
            this.btnImportarArquivoCnpj.Size = new System.Drawing.Size(75, 23);
            this.btnImportarArquivoCnpj.TabIndex = 0;
            this.btnImportarArquivoCnpj.Text = "Importar";
            this.btnImportarArquivoCnpj.UseVisualStyleBackColor = true;
            this.btnImportarArquivoCnpj.Click += new System.EventHandler(this.btnImportarArquivoCnpj_Click);
            // 
            // txtDadosLer
            // 
            this.txtDadosLer.Location = new System.Drawing.Point(12, 95);
            this.txtDadosLer.Multiline = true;
            this.txtDadosLer.Name = "txtDadosLer";
            this.txtDadosLer.Size = new System.Drawing.Size(792, 492);
            this.txtDadosLer.TabIndex = 1;
            // 
            // txt2
            // 
            this.txt2.Location = new System.Drawing.Point(892, 95);
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(100, 20);
            this.txt2.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1388, 781);
            this.Controls.Add(this.txt2);
            this.Controls.Add(this.txtDadosLer);
            this.Controls.Add(this.btnImportarArquivoCnpj);
            this.Name = "Form1";
            this.Text = "xdxd";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImportarArquivoCnpj;
        private System.Windows.Forms.TextBox txtDadosLer;
        private System.Windows.Forms.TextBox txt2;
    }
}

