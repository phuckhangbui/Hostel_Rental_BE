using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class TypeServiceDao : BaseDAO<TypeService>
    {
        private static TypeServiceDao instance = null;
        private static readonly object instacelock = new object();

        private TypeServiceDao()
        {
        }

        public static TypeServiceDao Instance
        {
            get
            {
                lock (instacelock)
                {
                    if (instance == null)
                    {
                        instance = new TypeServiceDao();
                    }
                    return instance;
                }

            }
        }

        public TypeService GetTypeServiceById(int id)
        {
            TypeService typeService = null;
            using (var context = new DataContext())
            {
                typeService = context.TypeService.FirstOrDefault(x => x.TypeServiceID == id);
            }
            return typeService;
        }
    }
}