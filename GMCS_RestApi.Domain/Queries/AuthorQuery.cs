using System;

namespace GMCS_RestApi.Domain.Queries
{
    public class AuthorQuery
    {
        public int Id;
        public override bool Equals(object? obj)
        {
            return true;
        }

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