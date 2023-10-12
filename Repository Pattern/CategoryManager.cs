using Models;

namespace Repository_Pattern
{
    public class CategoryManager : MainManager<Category>
    {
        public CategoryManager(MyDBContext _dBContext) : base(_dBContext)
        {
        }
    }
}
