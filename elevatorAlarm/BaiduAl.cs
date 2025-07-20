using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevatorAlarm
{
    class BaiduAl
    {

        //冬奥电梯视频String filenameAlarm = "D:\\0Aproject\\dongaoVideo\\v1.mp4";
        //static void Main(string[] args)
        //{
        //    String filenameAlarm = "E:\\困人智能检测程序和冬奥视频\\延庆冬奥运营中心中区1-能用.mp4";

        //    var capture = new VideoCapture(filenameAlarm);

        //    Mat image = new Mat();

        //    int sum = 0;
        //    while (capture.Read(image))
        //    {

        //        if (image.Empty())
        //            break;

        //        if (sum == 30)
        //        {
        //            //放大视频图像
        //            float scale = 0.5f;
        //            float width = image.Width;
        //            float height = image.Height;
        //            Cv2.Resize(image, image, new OpenCvSharp.Size(width * scale, height * scale));

        //            Cv2.ImShow(" video", image);

        //            BaiduAl baiduAl = new BaiduAl();
        //            String[] bodyNum = { "", "", "0" };
        //            bodyNum = baiduAl.BodyNum(bodyNum, image);
        //            sum = 0;
        //            Console.WriteLine(bodyNum[2] + "人");
        //            Cv2.WaitKey(20);
        //        }
        //        sum++;
        //    }

        //}


        public string[] BodyNum(string[] bodyNum, Mat video)
        {
            if (video != null)
            {
                Bitmap map = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(video);
                //video = OpenCvSharp.Extensions.BitmapConverter.ToMat(map);
                byte[] image = BitmapToBytes(map);
                bodyNum[2] = BodyNumDemo(bodyNum[0], bodyNum[1], image);
               //  Console.WriteLine("检测结果为：" + bodyNum[2]+"@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

            }
            else
            {
                bodyNum[2] = "-1";
            }
            return bodyNum;
        }
        //String app_id, String api_key, String secret_key, String str
        public String BodyNumDemo(String api_key, String secret_key, byte[] image)
        {
            // 设置APPID/AK/SK

            //var APP_ID = "";
            //var API_KEY = "";
            //var SECRET_KEY = "";


            var API_KEY = api_key;
            var SECRET_KEY = secret_key;

            var client = new Baidu.Aip.BodyAnalysis.Body(API_KEY, SECRET_KEY);
            client.Timeout = 60000;  // 修改超时时间
                                     // var image = File.ReadAllBytes(str);//重要代码！！！！！！！！！！！！！！！！！！！！！！！！！！！！





            // 调用人流量统计，可能会抛出网络等异常，请使用try/catch捕获
            String res = null;
            try
            {
                var result = client.BodyNum(image);
                res = result["person_num"].ToString();
            }
            catch (Exception ex)
            {
                res = "-1";//
                Console.WriteLine(ex.Message);
            }

            //Console.WriteLine(result["person_num"].ToString());
            // 如果有可选参数
            //var options = new Dictionary<string, object>{
            //{"area", "0,0,100,100,200,200"},
            //{"show", "false"}
            // };
            // 带参数调用人流量统计
            // result = client.BodyNum(image, options);
            // Console.WriteLine(result);

            //Cv2.ImShow("video", video);
            //Cv2.WaitKey();
            return res;
        }



        //Bitmap转byte[]    
        public byte[] BitmapToBytes(Bitmap bitmap)
        {
            MemoryStream ms = null;
            byte[] bytes = null;
            try
            {

                ms = new MemoryStream();
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                bytes = ms.GetBuffer();
                return bytes;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return bytes;
            }
            finally
            {
                ms.Close();
            }
        }
    }
}
