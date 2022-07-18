// See https://aka.ms/new-console-template for more information
using VismaInternship;
using VismaInternship.Models;
using System.Text.Json;
using Newtonsoft.Json;

List<Meeting> meetings = new List<Meeting>();

Console.Title = "VismaInternship";
Console.ForegroundColor = ConsoleColor.Blue;
// ________________________________________________________

// Load data from JSON
using (StreamReader r = new StreamReader("MeetingData.json"))
{
    string json = r.ReadToEnd();
    meetings = JsonConvert.DeserializeObject<List<Meeting>>(json);
}


// -- Main menu here
while (true)
{
    Console.WriteLine("\t [1] Create a new meeting");
    Console.WriteLine("\t [2] Delete a meeeting");
    Console.WriteLine("\t [3] Add a person to the meeting");
    Console.WriteLine("\t [4] Remove a person from the meeting");
    Console.WriteLine("\t [5] List all meetings\n");
    Console.Write("\t your choice: ");
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
        case 3:
            Console.Clear();
            addPerson();
            break;
        case 4:
            Console.Clear();
            removePerson();
            break;
        case 5:
            Console.Clear();
            listAllMeetings();
            break;
        default:
            Console.Clear();
            Console.WriteLine("Option does not exist\n");
            break;
    }
    //Console.ReadKey();
}

//--Create a meeting, maybe add so you can exit... (Save added)
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

    //Save the added meet
    saveToJSON();
}


