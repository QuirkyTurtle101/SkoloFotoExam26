using Microsoft.AspNetCore.Routing.Constraints;

namespace SkoloFotoExam26.Models
{
    public class Photo
    {
        #region Properties
        public int PhotoID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public double Price { get; set; }
        public DateTime TheDate { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public PhotoType PhotoType { get; set; }

        //public Booking TheBooking { get; set; }
        #endregion

        #region Constructor
        public Photo()
        {
            
        }
        public Photo(string fileName, string filePath, double price, DateTime theDate, int height, int width, PhotoType photoType
           )
        {
            FileName = fileName;
            FilePath = filePath;
            Price = price;
            TheDate = theDate;
            Height = height;
            Width = width;
            PhotoType = photoType;
            //TheBooking = theBooking;
        }
        public Photo(int photoID, string fileName, string filePath, double price, DateTime theDate, int height, int width, 
            PhotoType photoType)
        {
            PhotoID = photoID;
            FileName = fileName;
            FilePath = filePath;
            Price = price;
            TheDate = theDate;
            Height = height;
            Width = width;
            PhotoType = photoType;
            //TheBooking = theBooking;
        }

        #endregion

    }
}
