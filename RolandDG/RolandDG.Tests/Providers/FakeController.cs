using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace RolandDG.Tests.Providers
{
    public static class FakeController
    {
        public static void MakeAjaxRequest(this Controller controller)
        {
            // First create request with X-Requested-With header set
            Mock<HttpRequestBase> httpRequest = new Mock<HttpRequestBase>();
            httpRequest.SetupGet(x => x.Headers).Returns(
                new WebHeaderCollection() {
                    {"X-Requested-With", "XMLHttpRequest"}
                }
            );

            // Then create contextBase using above request
            Mock<HttpContextBase> httpContext = new Mock<HttpContextBase>();
            httpContext.SetupGet(x => x.Request).Returns(httpRequest.Object);

            // Set controllerContext
            controller.ControllerContext = new ControllerContext(httpContext.Object, new RouteData(), controller);
        }
    }
}
