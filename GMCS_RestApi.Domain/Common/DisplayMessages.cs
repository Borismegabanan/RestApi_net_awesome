
namespace GMCS_RestApi.Domain.Common
{
    public static class DisplayMessages
    {
        public static class Error
        {
            public const string BookNotFound = "не найдена книга";

            public const string AuthorNotFound = "не найден автор";

            public const string SoldBookBadRequest = "Данной книги нет в налчии";
        }

        public static class Validation
        {
            public const string RequireProperty = "Не заполнен обязательный параметр: {PropertyName}";
        }





    }
}
