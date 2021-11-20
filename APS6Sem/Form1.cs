using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Face;
using Emgu.CV.CvEnum;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace APS6Sem
{
    public partial class Form1 : Form
    {
        #region Variáveis do Programa

        int id = 0;
        private Capture capturarVideo = null;
        private Image<Bgr, Byte> frameAtual = null;
        Mat quadro = new Mat();
        private bool deteccaoDeRostosHabilitada = false;
        CascadeClassifier classificadorFacial = new CascadeClassifier("haarcascade_frontalface_alt.xml");
        Image<Bgr, Byte> resultadoDeRosto = null;
        List<Image<Gray, Byte>> rostosTreinados = new List<Image<Gray, byte>>();
        List<int> pessoas = new List<int>();

        bool SalvarImagem = false;
        private bool  imgTreinada = false;
        EigenFaceRecognizer reconhecedor;
        List<string> nomeDePessoas = new List<string>();

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void btnWebcam_Click(object sender, EventArgs e)
        {
            if (capturarVideo != null) capturarVideo.Dispose();
            capturarVideo = new Capture();
            Application.Idle += ProcessarFrame;
        }

        private void ProcessarFrame(object sender, EventArgs e)
        {
            if (capturarVideo != null && capturarVideo.Ptr != IntPtr.Zero)
            {
                capturarVideo.Retrieve(quadro, 0);
                frameAtual = quadro.ToImage<Bgr, Byte>().Resize(pboxWebcam.Width, pboxWebcam.Height, Inter.Cubic);

                if (deteccaoDeRostosHabilitada)
                {
                    Mat imagemCinza = new Mat();
                    CvInvoke.CvtColor(frameAtual, imagemCinza, ColorConversion.Bgr2Gray);
                    
                    CvInvoke.EqualizeHist(imagemCinza, imagemCinza);

                    Rectangle[] rostos = classificadorFacial.DetectMultiScale(imagemCinza, 1.1, 3, Size.Empty, Size.Empty);
                    
                    if (rostos.Length > 0)
                    {

                        foreach (var rosto in rostos)
                        {
                            Image<Bgr, Byte> imagemResultante = frameAtual.Convert<Bgr, Byte>();
                            imagemResultante.ROI = rosto;
                            pboxRosto.SizeMode = PictureBoxSizeMode.StretchImage;
                            pboxRosto.Image = imagemResultante.Bitmap;

                            if (SalvarImagem)
                            {
                                string path = Directory.GetCurrentDirectory() + @"\TrainedImages";
                                if (!Directory.Exists(path))
                                    Directory.CreateDirectory(path);
                                Task.Factory.StartNew(() => {
                                    for (int i = 0; i < 10; i++)
                                    {
                                        imagemResultante.Resize(200, 200, Inter.Cubic).Save(path + @"\" + tboxNome.Text +"_"+ DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss") + ".jpg");
                                        Thread.Sleep(1000);
                                    }
                                });

                            }
                            SalvarImagem = false;

                            if (btnAdicionarRosto.InvokeRequired)
                            {
                                btnAdicionarRosto.Invoke(new ThreadStart(delegate {
                                    btnAdicionarRosto.Enabled = true;
                                }));
                            }

                            if (imgTreinada)
                            {
                                Image<Gray, Byte> resultadoCinza = imagemResultante.Convert<Gray, Byte>().Resize(200,200,Inter.Cubic);
                                CvInvoke.EqualizeHist(resultadoCinza,resultadoCinza);
                                var resultado = reconhecedor.Predict(resultadoCinza);
                                pictureBox1.Image = resultadoCinza.Bitmap;
                                pictureBox2.Image = rostosTreinados[resultado.Label].Bitmap;
                                Debug.WriteLine(resultado.Label+". "+resultado.Distance);
                                if (resultado.Label != -1 && resultado.Distance < 2000)
                                {
                                    CvInvoke.PutText(frameAtual, nomeDePessoas[resultado.Label], new Point(rosto.X - 2, rosto.Y - 2),
                                        FontFace.HersheyComplex, 1.0, new Bgr(Color.Orange).MCvScalar);
                                    CvInvoke.Rectangle(frameAtual, rosto, new Bgr(Color.Green).MCvScalar, 2);
                                }
                                else
                                {
                                    CvInvoke.PutText(frameAtual, "Unknown", new Point(rosto.X - 2, rosto.Y - 2),
                                        FontFace.HersheyComplex, 1.0, new Bgr(Color.Orange).MCvScalar);
                                    CvInvoke.Rectangle(frameAtual, rosto, new Bgr(Color.Red).MCvScalar, 2);

                                }
                            }
                        }
                    }
                }

                pboxWebcam.Image = frameAtual.Bitmap;
            }

            if (frameAtual != null)
                frameAtual.Dispose();
        }

        private void btnDetectarRostos_Click(object sender, EventArgs e)
        {
            deteccaoDeRostosHabilitada = true;
        }

        private void btnAddRosto_Click(object sender, EventArgs e)
        {
            btnAdicionarRosto.Enabled = false;
            SalvarImagem = true;
        }

        private void btnTreinar_Click(object sender, EventArgs e)
        {
            TreinarImagens();
        }
        private bool TreinarImagens()
        {
            int contadorImagens = 0;
            double limiar = 2000;
            rostosTreinados.Clear();
            pessoas.Clear();
            nomeDePessoas.Clear();
            try
            {
                string path = Directory.GetCurrentDirectory() + @"\TrainedImages";
                string[] files = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    Image<Gray, byte> trainedImage = new Image<Gray, byte>(file).Resize(200,200,Inter.Cubic);
                    CvInvoke.EqualizeHist(trainedImage,trainedImage);
                    rostosTreinados.Add(trainedImage);
                    pessoas.Add(contadorImagens);
                    string name = file.Split('\\').Last().Split('_')[0];
                    nomeDePessoas.Add(name);
                    contadorImagens++;
                    Debug.WriteLine(contadorImagens + ". " +name);

                }

                if (rostosTreinados.Count() > 0)
                {
                    reconhecedor = new EigenFaceRecognizer(contadorImagens, limiar);
                    reconhecedor.Train(rostosTreinados.ToArray(), pessoas.ToArray());

                    imgTreinada = true;
                    return true;
                }
                else
                {
                    imgTreinada = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                imgTreinada = false;                
                MessageBox.Show("Error in Train Images: " + ex.Message);
                return false;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
