namespace GMCS_RestApi.Domain.Queries
{
    public class BookQuery
    {
        public int Id;

        protected bool Equals(BookQuery other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}