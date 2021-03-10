namespace GMCS_RestApi.Domain.Queries
{
    public class AuthorQuery
    {
        public int Id;

        protected bool Equals(AuthorQuery other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}