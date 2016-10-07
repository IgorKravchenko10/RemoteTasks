using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProxyClasses
{
    public class ProxyUtils
    {
        public static string ProjectName()
        {
            return Assembly.GetExecutingAssembly().GetName().FullName;
        }
        /// <summary>
        /// Возвращает список дней недели
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDaysOfWeek()
        {
            System.Globalization.CultureInfo currentCulture = new System.Globalization.CultureInfo("ru");
            System.Threading.Thread.CurrentThread.CurrentUICulture = currentCulture;

            CultureInfo current = System.Threading.Thread.CurrentThread.CurrentUICulture;
            List<string> myDays = current.DateTimeFormat.DayNames.ToList();
            if (current.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Monday)
            {
                myDays.Add(DateTimeFormatInfo.CurrentInfo.GetDayName(DayOfWeek.Sunday));
                myDays.RemoveAt(0);
            }
            return myDays;
        }

        public static string GetNameDayOfWeek(int dayOfWeek)
        {
            if (dayOfWeek < 1)
            {
                dayOfWeek = 1;
            }

            if (dayOfWeek > 7)
            {
                dayOfWeek = 7;
            }

            List<string> daysOfWeek = GetDaysOfWeek();
            return daysOfWeek[dayOfWeek - 1];
        }

        public static void SaveTasksToFile(List<PrxTask> myList, string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(myList.GetType());
            using (StreamWriter streamWriter = File.CreateText(fileName))
            {
                xmlSerializer.Serialize(streamWriter, myList);
            }
        }

        public static List<PrxTask> ReadTasksFromFile(string fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<PrxTask>));
            using (Stream readStream = File.Open(fileName, FileMode.Open))
            {
                return (List<PrxTask>)xs.Deserialize(readStream);
            }
        }
    }
}
