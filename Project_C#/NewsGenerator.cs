using Projekt_PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_
{
    public class Newsgenerator
    {
        private List<MyMedia> myMediaList;
        private List<IReportable> myobjectList;
        private int mediaIndex = 0; // Counter of media
        private int objectIndex = 0; // Counter of object

        public Newsgenerator(List<MyMedia> myMediaList, List<IReportable> myobjectList)
        {
            this.myMediaList = myMediaList;
            this.myobjectList = myobjectList;
        }

        public string GenerateNextNews()
        {
            // Check if some message left
            if (mediaIndex >= myMediaList.Count || objectIndex >= myobjectList.Count)
            {
                return null;
            }

            // Take a next couple
            MyMedia currentMedia = myMediaList[mediaIndex];
            IReportable currentObject = myobjectList[objectIndex];

            // Generate Message
            string news = currentObject.Accept(currentMedia);

            // Go to the next pair
            mediaIndex++;
            if (mediaIndex >= myMediaList.Count)
            {
                mediaIndex = 0;
                objectIndex++;
            }

            return news;
        }
    }
}
