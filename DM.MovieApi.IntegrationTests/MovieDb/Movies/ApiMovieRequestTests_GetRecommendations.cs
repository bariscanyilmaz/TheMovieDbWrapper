using System.Linq;
using System.Threading.Tasks;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DM.MovieApi.IntegrationTests.MovieDb.Movies
{
    [TestClass]
    public class GetRecommendationsTests
    {
        private IApiMovieRequest _api;

        [TestInitialize]
        public void TestInit()
        {
            ApiResponseUtil.ThrottleTests();
            _api = MovieDbFactory.Create<IApiMovieRequest>().Value;
        }

        [TestMethod]
        public async Task GetRecommendationsAsync_Returns_ValidResults()
        {
            const int movieIdRunLolaRun = 104;

            ApiSearchResponse<MovieInfo> response = await _api.GetRecommendationsAsync(movieIdRunLolaRun);

            ApiResponseUtil.AssertErrorIsNull(response);

            Assert.IsTrue(response.Results.Count > 0);
            Assert.IsTrue(response.TotalPages > 0);
            Assert.IsTrue(response.TotalResults > 0);
            Assert.IsTrue(response.PageNumber == 1);
        }

        [TestMethod]
        public async Task GetRecommendationsAsync_HasError_InvalidMovieId()
        {
            const int movieId = 1;

            ApiSearchResponse<MovieInfo> response = await _api.GetRecommendationsAsync(movieId);
            Assert.IsNotNull(response.Error);           
        }
    }
}
