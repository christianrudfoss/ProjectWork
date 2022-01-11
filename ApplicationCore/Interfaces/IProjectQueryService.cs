using ApplicationCore.Entities.ProjectAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{

    public interface IProjectQueryService
    {
        public Task<IEnumerable<Project>> GetProjects();
        public Task<Project> GetProject(int id);
        public Task<string> Delete(int id);
        public Task<string> Edit(int id, Project project);
        public Task<Project> Create(Project project);
    }
}
