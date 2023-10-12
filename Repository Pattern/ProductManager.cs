using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;


namespace Repository_Pattern
{
    public class ProductManager : MainManager<Product>
    {
        public ProductManager(MyDBContext _dBContext) : base(_dBContext)
        {
        }

        public EntityEntry<Product> Edit(Product product, int ID)
        {
            Product Old = this.Get(ID);
            Old.CategoryID = product.CategoryID;
            Old.Description = product.Description;
            Old.Name = product.Name;
            Old.Price = product.Price;
            Old.Quantity = product.Quantity;

            return Update(Old);

        }
        public Product Get(int ID) => Get().Where(i => i.ID == ID).FirstOrDefault();

    }
}
