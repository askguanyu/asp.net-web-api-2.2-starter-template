using LegacyApplication.Repositories.Core;
using LegacyApplication.Repositories.HumanResources;
using Serilog;

namespace LegacyApplication.Services.Core
{
    public interface ICommonService
    {
        IUploadedFileRepository UploadedFileRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        ILogger Log { get; }
    }

    public class CommonService : ICommonService
    {
        public IUploadedFileRepository UploadedFileRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }
        public ILogger Log { get; }

        public CommonService(
            IUploadedFileRepository uploadedFileRepository,
            IDepartmentRepository departmentRepository,
            ILogger log)
        {
            UploadedFileRepository = uploadedFileRepository;
            DepartmentRepository = departmentRepository;
            Log = log;
        }
    }
}
