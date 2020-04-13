using ArmaOps.Common;
using ArmaOps.Domain.Example;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArmaOps.Application.Example.Services
{
    public interface IExampleService
    {
        Task<IEnumerable<ExampleModel>> GetSomeData();
    }

    [Preserve(AllMembers = true)]
    public class ExampleService : IExampleService
    {
        Random random = new Random();

        public async Task<IEnumerable<ExampleModel>> GetSomeData()
        {
            IEnumerable<ExampleModel> data = new List<ExampleModel> {
            new ExampleModel ($"Title {random.Next()}",$"Description {random.Next()}", "https://banner2.cleanpng.com/20180530/tq/kisspng-arma-3-apex-arma-2-operation-arrowhead-video-gam-5b0f5d2f2b7c09.0298490415277335511781.jpg"),
            new ExampleModel ($"Title {random.Next()}",$"Description {random.Next()}", "https://banner2.cleanpng.com/20180530/tq/kisspng-arma-3-apex-arma-2-operation-arrowhead-video-gam-5b0f5d2f2b7c09.0298490415277335511781.jpg"),
            new ExampleModel ($"Title {random.Next()}",$"Description {random.Next()}", "https://banner2.cleanpng.com/20180530/tq/kisspng-arma-3-apex-arma-2-operation-arrowhead-video-gam-5b0f5d2f2b7c09.0298490415277335511781.jpg"),
            new ExampleModel ($"Title {random.Next()}",$"Description {random.Next()}", "https://banner2.cleanpng.com/20180530/tq/kisspng-arma-3-apex-arma-2-operation-arrowhead-video-gam-5b0f5d2f2b7c09.0298490415277335511781.jpg"),
            new ExampleModel ($"Title {random.Next()}",$"Description {random.Next()}", "https://banner2.cleanpng.com/20180530/tq/kisspng-arma-3-apex-arma-2-operation-arrowhead-video-gam-5b0f5d2f2b7c09.0298490415277335511781.jpg"),
            new ExampleModel ($"Title {random.Next()}",$"Description {random.Next()}", "https://banner2.cleanpng.com/20180530/tq/kisspng-arma-3-apex-arma-2-operation-arrowhead-video-gam-5b0f5d2f2b7c09.0298490415277335511781.jpg"),
            new ExampleModel ($"Title {random.Next()}",$"Description {random.Next()}", "https://banner2.cleanpng.com/20180530/tq/kisspng-arma-3-apex-arma-2-operation-arrowhead-video-gam-5b0f5d2f2b7c09.0298490415277335511781.jpg"),
            new ExampleModel ($"Title {random.Next()}",$"Description {random.Next()}", "https://banner2.cleanpng.com/20180530/tq/kisspng-arma-3-apex-arma-2-operation-arrowhead-video-gam-5b0f5d2f2b7c09.0298490415277335511781.jpg"),
            new ExampleModel ($"Title {random.Next()}",$"Description {random.Next()}", "https://banner2.cleanpng.com/20180530/tq/kisspng-arma-3-apex-arma-2-operation-arrowhead-video-gam-5b0f5d2f2b7c09.0298490415277335511781.jpg"),
            new ExampleModel ($"Title {random.Next()}",$"Description {random.Next()}", "https://banner2.cleanpng.com/20180530/tq/kisspng-arma-3-apex-arma-2-operation-arrowhead-video-gam-5b0f5d2f2b7c09.0298490415277335511781.jpg")};

            await Task.Delay(2000);

            return random.Next() % 5 == 0 ? throw new Exception($"Random error at {DateTime.Now}") : data;
        }
    }
}
