﻿using Portal.Core.Models;
using Portal.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Interface
{
    public interface IDepartmentRep
    {
        Task<List<Department>> GetAsync();
        Task<Department> GetByIdAsync(int id);
        Task CreateAsync(Department obj);
        Task UpdateAsync(Department obj);
        Task DeleteAsync(Department obj);


    }
}
