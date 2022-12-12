namespace Lab8._2
{
    //динамические пути убрать
    class Program
    {
        public static List<Data> listOfData = new List<Data> { };
        public static string totalMoney;
        static void Main(string[] args)
        {
            ReadFile();
            foreach (var item in listOfData)
            {
                Console.WriteLine("Date: {0} Time: {1} Money: {2} Action: {3}", item.date, item.time, item.money, item.action);
            }
        }

        public static void ReadFile()
        {
            char[] separators = {'|'};
            foreach (var item in File.ReadLines("C:\\Users\\Lutin\\Desktop\\Lab5\\Lab8.2\\extract.txt")) 
            {
                string[] dataFromLine = item.Split(separators);
                ConvertToClass(dataFromLine);
            }
        }

        public static void ConvertToClass(string[] dataFromLine)
        {
            Data data = new Data();

            if (int.TryParse(dataFromLine[0], out _))
            {
                totalMoney = dataFromLine[0];
                return;
            }

            string[] dateAndTime = dataFromLine[0].Split(" ");

            if (dataFromLine.Length == 2)
            {
                data.date = dateAndTime[0];
                data.time = dateAndTime[1];
                data.action = dataFromLine[1].Trim();
            }
            else
            {
                data.date = dateAndTime[0];
                data.time = dateAndTime[1];
                data.money = dataFromLine[1].Trim();
                data.action = dataFromLine[2].Trim();
            }
              

            listOfData.Add(data);
        }

        public void IsCorrect()
        {

        }
    }
}