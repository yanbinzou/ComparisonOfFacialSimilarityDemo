using FaceRecognitionDotNet;

namespace face
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //此文件夹路径下必须包含4个.dat文件 我这里使用的是   https://github.com/ageitgey/face_recognition_models
        //These models were created by Davis King and are licensed in the public domain or under CC0 1.0 Universal. See LICENSE.
        //路径请自己更改为自己的
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



                // 只取第一个人脸进行对比
                var faceEncoding1 = faceEncodings1.FirstOrDefault();
                var faceEncoding2 = faceEncodings2.FirstOrDefault();

                if (faceEncoding1 != null && faceEncoding2 != null)
                {
                    // 计算人脸相似度
                    var similarity = FaceRecognition.FaceDistance(faceEncoding1, faceEncoding2);

                    // 输出相似度
                    this.label1.Text = similarity.ToString();
                    //经过测试 小于0.42大概率为同一个人

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
            // 使用 OpenFileDialog 选择图片文件
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取所选的图片文件路径
                string[] fileNames = openFileDialog.FileNames;

                if (fileNames.Length >= 2)
                {
                    // 释放先前的图片资源
                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }
                    if (pictureBox2.Image != null)
                    {
                        pictureBox2.Image.Dispose();
                    }



                    // 读取并在 PictureBox 中显示第一张图片
                    System.Drawing.Image image1 = System.Drawing.Image.FromFile(fileNames[0]);
                    pictureBox1.Image = image1;
                    imagePath1 = fileNames[0];

                    // 读取并在 PictureBox 中显示第二张图片
                    System.Drawing.Image image2 = System.Drawing.Image.FromFile(fileNames[1]);
                    pictureBox2.Image = image2;
                    imagePath2 = fileNames[1];
                }
            }
        }
    }
}