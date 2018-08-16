namespace MyDiary.Data
{
    public class Photo : Entity
    {
        public string Name { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
        public string Image { get; set; }
    }
}