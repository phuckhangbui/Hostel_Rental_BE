using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<TypeService> GetTypeServiceById(int id)
        {
            TypeService typeService = null;
            using (var context = new DataContext())
            {
                typeService = await context.TypeService.FirstOrDefaultAsync(x => x.TypeServiceID == id);
            }
            return typeService;
        }

        public async Task<bool> CheckTypeServiceNameExist(string typeServiceName)
        {
            using (var context = new DataContext())
            {
                return await context.TypeService.AnyAsync(x => x.TypeName.Trim().ToLower() == typeServiceName.Trim().ToLower());
            }
        }
    }
}