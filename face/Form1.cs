using FaceRecognitionDotNet;

namespace face
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //���ļ���·���±������4��.dat�ļ� ������ʹ�õ���   https://github.com/ageitgey/face_recognition_models
        //These models were created by Davis King and are licensed in the public domain or under CC0 1.0 Universal. See LICENSE.
        //·�����Լ�����Ϊ�Լ���
        private static FaceRecognition _FaceRecognition = FaceRecognition.Create("C:\\Users\\admin\\Desktop\\face\\dat");
        private static string imagePath1 = string.Empty;
        private static string imagePath2 = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            var image1 = FaceRecognition.LoadImageFile(imagePath1);
            var image2 = FaceRecognition.LoadImageFile(imagePath2);

            try
            {
                var faceLocations1 = _FaceRecognition.FaceLocations(image1);
                var faceLocations2 = _FaceRecognition.FaceLocations(image2);

                var faceEncodings1 = _FaceRecognition.FaceEncodings(image1, faceLocations1);
                var faceEncodings2 = _FaceRecognition.FaceEncodings(image2, faceLocations2);



                // ֻȡ��һ���������жԱ�
                var faceEncoding1 = faceEncodings1.FirstOrDefault();
                var faceEncoding2 = faceEncodings2.FirstOrDefault();

                if (faceEncoding1 != null && faceEncoding2 != null)
                {
                    // �����������ƶ�
                    var similarity = FaceRecognition.FaceDistance(faceEncoding1, faceEncoding2);

                    // ������ƶ�
                    this.label1.Text = similarity.ToString();
                    //�������� С��0.42�����Ϊͬһ����

                }
            }
            catch (Exception ex)
            {
                this.label1.Text = ex.Message;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.label1.Text = "";
            // ʹ�� OpenFileDialog ѡ��ͼƬ�ļ�
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // ��ȡ��ѡ��ͼƬ�ļ�·��
                string[] fileNames = openFileDialog.FileNames;

                if (fileNames.Length >= 2)
                {
                    // �ͷ���ǰ��ͼƬ��Դ
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }
                    if (pictureBox2.Image != null)
                    {
                        pictureBox2.Image.Dispose();
                    }



                    // ��ȡ���� PictureBox ����ʾ��һ��ͼƬ
                    System.Drawing.Image image1 = System.Drawing.Image.FromFile(fileNames[0]);
                    pictureBox1.Image = image1;
                    imagePath1 = fileNames[0];

                    // ��ȡ���� PictureBox ����ʾ�ڶ���ͼƬ
                    System.Drawing.Image image2 = System.Drawing.Image.FromFile(fileNames[1]);
                    pictureBox2.Image = image2;
                    imagePath2 = fileNames[1];
                }
            }
        }
    }
}