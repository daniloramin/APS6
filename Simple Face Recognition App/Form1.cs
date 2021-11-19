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

namespace Simple_Face_Recognition_App
{
    public partial class Form1 : Form
    {
        #region Variables
        int id = 0;
        private Capture capturarVideo = null;
        private Image<Bgr, Byte> frameAtual = null;
        Mat quadro = new Mat();
        private bool deteccaoDeRostosHabilitada = false;
        CascadeClassifier classificadorFacial = new CascadeClassifier(@"C:\Users\danil\Desktop\Trabalhos\APS\Simple-Face-Recognition-App-CS\Simple Face Recognition App\haarcascade_frontalface_alt.xml");
        Image<Bgr, Byte> resultadoDeRosto = null;
        List<Image<Gray, Byte>> rostosTreinados = new List<Image<Gray, byte>>();
        List<int> Pessoas = new List<int>();

        bool EnableSaveImage = false;
        private bool  imgTreinada = false;
        EigenFaceRecognizer recognizer;
        List<string> NomeDePessoas = new List<string>();

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (capturarVideo != null) capturarVideo.Dispose();
            capturarVideo = new Capture();
            Application.Idle += ProcessFrame;
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            if (capturarVideo != null && capturarVideo.Ptr != IntPtr.Zero)
            {
                capturarVideo.Retrieve(quadro, 0);
                frameAtual = quadro.ToImage<Bgr, Byte>().Resize(picCapture.Width, picCapture.Height, Inter.Cubic);

                if (deteccaoDeRostosHabilitada)
                {
                    Mat grayImage = new Mat();
                    CvInvoke.CvtColor(frameAtual, grayImage, ColorConversion.Bgr2Gray);
                    
                    CvInvoke.EqualizeHist(grayImage, grayImage);

                    Rectangle[] faces = classificadorFacial.DetectMultiScale(grayImage, 1.1, 3, Size.Empty, Size.Empty);
                    
                    if (faces.Length > 0)
                    {

                        foreach (var face in faces)
                        {
                            Image<Bgr, Byte> resultImage = frameAtual.Convert<Bgr, Byte>();
                            resultImage.ROI = face;
                            picDetected.SizeMode = PictureBoxSizeMode.StretchImage;
                            picDetected.Image = resultImage.Bitmap;

                            if (EnableSaveImage)
                            {
                                string path = Directory.GetCurrentDirectory() + @"\TrainedImages";
                                if (!Directory.Exists(path))
                                    Directory.CreateDirectory(path);
                                Task.Factory.StartNew(() => {
                                    for (int i = 0; i < 10; i++)
                                    {
                                        resultImage.Resize(200, 200, Inter.Cubic).Save(path + @"\" + txtPersonName.Text +"_"+ DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss") + ".jpg");
                                        Thread.Sleep(1000);
                                    }
                                });

                            }
                            EnableSaveImage = false;

                            if (btnAddPerson.InvokeRequired)
                            {
                                btnAddPerson.Invoke(new ThreadStart(delegate {
                                    btnAddPerson.Enabled = true;
                                }));
                            }

                            if (imgTreinada)
                            {
                                Image<Gray, Byte> grayFaceResult = resultImage.Convert<Gray, Byte>().Resize(200,200,Inter.Cubic);
                                CvInvoke.EqualizeHist(grayFaceResult,grayFaceResult);
                                var result = recognizer.Predict(grayFaceResult);
                                pictureBox1.Image = grayFaceResult.Bitmap;
                                pictureBox2.Image = rostosTreinados[result.Label].Bitmap;
                                Debug.WriteLine(result.Label+". "+result.Distance);
                                if (result.Label != -1 && result.Distance < 2000)
                                {
                                    CvInvoke.PutText(frameAtual, NomeDePessoas[result.Label], new Point(face.X - 2, face.Y - 2),
                                        FontFace.HersheyComplex, 1.0, new Bgr(Color.Orange).MCvScalar);
                                    CvInvoke.Rectangle(frameAtual, face, new Bgr(Color.Green).MCvScalar, 2);
                                }
                                else
                                {
                                    CvInvoke.PutText(frameAtual, "Unknown", new Point(face.X - 2, face.Y - 2),
                                        FontFace.HersheyComplex, 1.0, new Bgr(Color.Orange).MCvScalar);
                                    CvInvoke.Rectangle(frameAtual, face, new Bgr(Color.Red).MCvScalar, 2);

                                }
                            }
                        }
                    }
                }

                picCapture.Image = frameAtual.Bitmap;
            }

            if (frameAtual != null)
                frameAtual.Dispose();
        }

        private void btnDetectFaces_Click(object sender, EventArgs e)
        {
            deteccaoDeRostosHabilitada = true;
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            btnAddPerson.Enabled = false;
            EnableSaveImage = true;
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            TrainImagesFromDir();
        }
        private bool TrainImagesFromDir()
        {
            int ImagesCount = 0;
            double Threshold = 2000;
            rostosTreinados.Clear();
            Pessoas.Clear();
            NomeDePessoas.Clear();
            try
            {
                string path = Directory.GetCurrentDirectory() + @"\TrainedImages";
                string[] files = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    Image<Gray, byte> trainedImage = new Image<Gray, byte>(file).Resize(200,200,Inter.Cubic);
                    CvInvoke.EqualizeHist(trainedImage,trainedImage);
                    rostosTreinados.Add(trainedImage);
                    Pessoas.Add(ImagesCount);
                    string name = file.Split('\\').Last().Split('_')[0];
                    NomeDePessoas.Add(name);
                    ImagesCount++;
                    Debug.WriteLine(ImagesCount + ". " +name);

                }

                if (rostosTreinados.Count() > 0)
                {
                    recognizer = new EigenFaceRecognizer(ImagesCount, Threshold);
                    recognizer.Train(rostosTreinados.ToArray(), Pessoas.ToArray());

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
