namespace Everbridge.ControlCenter.TechnicalChallenge.DoorDatabase
{
    public class DoorRecordDto
    {
        public DoorRecordDto()
        {
        }

        public DoorRecordDto(DoorRecord record)
        {
            Id = record.Id;
            Label = record.Label;
            IsOpen = record.IsOpen;
            IsLocked = record.IsLocked;
        }

        public string Id { get; set; }

        public string Label { get; set; }

        public bool IsOpen { get; set; }

        public bool IsLocked { get; set; }
    }
}
