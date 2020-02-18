using HolidayOptimizations.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HolidayOptimizations.StorageRepository.DataRepository.Background
{
    public class DbInsertTask<T> where T: BaseEntity
    {
        public void Insert(List<T> elements)
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var element in elements)
                {
                    
                }
            });
        }
    }
}

