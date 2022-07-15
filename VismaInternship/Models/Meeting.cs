using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaInternship.Models;

namespace VismaInternship
{
    [Serializable]
    public class Meeting
    {
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public Person ResponsiblePerson { get; set; }
        [JsonProperty]
        public string Description { get; set; }
        [JsonProperty]
        public string Category { get; set; }
        [JsonProperty]
        public string Type { get; set; }
        [JsonProperty]
        public DateTime StartDate { get; set; }
        [JsonProperty]
        public DateTime EndDate { get; set; }

        [JsonProperty]
        public List<Person> People = new List<Person>();

        public void Load(Person person)
        {
            People.Add(person);
        }
    }
}
