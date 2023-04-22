using AutoMapper;
using Xunit.Abstractions;

namespace UnitTests
{
    public abstract class ControllerTestBase
    {
        protected const int Success200 = 200;
        protected const int Found302 = 302;
        protected const int BadRequest400 = 400;
        protected const int Unauthorized401 = 401;
        protected const int Forbidden403 = 403;
        protected const int NotFound404 = 404;
        
        protected readonly ITestOutputHelper Output;
        protected IMapper Mapper { get; private set; }
        
        protected ControllerTestBase(ITestOutputHelper output)
        {
            Output = output;
            CreateMapper();
        }

        private void CreateMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new ConfigProfile()); 
                cfg.AddProfile(new AquariumProfile()); 
                cfg.AddProfile(new DeviceProfile()); 
                cfg.AddProfile(new MeasurmentProfile()); 
            });

            Mapper = new Mapper(mapperConfig);
        }
        
        protected (int Status, TDtoType? Object) GetValueFromResult<TDtoType>
            (IActionResult? result) where TDtoType : class
        {
            if (result is null)
                return (-1, null);
            
            try
            {
                var okObjectResult = result as OkObjectResult;
                var obj = okObjectResult.Value as TDtoType;
                return (Success200, obj);
            }
            catch (Exception e)
            {
                var statusCodeResult = result as StatusCodeResult;
                
                if (statusCodeResult is null)
                    return (-1, null);
                
                return (statusCodeResult.StatusCode, null);
            }
        }
    }
}
