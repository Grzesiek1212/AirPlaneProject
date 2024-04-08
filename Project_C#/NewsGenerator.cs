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
        private int mediaIndex = 0;
        private int objectIndex = 0;

        public Newsgenerator(List<MyMedia> myMediaList, List<IReportable> myobjectList)
        {
            this.myMediaList = myMediaList;
            this.myobjectList = myobjectList;
        }

        public string GenerateNextNews()
        {
            // Sprawdzamy, czy wszystkie pary zostały wyczerpane
            if (mediaIndex >= myMediaList.Count || objectIndex >= myobjectList.Count)
            {
                return null;
            }

            // Wybieramy kolejną parę
            MyMedia currentMedia = myMediaList[mediaIndex];
            IReportable currentObject = myobjectList[objectIndex];

            // Generujemy wiadomość
            string news = currentObject.Accept(currentMedia);

            // Przechodzimy do następnej pary
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