//Only the person responsible can delete the meeting... (Save added)
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
            Console.WriteLine("\t[0]Return to main menu\n");
            int i = 1;
            foreach (Meeting meeting in meetings)
            {
                Console.WriteLine("\t[" + i + "]" + meeting.Name);
                i++;
            }
            Console.WriteLine("\t your choice: \t");

            string str = Console.ReadLine();
            int nr;
            bool isNumeric = int.TryParse(str, out nr);

            if (isNumeric)
            {
                if (nr == 0)
                {
                    Console.Clear();
                    return;
                }
                if (nr <= meetings.Count && nr > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Currently removing meeting: " + meetings[nr-1].Name);
                    Console.WriteLine("To be able to remove the meeting, you must enter Surname of the person who is responsible.\n");
                    Console.WriteLine("Surname of the responsible person: \t");
                    string surname = Console.ReadLine();
                    if(meetings[nr-1].ResponsiblePerson.Surname == surname && meetings[nr-1].ResponsiblePerson.isResponsible == true)
                    {
                        meetings.RemoveAt(nr - 1);
                        Console.Clear();
                        
                        //Save data to json
                        saveToJSON();
                        Console.WriteLine("Meeting succesfully removed");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Person that has been entered is not responsible for the meeting.\nMeeting has not been removed");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                        return;
                    }

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

//Add a new person to the meeting... (Save added)
void addPerson()
{
    Person person = new Person();
    Console.Write("Adding a new person to a meeting. If you want to cancel, leave the name field blank and press enter.\n");
    Console.Write("What is the first name of the person:\t");
    string firstName = Console.ReadLine();
    if (firstName.Length == 0)
    {
        Console.Clear();
        return;
    }
    person.Name = firstName;

    Console.Clear();

    Console.Write("Adding a new person to a meeting. If you want to cancel, leave the surname field blank and press enter.\n");
    Console.Write("What is the surname of the person:\t");
    string lastName = Console.ReadLine();
    if (lastName.Length == 0)
    {
        Console.Clear();
        return;
    }
    person.Surname = lastName;

    Console.Clear();


    while (true)
    {
        Console.Write("Name: " + firstName + "\nSurname: " + lastName +"\n");
        Console.WriteLine("\nSelect which meeting would you like to add the person to.");
        Console.WriteLine("\t[0]Cancel and return to main menu\n");
        int i = 1;
        foreach (Meeting meeting in meetings)
        {
            Console.WriteLine("\t[" + i + "]" + meeting.Name);
            i++;
        }
        Console.Write("\t your choice: \t");

        string str = Console.ReadLine();
        int nr;
        bool isNumeric = int.TryParse(str, out nr);

        if (isNumeric)
        {
            if (nr == 0)
            {
                Console.Clear();
                return;
            }
            if (nr <= meetings.Count && nr > 0)
            {

                //Check if person does not exist in a meet
                if(meetings[nr-1].People.Exists(x => x.Name == firstName.ToString() && x.Surname == lastName.ToString()))
                {
                    Console.Clear();
                    Console.WriteLine(firstName + " " + lastName + " already exists in " + meetings[nr - 1].Name);
                    Console.WriteLine("\nPerson has not been added");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }

                //Test zone
/*                Period first = new Period();
                Period second = new Period();
                first.Start = meetings[1].StartDate;
                first.End = meetings[1].EndDate;

                second.Start = meetings[2].StartDate;
                second.End = meetings[2].EndDate;

                if(first.IntersectsWith(second))
                {
                    Console.WriteLine("Test");
                }*/

                //Check if intereferes with another meet
                foreach(Meeting meeting in meetings)
                {
                    string name = firstName.ToString();
                    string surname = lastName.ToString();

                    Period first = new Period();
                    Period second = new Period();

                    first.Start = meetings[nr - 1].StartDate;
                    first.End = meetings[nr - 1].EndDate;

                    second.Start = meeting.StartDate;
                    second.End = meeting.EndDate;

                    if(first.IntersectsWith(second) && meetings[nr - 1].Name != meeting.Name && meeting.People.Exists(x => x.Name == name && x.Surname == surname))
                    {
                        //Interference warning
                        bool interferes = true;
                        while (interferes)
                        {
                            Console.Clear();
                            Console.WriteLine("Interferes with another meeting");
                            Console.WriteLine("Person already exists in meeting: " + meeting.Name + ", meeting starts at " + meeting.StartDate + " and ends at " + meeting.EndDate);
                            Console.WriteLine("Selected meeting: " + meetings[nr - 1].Name + ", starts at " + meetings[nr - 1].StartDate + " and ends at " + meetings[nr - 1].EndDate);
                            Console.Write("\nWould you still want to add the person to the selected meeting?\n");
                            Console.WriteLine("\t [1] Yes");
                            Console.WriteLine("\t [2] No");
                            Console.Write("\t your choice:");

                            string str1 = Console.ReadLine();
                            int nr1;
                            bool isNumeric1 = int.TryParse(str1, out nr1);

                            switch (nr1)
                            {
                                case 1:
                                    //If everything is okay add
                                    Console.Clear();
                                    meetings[nr - 1].People.Add(person);

                                    //Save data to JSON
                                    saveToJSON();
                                    Console.WriteLine(firstName + " " + lastName + " has succesfully been added to: " + meetings[nr - 1].Name);
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                    interferes = false;
                                    return;
                                    break;
                                case 2:
                                    Console.Clear();
                                    Console.WriteLine("Person has not been added");
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();
                                    Console.Clear();
                                    interferes = false;
                                    return;
                                    break;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("Please check your choice and try again\n");
                                    break;
                            }
                        }
                    }

                }


                //If everything is okay add
                Console.Clear();
                meetings[nr - 1].People.Add(person);

                //Save data to JSON
                saveToJSON();
                Console.WriteLine(firstName + " " + lastName + " has succesfully been added to: " + meetings[nr - 1].Name);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;

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



    System.Console.Clear();
}

//Remove a person from a meeting... (Save added)
void removePerson()
{
    while (true)
    {
        Console.WriteLine("Person deletion.\nSelect which meeting to delete from");
        Console.WriteLine("\t[0]Return to main menu\n");
        int i = 1;
        foreach (Meeting meeting in meetings)
        {
            Console.WriteLine("\t[" + i + "]" + meeting.Name);
            i++;
        }
        Console.WriteLine("\t your choice: \t");

        string str = Console.ReadLine();
        int nr;
        bool isNumeric = int.TryParse(str, out nr);

        if (isNumeric)
        {
            if (nr == 0)
            {
                Console.Clear();
                return;
            }
            if (nr <= meetings.Count && nr > 0)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Selected meeting: " + meetings[nr - 1].Name);
                    Console.WriteLine("\nAll of the available people in the meeting:\n");
                    Console.WriteLine("\t[0]Return to main menu\n");

                    int j = 1;
                    foreach (Person person in meetings[nr - 1].People)
                    {
                        Console.WriteLine("\t[" + j + "]" + person.Name + " " + person.Surname);
                        j++;
                    }
                    Console.WriteLine("\t your choice: \t");

                    string str1 = Console.ReadLine();
                    int nr1;
                    bool isNumeric1 = int.TryParse(str1, out nr1);

                    if (isNumeric1)
                    {
                        if(nr1==0)
                        {
                            Console.Clear();
                            return;
                        }
                        if(nr1 <= meetings[nr-1].People.Count && nr1 >0)
                        {
                            //Need to add check if is responsible
                            if(meetings[nr-1].People[nr1-1].isResponsible)
                            {
                                Console.Clear();
                                Console.WriteLine(meetings[nr - 1].People[nr1-1].Name + " " + meetings[nr - 1].People[nr1-1].Surname + " is responsible for the meeting, he cannot be removed.");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                Console.Clear();
                                return;
                            }

                            //Removes
                            meetings[nr - 1].People.RemoveAt(nr1 - 1);

                            //Save data to JSON
                            saveToJSON();
                            Console.Clear();
                            Console.WriteLine("Person succesfully removed.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            return;
                        }

                    }

                }

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

//List all meetings/filter
void listAllMeetings()
{
    while (true)
    {
        Console.WriteLine("View meetings, select an option:\n");
        Console.WriteLine("\t [1] List all available");
        Console.WriteLine("\t [2] Filter by description");
        Console.WriteLine("\t [3] Filter by responsible person");
        Console.WriteLine("\t [4] Filter by category");
        Console.WriteLine("\t [5] Filter by type");
        Console.WriteLine("\t [6] Filter by dates");
        Console.WriteLine("\t [7] Filter by the number of attendees\n");
        Console.WriteLine("\t [8] Exit to main menu");
        Console.Write("\t your choice: ");
        string str = Console.ReadLine();
        int nr;
        bool isNumeric = int.TryParse(str, out nr);

        switch (nr)
        {
            case 1:
                Console.Clear();
                listAllAvailable();
                break;
            case 2:
                Console.Clear();
                filterByDescription();
                break;
            case 3:
                Console.Clear();
                filterByResponsible();
                break;
            case 4:
                Console.Clear();
                filterByCategory();
                break;
            case 5:
                Console.Clear();
                filterByType();
                break;
            case 6:
                Console.Clear();
                filterByDate();
                break;
            case 7:
                Console.Clear();
                filterByNumberOfAttendees();
                break;
            case 8:
                Console.Clear();
                return;
            default:
                Console.Clear();
                Console.WriteLine("Option does not exist\n");
                break;
        }
        //Console.ReadKey();
    }
}

//View all meetings
void listAllAvailable()
{
    Console.WriteLine("All of the available meets\n");
    foreach(Meeting meeting in meetings)
    {
        Console.WriteLine("\tMeeting name: \t" + meeting.Name);
        Console.WriteLine("\tResponsible person: \t" + meeting.ResponsiblePerson.Name + " " + meeting.ResponsiblePerson.Surname);
        Console.WriteLine("\tDescription: \t" + meeting.Description);
        Console.WriteLine("\tCategory: \t" + meeting.Category);
        Console.WriteLine("\tType: \t" + meeting.Type);
        Console.WriteLine("\tStart Date: \t" + meeting.StartDate.ToString("g"));
        Console.WriteLine("\tEnd Date: \t" + meeting.EndDate.ToString("g"));
        Console.WriteLine("\n\tPeople in the meeting: \t");
        foreach(Person person in meeting.People)
        {
            Console.WriteLine("\t" + person.Name + " " + person.Surname);
        }
        Console.WriteLine("---------------------------------------------------------------------------");
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
    Console.Clear();
    return;
}

//Filter by description
void filterByDescription()
{
    List<Meeting> filteredMeetings = new List<Meeting>();

    Console.Write("Enter what description would you want to filter by (Leave blank to return):\t");
    string filter = Console.ReadLine();

    if(filter.Count() > 0)
    {
        foreach (Meeting meeting in meetings)
        {
            if (meeting.Description.Contains(filter))
            {
                filteredMeetings.Add(meeting);
            }
        }

        if(filteredMeetings.Count() > 0)
        {
            Console.Clear();
            Console.WriteLine("Meetings that have: " + filter + " in their description\n");
            foreach(Meeting meeting in filteredMeetings)
            {
                Console.WriteLine("\tMeeting name: \t" + meeting.Name);
                Console.WriteLine("\tResponsible person: \t" + meeting.ResponsiblePerson.Name + " " + meeting.ResponsiblePerson.Surname);
                Console.WriteLine("\tDescription: \t" + meeting.Description);
                Console.WriteLine("\tCategory: \t" + meeting.Category);
                Console.WriteLine("\tType: \t" + meeting.Type);
                Console.WriteLine("\tStart Date: \t" + meeting.StartDate.ToString("g"));
                Console.WriteLine("\tEnd Date: \t" + meeting.EndDate.ToString("g"));
                Console.WriteLine("\n\tPeople in the meeting: \t");
                foreach (Person person in meeting.People)
                {
                    Console.WriteLine("\t" + person.Name + " " + person.Surname);
                }
                Console.WriteLine("---------------------------------------------------------------------------");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("No meetings with selected description exist");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            return;
        }

    }
    else
    {
        Console.Clear();
        return;
    }

}

//Filter by responsible person
void filterByResponsible()
{
    List<Meeting> filteredMeetings = new List<Meeting>();

    Console.Write("Enter the name responsible you would want to filter by (Leave blank to return):\t");
    string name = Console.ReadLine();
    if (name == "")
    {
        Console.Clear();
        return;
    }
    Console.Clear();
    Console.Write("Enter the surname responsible you would want to filter by (Leave blank to return):\t");
    string surname = Console.ReadLine();
    if (surname == "")
    {
        Console.Clear();
        return;
    }
    Console.Clear();

        foreach (Meeting meeting in meetings)
        {
            if (meeting.ResponsiblePerson.Name == name && meeting.ResponsiblePerson.Surname == surname)
            {
                filteredMeetings.Add(meeting);
            }
        }

        if (filteredMeetings.Count() > 0)
        {
            Console.Clear();
            Console.WriteLine("Meetings with: " + name + " "+ surname +" being responsible\n");
            foreach (Meeting meeting in filteredMeetings)
            {
                Console.WriteLine("\tMeeting name: \t" + meeting.Name);
                Console.WriteLine("\tResponsible person: \t" + meeting.ResponsiblePerson.Name + " " + meeting.ResponsiblePerson.Surname);
                Console.WriteLine("\tDescription: \t" + meeting.Description);
                Console.WriteLine("\tCategory: \t" + meeting.Category);
                Console.WriteLine("\tType: \t" + meeting.Type);
                Console.WriteLine("\tStart Date: \t" + meeting.StartDate.ToString("g"));
                Console.WriteLine("\tEnd Date: \t" + meeting.EndDate.ToString("g"));
                Console.WriteLine("\n\tPeople in the meeting: \t");
                foreach (Person person in meeting.People)
                {
                    Console.WriteLine("\t" + person.Name + " " + person.Surname);
                }
                Console.WriteLine("---------------------------------------------------------------------------");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("No meetings with selected responsible person exist");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            return;
        }

}

//Filter by category
void filterByCategory()
{
    List<Meeting> filteredMeetings = new List<Meeting>();
    bool categorySelect = true;
    string category = "";

    while (categorySelect)
    {
        Console.WriteLine("\tAvailable categories:\n");
        Console.WriteLine("\t [1] CodeMonkey");
        Console.WriteLine("\t [2] Hub");
        Console.WriteLine("\t [3] Short");
        Console.WriteLine("\t [4] TeamBuilding");
        Console.WriteLine("\t [5] Exit to main menu");
        Console.Write("\t your choice: ");
        string str = Console.ReadLine();
        int nr;
        bool isNumeric = int.TryParse(str, out nr);

        switch (nr)
        {
            case 1:
                Console.Clear();
                category = "CodeMonkey";
                categorySelect = false;
                break;
            case 2:
                Console.Clear();
                category = "Hub";
                categorySelect = false;
                break;
            case 3:
                Console.Clear();
                category = "Short";
                categorySelect = false;
                break;
            case 4:
                Console.Clear();
                category = "TeamBuilding";
                categorySelect = false;
                break;
            case 5:
                Console.Clear();
                return;
            default:
                Console.Clear();
                Console.WriteLine("Option does not exist\n");
                break;
        }
    }



        foreach (Meeting meeting in meetings)
        {
            if (meeting.Category == category)
            {
                filteredMeetings.Add(meeting);
            }
        }

        if (filteredMeetings.Count() > 0)
        {
            Console.Clear();
            Console.WriteLine("Meetings with category: " + category + "\n");
            foreach (Meeting meeting in filteredMeetings)
            {
                Console.WriteLine("\tMeeting name: \t" + meeting.Name);
                Console.WriteLine("\tResponsible person: \t" + meeting.ResponsiblePerson.Name + " " + meeting.ResponsiblePerson.Surname);
                Console.WriteLine("\tDescription: \t" + meeting.Description);
                Console.WriteLine("\tCategory: \t" + meeting.Category);
                Console.WriteLine("\tType: \t" + meeting.Type);
                Console.WriteLine("\tStart Date: \t" + meeting.StartDate.ToString("g"));
                Console.WriteLine("\tEnd Date: \t" + meeting.EndDate.ToString("g"));
                Console.WriteLine("\n\tPeople in the meeting: \t");
                foreach (Person person in meeting.People)
                {
                    Console.WriteLine("\t" + person.Name + " " + person.Surname);
                }
                Console.WriteLine("---------------------------------------------------------------------------");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            return;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("No meetings with selected category exist");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            return;
        }
}

//Filter by type
void filterByType()
{
    List<Meeting> filteredMeetings = new List<Meeting>();
    bool typeSelect = true;
    string type = "";

    while (typeSelect)
    {
        Console.WriteLine("\tAvailable types:\n");
        Console.WriteLine("\t [1] Live");
        Console.WriteLine("\t [2] InPerson\n");
        Console.WriteLine("\t [3] Exit to main menu");
        Console.Write("\t your choice: ");
        string str = Console.ReadLine();
        int nr;
        bool isNumeric = int.TryParse(str, out nr);

        switch (nr)
        {
            case 1:
                Console.Clear();
                type = "Live";
                typeSelect = false;
                break;
            case 2:
                Console.Clear();
                type = "InPerson";
                typeSelect = false;
                break;
            case 3:
                Console.Clear();
                return;
            default:
                Console.Clear();
                Console.WriteLine("Option does not exist\n");
                break;
        }
    }



    foreach (Meeting meeting in meetings)
    {
        if (meeting.Type == type)
        {
            filteredMeetings.Add(meeting);
        }
    }

    if (filteredMeetings.Count() > 0)
    {
        Console.Clear();
        Console.WriteLine("Meetings with type: " + type + "\n");
        foreach (Meeting meeting in filteredMeetings)
        {
            Console.WriteLine("\tMeeting name: \t" + meeting.Name);
            Console.WriteLine("\tResponsible person: \t" + meeting.ResponsiblePerson.Name + " " + meeting.ResponsiblePerson.Surname);
            Console.WriteLine("\tDescription: \t" + meeting.Description);
            Console.WriteLine("\tCategory: \t" + meeting.Category);
            Console.WriteLine("\tType: \t" + meeting.Type);
            Console.WriteLine("\tStart Date: \t" + meeting.StartDate.ToString("g"));
            Console.WriteLine("\tEnd Date: \t" + meeting.EndDate.ToString("g"));
            Console.WriteLine("\n\tPeople in the meeting: \t");
            foreach (Person person in meeting.People)
            {
                Console.WriteLine("\t" + person.Name + " " + person.Surname);
            }
            Console.WriteLine("---------------------------------------------------------------------------");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        return;
    }
    else
    {
        Console.Clear();
        Console.WriteLine("No meetings with selected type exist");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        return;
    }
}

//Filter by date
void filterByDate()
{
    DateTime startDate = new DateTime(0);
    DateTime endDate = new DateTime(0);

    List<Meeting> filteredMeetings = new List<Meeting>();

    bool dateSelect = true;

    while (dateSelect)
    {
        Console.WriteLine("\tFilter by:\n");
        Console.WriteLine("\t [1] Starting from date");
        Console.WriteLine("\t [2] Between two dates\n");
        Console.WriteLine("\t [3] Exit to main menu");
        Console.Write("\t your choice: ");
        string str = Console.ReadLine();
        int nr;
        bool isNumeric = int.TryParse(str, out nr);

        switch (nr)
        {
            //Starting from
            case 1:
                Console.Clear();
                bool checkDateCase1 = false;
                {
                    while (checkDateCase1 == false)
                    {
                        //Start date select
                        Console.Write("Enter date from which you want to see the meetings (YYYY-MM-dd format): ");
                        string date = Console.ReadLine();
                        if (DateTime.TryParse(date, out startDate) == true)
                        {
                            checkDateCase1 = true;
                        }
                        else
                        {
                            System.Console.Clear();
                            Console.WriteLine("Wrong date format\n");
                        }
                    }
                }

                foreach (Meeting meeting in meetings)
                {
                    if (meeting.StartDate >= startDate)
                    {
                        filteredMeetings.Add(meeting);
                    }
                }

                if (filteredMeetings.Count() > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Meetings that will happen starting from " + startDate.ToString("d") + "\n");
                    foreach (Meeting meeting in filteredMeetings)
                    {
                        Console.WriteLine("\tMeeting name: \t" + meeting.Name);
                        Console.WriteLine("\tResponsible person: \t" + meeting.ResponsiblePerson.Name + " " + meeting.ResponsiblePerson.Surname);
                        Console.WriteLine("\tDescription: \t" + meeting.Description);
                        Console.WriteLine("\tCategory: \t" + meeting.Category);
                        Console.WriteLine("\tType: \t" + meeting.Type);
                        Console.WriteLine("\tStart Date: \t" + meeting.StartDate.ToString("g"));
                        Console.WriteLine("\tEnd Date: \t" + meeting.EndDate.ToString("g"));
                        Console.WriteLine("\n\tPeople in the meeting: \t");
                        foreach (Person person in meeting.People)
                        {
                            Console.WriteLine("\t" + person.Name + " " + person.Surname);
                        }
                        Console.WriteLine("---------------------------------------------------------------------------");
                    }
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("No meetings starting from " + startDate.ToString("d"));
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                dateSelect = false;
                break;

            //Between two dates
            case 2:
                Console.Clear();
                bool checkStartDate = false;
                {
                    while (checkStartDate == false)
                    {
                        //Start date select
                        Console.Write("Enter the start date (YYYY-MM-dd format): ");
                        string date = Console.ReadLine();
                        if (DateTime.TryParse(date, out startDate) == true)
                        {
                            checkStartDate = true;
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
                        Console.Write("Enter the end date (YYYY-MM-dd format): ");
                        string date = Console.ReadLine();
                        if (DateTime.TryParse(date, out endDate) == true)
                        {
                            checkEndDate = true;
                        }
                        else
                        {
                            System.Console.Clear();
                            Console.WriteLine("Wrong date format\n");
                        }
                    }
                }

                foreach (Meeting meeting in meetings)
                {
                    if (meeting.StartDate >= startDate && meeting.EndDate <= endDate)
                    {
                        filteredMeetings.Add(meeting);
                    }
                }

                if (filteredMeetings.Count() > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Meetings that will happen between " + startDate.ToString("d") + " and " + endDate.ToString("d") +"\n");
                    foreach (Meeting meeting in filteredMeetings)
                    {
                        Console.WriteLine("\tMeeting name: \t" + meeting.Name);
                        Console.WriteLine("\tResponsible person: \t" + meeting.ResponsiblePerson.Name + " " + meeting.ResponsiblePerson.Surname);
                        Console.WriteLine("\tDescription: \t" + meeting.Description);
                        Console.WriteLine("\tCategory: \t" + meeting.Category);
                        Console.WriteLine("\tType: \t" + meeting.Type);
                        Console.WriteLine("\tStart Date: \t" + meeting.StartDate.ToString("g"));
                        Console.WriteLine("\tEnd Date: \t" + meeting.EndDate.ToString("g"));
                        Console.WriteLine("\n\tPeople in the meeting: \t");
                        foreach (Person person in meeting.People)
                        {
                            Console.WriteLine("\t" + person.Name + " " + person.Surname);
                        }
                        Console.WriteLine("---------------------------------------------------------------------------");
                    }
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("No meetings between " + startDate.ToString("d") + " and " + endDate.ToString("d"));
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }
                dateSelect = false;
                break;
            case 3:
                Console.Clear();
                return;
            default:
                Console.Clear();
                Console.WriteLine("Option does not exist\n");
                break;
        }
    }
}

//Filter by number of attendees
void filterByNumberOfAttendees()
{
    List<Meeting> filteredMeetings = new List<Meeting>();

    while (true)
    {
        Console.Write("How many minimum people should be in the meeting (Leave blank to return):\t");

        string filter = Console.ReadLine();
        int nr;
        bool isNumeric = int.TryParse(filter, out nr);
        if (!isNumeric)
        {
            Console.Clear();
            Console.WriteLine("Please enter a correct number\n");
        }
        if (filter.Count() > 0)
        {
            foreach (Meeting meeting in meetings)
            {
                if (meeting.People.Count >= nr)
                {
                    filteredMeetings.Add(meeting);
                }
            }

            if (filteredMeetings.Count() > 0)
            {
                Console.Clear();
                Console.WriteLine("Meetings that have over " + filter + " people\n");
                foreach (Meeting meeting in filteredMeetings)
                {
                    Console.WriteLine("\tMeeting name: \t" + meeting.Name);
                    Console.WriteLine("\tResponsible person: \t" + meeting.ResponsiblePerson.Name + " " + meeting.ResponsiblePerson.Surname);
                    Console.WriteLine("\tDescription: \t" + meeting.Description);
                    Console.WriteLine("\tCategory: \t" + meeting.Category);
                    Console.WriteLine("\tType: \t" + meeting.Type);
                    Console.WriteLine("\tStart Date: \t" + meeting.StartDate.ToString("g"));
                    Console.WriteLine("\tEnd Date: \t" + meeting.EndDate.ToString("g"));
                    Console.WriteLine("\n\tPeople in the meeting: \t");
                    foreach (Person person in meeting.People)
                    {
                        Console.WriteLine("\t" + person.Name + " " + person.Surname);
                    }
                    Console.WriteLine("---------------------------------------------------------------------------");
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("No meetings with over " + nr + " people exist");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
                return;
            }

        }
        else
        {
            Console.Clear();
            return;
        }
    }
}

//Save data to json
void saveToJSON()
{
    //Change this for testing only rn
    JsonSerializerSettings jss = new JsonSerializerSettings()
    {
        TypeNameHandling = TypeNameHandling.Auto
    };
    string serializedCollection = JsonConvert.SerializeObject(meetings, jss);

    File.WriteAllText("MeetingData.json", serializedCollection);
}