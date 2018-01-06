using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Common
{
    public class ImageAttachment : GenericAttachment
    {
        #region Fields
        private JB2.Common.JB2Image _jb2image;

        #endregion Fields

        #region Constructors

        public ImageAttachment(byte[] byteArray)
        {
            _jb2image = JB2Image.FromByteArray(byteArray);
        }

        #endregion Constructors



        public override bool IsImage
        {
            get
            {
                try
                {
                    var img = _jb2image.ToImage();
                    if (img != null)
                        return true;
                    else
                        return false;
                }
                catch(Exception ex)
                {
                    return false;
                }
               
            }
        }

        public override byte[] GetFileContent()
        {
            throw new NotImplementedException();
        }

        public override int GetFileSize()
        {
            throw new NotImplementedException();
        }
    }
}
