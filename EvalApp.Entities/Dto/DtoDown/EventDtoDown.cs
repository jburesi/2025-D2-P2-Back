namespace EvalApp.Entities.Dto.DtoDown
{
    public record EventDtoDown
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }
}
