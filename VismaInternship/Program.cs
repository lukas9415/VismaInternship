// See https://aka.ms/new-console-template for more information
using VismaInternship;
using VismaInternship.Models;
using System.Text.Json;
using Newtonsoft.Json;

List<Meeting> meetings = new List<Meeting>();

Console.Title = "VismaInternship";
Console.ForegroundColor = ConsoleColor.Blue;
// ________________________________________________________

using (StreamReader r = new StreamReader("test.json"))
{
    string json = r.ReadToEnd();
    List<Meeting> itemstest = JsonConvert.DeserializeObject<List<Meeting>>(json);
}

void menu()
{
    //zuvim
}

while (true)
{
    Console.WriteLine("\t [1] Create a new meeting");
    Console.WriteLine("\t [2] Delete a meeeting");
    Console.WriteLine("\t [3] Add a person to the meeting");
    Console.WriteLine("\t [4] Remove a person from the meeting");
    Console.WriteLine("\t [5] List all meetings");
    Console.WriteLine("\t your choice: ");
    string str = Console.ReadLine();
    int nr;
    bool isNumeric = int.TryParse(str, out nr);

    switch (nr)
    {
        case 1:
            System.Console.Clear();
            createMeeting();
            break;
        case 2:
            Console.Clear();
            deleteMeeting();
            break;
        default:
            Console.Clear();
            Console.WriteLine("Option does not exist\n");
            break;
    }
    //Console.ReadKey();
}

//--Create a meeting, maybe add so you can exit...
void createMeeting()
{
    Meeting meeting = new Meeting();
    Person person = new Person();
    Console.Write("Creating a new meeting\n");
    Console.Write("What is the name of the meeting:\t");
    meeting.Name = Console.ReadLine();
    System.Console.Clear();

    Console.Write("Who is responsible for the meeting\nFirst Name:\t");
    person.Name = Console.ReadLine();
    System.Console.Clear();
    Console.Write("Who is responsible for the meeting\nLast Name:\t");
    person.Surname = Console.ReadLine();
    person.isResponsible = true;
    System.Console.Clear();
    meeting.ResponsiblePerson = person;

    Console.Write("Description for the meeting:\t");
    meeting.Description = Console.ReadLine();
    System.Console.Clear();

    //Category select
    bool checkCategory = true;
    while(checkCategory)
    {
        Console.Write("Category:\n");
        Console.WriteLine("\t [1] Codemonkey");
        Console.WriteLine("\t [2] Hub");
        Console.WriteLine("\t [3] Short");
        Console.WriteLine("\t [4] TeamBuilding");

        string str = Console.ReadLine();
        int nr;
        bool isNumeric = int.TryParse(str, out nr);

        switch (nr)
        {
            case 1:
                meeting.Category = "Codemonkey";
                checkCategory = false;
                break;
            case 2:
                meeting.Category = "Hub";
                checkCategory = false;
                break;
            case 3:
                meeting.Category = "Short";
                checkCategory = false;
                break;
            case 4:
                meeting.Category = "TeamBuilding";
                checkCategory = false;
                break;
            default:
                Console.Clear();
                Console.WriteLine("Category does not exist, select again\n");
                break;
        }
    }
    Console.Clear();

    //Type select
    bool checkType = true;
    while (checkType)
    {
        Console.Write("Type:\n");
        Console.WriteLine("\t [1] Live");
        Console.WriteLine("\t [2] InPerson");

        string str = Console.ReadLine();
        int nr;
        bool isNumeric = int.TryParse(str, out nr);

        switch (nr)
        {
            case 1:
                meeting.Type = "Live";
                checkType = false;
                break;
            case 2:
                meeting.Type = "InPerson";
                checkType = false;
                break;
            default:
                Console.Clear();
                Console.WriteLine("Type does not exist, select again\n");
                break;
        }
    }
    Console.Clear();

    bool checkStartDate = false;
    {
        while (checkStartDate == false)
        {
            //Start date select
            Console.Write("Start date (YYYY-MM-dd HH:mm format):\t");
            string date = Console.ReadLine();
            DateTime finalDate;
            if (DateTime.TryParse(date, out finalDate) == true)
            {
                checkStartDate = true;
                meeting.StartDate = DateTime.Parse(date);
            }
            else
            {
                System.Console.Clear();
                Console.WriteLine("Wrong date format\n");
            }
        }
    }
    Console.Clear();

    bool checkEndDate = false;
    {
        while (checkEndDate == false)
        {
            //Start date select
            Console.Write("Meeting StartDate: " + meeting.StartDate + "\n");
            Console.Write("End date (YYYY-MM-dd HH:mm format):\t");
            string date = Console.ReadLine();
            DateTime finalDate;
            if (DateTime.TryParse(date, out finalDate) == true)
            {
                checkEndDate = true;
                meeting.EndDate = DateTime.Parse(date);
            }
            else
            {
                System.Console.Clear();
                Console.WriteLine("Wrong date format\n");
            }
        }
    }
    Console.Clear();

    meeting.Load(person);
    meetings.Add(meeting);

    Console.WriteLine("Created a new meeting\n");
    Console.WriteLine("Name of the meeting: \t" + meeting.Name);
    Console.WriteLine("Responsible for the meeting: \t" + meeting.ResponsiblePerson.Name + " " + meeting.ResponsiblePerson.Surname);
    Console.WriteLine("Description: \t" + meeting.Description);
    Console.WriteLine("Category: \t" + meeting.Category);
    Console.WriteLine("Type: \t" + meeting.Type);
    Console.WriteLine("StartDate: \t" + meeting.StartDate);
    Console.WriteLine("EndDate: \t" + meeting.EndDate);
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
    Console.Clear();


    //Change this for testing only rn
    JsonSerializerSettings jss = new JsonSerializerSettings()
    {
        TypeNameHandling = TypeNameHandling.Auto
    };
    string serializedCollection = JsonConvert.SerializeObject(meetings, jss);

    File.WriteAllText("test.json", serializedCollection);

}


//Only the person responsible can delete the meeting ADD THIS -------------------------------------------
void deleteMeeting()
{
    //Check if there are any meetings in the system
    if (meetings.Count == 0)
    {
        Console.WriteLine("There are no meetings available to delete");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    else
    {
        while (true)
        {
            Console.WriteLine("Meeting deletion.\nSelect which meeting would you want to delete");
            int i = 1;
            foreach (Meeting meeting in meetings)
            {
                Console.WriteLine("\t[" + i + "]" + meeting.Name);
            }
            Console.WriteLine("\t your choice: ");

            string str = Console.ReadLine();
            int nr;
            bool isNumeric = int.TryParse(str, out nr);

            if (isNumeric)
            {
                if (nr <= meetings.Count && nr > 0)
                {
                    //Reik pridet check
                    meetings.RemoveAt(nr - 1);
                    Console.Clear();
                    Console.WriteLine("Meeting succesfully removed");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Meeting does not exist, check your choice\n");
                }
            }

            else
            {
                Console.Clear();
                Console.WriteLine("Please enter the meeting number correctly\n");
            }
        }

    }
}