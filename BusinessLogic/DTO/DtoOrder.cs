using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class DtoOrder
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public DateTime DateCreated { get; set; }
        public int Status { get; set; }
        public double Total { get; private set; }
        public ObservableCollection<DtoOrderDetails> Details { get; }        

        public DtoOrder()
        {
            Details = new ObservableCollection<DtoOrderDetails>();
            Details.CollectionChanged += DetailsCollectionChanged;
        }

        private void DetailsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Total = Details.Sum(x => x.Price * x.Quantity);
        }
    }
}
