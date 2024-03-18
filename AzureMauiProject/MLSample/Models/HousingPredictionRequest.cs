using Newtonsoft.Json;

namespace MLSample.Models;

public class HousingPredictionRequest
{
    [JsonProperty("Inputs")]
    public HousingPredictionRequestDataList Inputs { get; set; } = new HousingPredictionRequestDataList(); 
}

public class HousingPredictionRequestDataList
{
    [JsonProperty("data")]
    public List<HousingPredictionRequestData> Data { get; set; } = new List<HousingPredictionRequestData>();

[JsonProperty("GlobalParameters")]
    public double? GlobalParameters { get; set; }
}

public  class HousingPredictionRequestData
{
    [JsonProperty("Crime")]
    public double? Crime { get; set; }
    [JsonProperty("zone_percent")]
    public double? ZoningPercent { get; set; }
    [JsonProperty("industry_percent")]
    public double? IndustryPercent { get; set; }
    [JsonProperty("river_lot")]
    public double? RiverLot { get; set; }
    [JsonProperty("nox_concentration")]
    public double? NitricOxide { get; set; }
    [JsonProperty("rooms")]
    public double? Rooms { get; set; }
    [JsonProperty("home_age")]
    public double? Age { get; set; }
    [JsonProperty("work_distance")]
    public double? Distance { get; set; }
    [JsonProperty("highway_access")]
    public double? HighwayAccess { get; set; }
    [JsonProperty("tax_in_thousands")]
    public double? Tax { get; set; }
    [JsonProperty("student_teacher_ratio")]
    public double? PupilTeacherRatio { get; set; }
    [JsonProperty("african_american_percent")]
    public double? AfricanAmerican { get; set; }
    [JsonProperty("poor_percent")]
    public double? LowerStatus { get; set; }
}

public class HousingPredictionResponse
{
    [JsonProperty("Results")]
    public List<double> Results { get; set; } = new List<double>();
}