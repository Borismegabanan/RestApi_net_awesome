using System.ComponentModel;

namespace GMCS_RestApi.Domain.Enums
{
    /// <summary>
    /// Возможные состояния книги
    /// </summary>
    public enum EBookState
    {
        [Description("Продано")]
        Sold = 1,

        [Description("В наличии")]
        InStock = 2,

        [Description("Неизвестно")]
        Unknown = 3
    }
}
