using Models;

namespace Repository_Pattern
{
    public class UnitOfWork
    {
        private MyDBContext MyDBContext { get; set; }
        public UnitOfWork(MyDBContext dBContext)
        {
            MyDBContext = dBContext;
        }
        public void commit()
        {
            MyDBContext.SaveChanges();
        }

    }
}
