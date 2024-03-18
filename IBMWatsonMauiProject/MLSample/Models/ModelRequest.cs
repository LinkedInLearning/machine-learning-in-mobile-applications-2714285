using Newtonsoft.Json;

namespace MLSample.Models;

    public class ModelRequest
    {
        [JsonProperty("input_data")]
        public InputData InputData { get; set; } =  new InputData();
    }

    public class InputData
    {
        public InputData()
        {
            Fields = new List<string>
            {
                "crime",
                "zone_percent",
                "industry_percent",
                "river_lot",
                "nox_concentration",
                "rooms",
                "home_age",
                "work_distance",
                "highway_access",
                "tax_in_thousands",
                "student_teacher_ratio",
                "african_american_percent",
                "poor_percent",
            };
        }

        [JsonProperty("fields")]
        public List<string> Fields { get; set; } = new List<string>();  

        [JsonProperty("values")]
        public List<List<double?>> Values { get; set; } = new List<List<double?>>();  
    }