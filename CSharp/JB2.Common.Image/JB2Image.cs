using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public class JB2Image : JB2.Common.IImage
    {
        private System.Drawing.Image _image;

        #region Contructors
        public JB2Image()
        {

        }

        private JB2Image(byte[] imageContent)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imageContent);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);

            _image = returnImage;
        }


        private JB2Image(System.IO.MemoryStream stream)
        {
            _image = System.Drawing.Image.FromStream(stream);
        }

        private JB2Image(System.Drawing.Image image)
        {
            _image = image;
        }

        #endregion

        public int Height
        {
            get
            {
                return _image.Height;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Width {
            get
            {
                return _image.Width;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Url
        {
            get; set;
        }

        public byte[] FileContent
        {
            get
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                this._image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #region Public Methods

        public System.Drawing.Image ToImage()
        {

            return (System.Drawing.Image)this;

        }

        public JB2Image GetThumbnail(int width, int height)
        {
            System.Drawing.Image thumbnailImage = this._image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(this.ThumbnailCallback), IntPtr.Zero);

            System.IO.MemoryStream memStream = new System.IO.MemoryStream();

            thumbnailImage.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            var result = new JB2Image(thumbnailImage);


            return result;
            //// create an image object, using the filename we just retrieved
            //System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(file));

            //// create the actual thumbnail image
            //System.Drawing.Image thumbnailImage = image.GetThumbnailImage(64, 64, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

            //// make a memory stream to work with the image bytes
            //MemoryStream imageStream = new MemoryStream();

            //// put the image into the memory stream
            //thumbnailImage.Save(imageStream, System.Drawing.Imaging.Imageformat.Jpeg);

            //// make byte array the same size as the image
            //byte[] imageContent = new Byte[imageStream.Length];

            //// rewind the memory stream
            //imageStream.Position = 0;

            //// load the byte array with the image
            //imageStream.Read(imageContent, 0, (int)imageStream.Length);
        }


        public JB2Image ConvertFormat(Enum.ImageFormatType formatType)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();


            //System.Drawing.Imaging.ImageFormat format = 
            switch(formatType)
            {
                case Enum.ImageFormatType.Bmp:
                    this._image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case Enum.ImageFormatType.Gif:
                    this._image.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case Enum.ImageFormatType.Icon:
                    this._image.Save(stream, System.Drawing.Imaging.ImageFormat.Icon);
                    break;
                case Enum.ImageFormatType.Jpeg:
                    this._image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
            }
            //System.Drawing.Image newImage = System.Drawing.Image.FromStream(stream);

            return new JB2Image(stream);
        }


        #endregion Public Methods

        private bool ThumbnailCallback()
        {
            return true;
        }

        #region Static members

        static public JB2Image FromFile(string filename)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(filename);

            var result = new JB2Image(image);

            return result;

        }

        static public JB2Image FromUrl(string url)
        {

            var webClient = new System.Net.WebClient();
            byte[] imageBytes = webClient.DownloadData(url);

            //System.IO.MemoryStream ms = new MemoryStream(imageBtyes);

            JB2Image result = new JB2Image(imageBytes);



            return result;

        }


        #endregion Static members


        #region implicit operators

        public static implicit operator System.Drawing.Image(JB2Image rhs)
        {
            return rhs._image;
        }

        public static implicit operator JB2Image(System.Drawing.Image rhs)
        {
            var result = new JB2Image(rhs);

            return result;
        }



        #endregion
    }

}


