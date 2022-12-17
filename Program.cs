namespace Lab8._2
{
    class Program
    {
        public static List<Data> listOfData = new List<Data> { };
        public static string? saveOfOperation;

        public static int totalMoney;
        public static int crutch = 0;
        public static int count = 0;
        static void Main(string[] args)
        {
            ReadFile();
            IsDataCorrect();
            InputData();
            Console.ReadKey();
        }

        public static void IsDataCorrect()
        {
            int phantomMoney = totalMoney;
            foreach (var item in listOfData) PerformTheOperation(item.action, item.money);
            
            if (totalMoney >= 0)
                Console.WriteLine("Data is correct...");
            else
                Console.WriteLine("Data is wrong....");

            totalMoney = phantomMoney;
        }

        public static void ReadFile()
        {
            foreach (var item in File.ReadLines("extract.txt")) 
            {
                string[] dataFromLine = item.Split("|");
                ConvertToClass(dataFromLine);
            }
        }

        public static void InputData()
        {
            Console.WriteLine("Format: 2021-06-01 12:00");
            string inputStr = Console.ReadLine();

            if (inputStr.Length == 0)
            {
                GetFullResult();
                return;
            }

            string[] inputDate = inputStr.Split(" ");
            SearchRecordInData(inputDate);
        }

        public static void ConvertToClass(string[] dataFromLine)
        {
            Data data = new Data();

            if (int.TryParse(dataFromLine[0], out int money))
            {
                totalMoney = money;
                return;
            }

            string[] dateAndTime = dataFromLine[0].Split(" ");

            if (dataFromLine.Length == 2)
            {
                data.id = count++;
                data.date = dateAndTime[0];
                data.time = dateAndTime[1];
                data.action = dataFromLine[1].Trim();
            }
            else
            {
                data.id = count++;
                data.date = dateAndTime[0];
                data.time = dateAndTime[1];
                data.money = int.Parse(dataFromLine[1].Trim());
                data.action = dataFromLine[2].Trim();
            }
              

            listOfData.Add(data);
        }

        public static void PerformTheOperation(string? operation, int dirtyMoney)
        {
            if (dirtyMoney != 0) crutch = dirtyMoney;
            switch (operation)
            {
                case "in":
                    totalMoney += crutch;
                    saveOfOperation = "in";
                    break;
                case "out":
                    totalMoney -= crutch;
                    saveOfOperation = "out";
                    break;
                case "revert":
                    CancelTheOperation(crutch);
                    break;
                default:
                    Console.WriteLine("Operation not found");
                    break;
            }
        }

        public static void CancelTheOperation(int dirtyMoney)
        {
            switch (saveOfOperation)
            {
                case "in":
                    totalMoney -= dirtyMoney;
                    break;
                case "out":
                    totalMoney += dirtyMoney;
                    break;
                default:
                    Console.WriteLine("The operation could not be canceled");
                    break;
            }
        }

        public static void SearchRecordInData(string[] inputDate)
        {
            List<Data> extractedData = listOfData.FindAll(date => date.date == inputDate[0] && date.time == inputDate[1]);
            if (extractedData.Count > 1)
            {
                foreach (var item in extractedData)
                {
                    Console.WriteLine($"Id: {item.id} Date: {item.date} Time: {item.time} Money: {item.money} Action: {item.action}");
                }

                Console.Write("2 operations were found. Enter the operation number: ");
                int input = int.Parse(Console.ReadLine());

                foreach (var item in listOfData)
                {
                    if (item.id == input)
                    {
                        PerformTheOperation(item.action, item.money);
                        break;
                    }
                    PerformTheOperation(item.action, item.money);
                }
                Console.WriteLine("Your money: " + totalMoney);
            }
            else
            {
                foreach (var item in listOfData)
                {
                    if (item.date == inputDate[0] && item.time == inputDate[1])
                    {
                        PerformTheOperation(item.action, item.money);
                        break;
                    }
                    PerformTheOperation(item.action, item.money);
                }
                Console.WriteLine("Your money: " + totalMoney);
            }
        }
        
        public static void GetFullResult()
        {
            foreach (var item in listOfData)
            {
                PerformTheOperation(item.action, item.money);
            }
            Console.WriteLine("Your money: " + totalMoney);
        }
    }
}