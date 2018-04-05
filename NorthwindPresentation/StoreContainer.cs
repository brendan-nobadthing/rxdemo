using NorthwindApplication.Customer;
using Redux;

namespace NorthwindPresentation
{
    /// <summary>
    /// static class for global redux stores 
    /// </summary>
    public static class StoreContainer
    {

        public static Store<CustomerState> CustomerStore { get; set; }

    }
}