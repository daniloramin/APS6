namespace APS6Sem
{
    partial class Form1
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
            this.pboxWebcam = new System.Windows.Forms.PictureBox();
            this.btnWebcam = new System.Windows.Forms.Button();
            this.btnDetectarRostos = new System.Windows.Forms.Button();
            this.tboxNome = new System.Windows.Forms.TextBox();
            this.btnTreinar = new System.Windows.Forms.Button();
            this.pboxRosto = new System.Windows.Forms.PictureBox();
            this.btnAdicionarRosto = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pboxWebcam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxRosto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // picCapture
            // 
            this.pboxWebcam.Location = new System.Drawing.Point(5, 12);
            this.pboxWebcam.Name = "picCapture";
            this.pboxWebcam.Size = new System.Drawing.Size(451, 270);
            this.pboxWebcam.TabIndex = 0;
            this.pboxWebcam.TabStop = false;
            // 
            // btnCapture
            // 
            this.btnWebcam.Location = new System.Drawing.Point(5, 299);
            this.btnWebcam.Name = "btnCapture";
            this.btnWebcam.Size = new System.Drawing.Size(123, 48);
            this.btnWebcam.TabIndex = 1;
            this.btnWebcam.Text = "WebCam";
            this.btnWebcam.UseVisualStyleBackColor = true;
            this.btnWebcam.Click += new System.EventHandler(this.btnWebcam_Click);
            // 
            // btnDetectFaces
            // 
            this.btnDetectarRostos.Location = new System.Drawing.Point(173, 299);
            this.btnDetectarRostos.Name = "btnDetectFaces";
            this.btnDetectarRostos.Size = new System.Drawing.Size(123, 48);
            this.btnDetectarRostos.TabIndex = 2;
            this.btnDetectarRostos.Text = "Detectar Rostos";
            this.btnDetectarRostos.UseVisualStyleBackColor = true;
            this.btnDetectarRostos.Click += new System.EventHandler(this.btnDetectarRostos_Click);
            // 
            // txtPersonName
            // 
            this.tboxNome.Location = new System.Drawing.Point(485, 28);
            this.tboxNome.Name = "txtPersonName";
            this.tboxNome.Size = new System.Drawing.Size(164, 20);
            this.tboxNome.TabIndex = 3;
            // 
            // btnTrain
            // 
            this.btnTreinar.Location = new System.Drawing.Point(527, 299);
            this.btnTreinar.Name = "btnTrain";
            this.btnTreinar.Size = new System.Drawing.Size(122, 48);
            this.btnTreinar.TabIndex = 5;
            this.btnTreinar.Text = "Treinar Imagens";
            this.btnTreinar.UseVisualStyleBackColor = true;
            this.btnTreinar.Click += new System.EventHandler(this.btnTreinar_Click);
            // 
            // picDetected
            // 
            this.pboxRosto.Location = new System.Drawing.Point(485, 54);
            this.pboxRosto.Name = "picDetected";
            this.pboxRosto.Size = new System.Drawing.Size(164, 123);
            this.pboxRosto.TabIndex = 7;
            this.pboxRosto.TabStop = false;
            // 
            // btnAddPerson
            // 
            this.btnAdicionarRosto.Location = new System.Drawing.Point(348, 299);
            this.btnAdicionarRosto.Name = "btnAddPerson";
            this.btnAdicionarRosto.Size = new System.Drawing.Size(123, 48);
            this.btnAdicionarRosto.TabIndex = 9;
            this.btnAdicionarRosto.Text = "Adicionar Rosto";
            this.btnAdicionarRosto.UseVisualStyleBackColor = true;
            this.btnAdicionarRosto.Click += new System.EventHandler(this.btnAddRosto_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(485, 183);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(79, 99);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(570, 183);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(79, 99);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(487, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Nome";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 355);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnAdicionarRosto);
            this.Controls.Add(this.pboxRosto);
            this.Controls.Add(this.btnTreinar);
            this.Controls.Add(this.tboxNome);
            this.Controls.Add(this.btnDetectarRostos);
            this.Controls.Add(this.btnWebcam);
            this.Controls.Add(this.pboxWebcam);
            this.Name = "Form1";
            this.Text = "Simple Face Recognition App";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pboxWebcam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxRosto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pboxWebcam;
        private System.Windows.Forms.Button btnWebcam;
        private System.Windows.Forms.Button btnDetectarRostos;
        private System.Windows.Forms.TextBox tboxNome;
        private System.Windows.Forms.Button btnTreinar;
        private System.Windows.Forms.PictureBox pboxRosto;
        private System.Windows.Forms.Button btnAdicionarRosto;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
    }
}

