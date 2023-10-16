using Models;

namespace ViewModel
{
    public static class Type
    {
        public static Product ToProduct(this ProductViewModel PAdded)
        {
            Product Product = new Product()
            {
                Name = PAdded.Name,
                Description = PAdded.Description,
                Price = PAdded.Price,
                Quantity = PAdded.Quantity,
                CategoryID = PAdded.CategoryID
            };

            Product.ProductAttachments = new List<ProductAttachment>();
            foreach (string PURL in PAdded.ImagesUrl)
            {
                Product.ProductAttachments.Add(new ProductAttachment()
                {
                    Image = PURL
                });
            }
            return Product;

        }

        public static ProductViewModel ToProductViewModel(this Product ProductToBeTransferred)
        {
            ProductViewModel Product = new ProductViewModel()
            {
                ID = ProductToBeTransferred.ID,
                Name = ProductToBeTransferred.Name,
                Description = ProductToBeTransferred.Description,
                Price = ProductToBeTransferred.Price,
                Quantity = ProductToBeTransferred.Quantity,
                CategoryID = ProductToBeTransferred.CategoryID,
                ImagesUrl = ProductToBeTransferred.ProductAttachments.Select(x => x.Image).ToList(),
            };

            return Product;
        }
    }

}
