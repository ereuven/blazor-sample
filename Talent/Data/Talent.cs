using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Talent.Data
{
    public class Talent
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [BsonElement("last_name")]
        public string LastName { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("target_unit")]
        public string TargetUnit { get; set; }

        [BsonElement("comments")]
        public IList<Comment> Comments { get; set; }

        [BsonElement("stages")]
        public IList<TalentStage> Stages { get; set; }

        [BsonElement("responsible_employee_id")]
        public string ResponsibleEmployeeId { get; set; }

        [BsonIgnore]
        public string DisplayName => $"{FirstName} {LastName}";
    }

    public class TargetUnit
    {
        [BsonId, BsonElement("unit_num")]
        public string UnitNum { get; set; }

        [BsonElement("department")]
        public string Department { get; set; }
    }

    public class TalentStage
    {
        [BsonElement("stage_num")]
        public string StageName { get; set; }

        [BsonElement("stage_date")]
        public DateTime StageDate { get; set; }

        [BsonElement("comments")]
        public IList<Comment> Comments { get; set; }
    }

    public class Comment
    {
        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; }

        [BsonElement("employee_id")]
        public string EmployeeId { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }
    }
}