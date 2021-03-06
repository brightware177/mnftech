﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using DAL.UnitOfWork;

namespace BL.Managers
{
    public class WorkplaceManager : WorkplaceManagerService
    {
        IUnitOfWork repo;
        public WorkplaceManager(IUnitOfWork uow)
        {
            repo = uow;
        }

        public void AddStaff(Staff staff)
        {
            repo.Workplaces.CreateStaff(staff);
            repo.SaveChanges();
        }

        public Workplace AddWorkplace(Workplace workplace)
        {
            var newWorkplace = repo.Workplaces.CreateWorkplace(workplace);
            repo.SaveChanges();
            return newWorkplace;
        }

        public Workplace ChangeWorkplace(Workplace model)
        {
            return repo.Workplaces.UpdateWorkplace(model);
        }

        public IEnumerable<City> GetCities()
        {
            return repo.Workplaces.ReadCities();
        }

        public IEnumerable<Infrastructure> GetInfrastructures()
        {
            return repo.Workplaces.ReadInfrastructures();
        }

        public Staff GetStaffWithApplicationUserId(string ApplicationUserId)
        {
            return repo.Workplaces.ReadStaff(ApplicationUserId);
        }

        public Workplace GetWorkplace(int id)
        {
            return repo.Workplaces.ReadWorkplace(id);
        }

        public Workplace GetWorkplace(string id)
        {
            return repo.Workplaces.ReadWorkplace(id);
        }

        public IEnumerable<Workplace> GetWorkplaces()
        {
            if (repo.Workplaces.ReadWorkplaces() != null)
                return repo.Workplaces.ReadWorkplaces();
            return null;
        }

        public void RemoveWorkplace(Workplace workplace)
        {
            repo.Workplaces.DeleteWorkplace(workplace);
        }
    }
}
