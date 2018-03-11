using System;
using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;

namespace Explore.Fixie.Tests.Tests
{
    public class given_100_posts_exist_when_getting_all_posts
    {
        private readonly ILogger _logger;
        private readonly List<Post> _posts;

        public given_100_posts_exist_when_getting_all_posts(ILogger logger)
        {
            _logger = logger;

            var gateway = new PostGateway("https://jsonplaceholder.typicode.com/", logger);
            _posts = gateway.GetPosts().Result;
        }

        public void then_should_return_100_posts()
        {
            _posts.Count.Should().Be(100);
        }

        public void then_should_return_each_post_with_properties_populated()
        {
            _posts.ForEach(p => p.CheckAllPropertiesAreNotNull(z => z.Id, z => z.Title, z => z.Body));
        }

        public void then_should_not_call_logger()
        {
            _logger.DidNotReceive().Error(Arg.Any<string>());
        }
    }

    public class given_post_does_not_exist_when_getting_a_post
    {
        private readonly ILogger _logger;
        private const int MissingPostId = 101;
        private readonly Post _post;

        public given_post_does_not_exist_when_getting_a_post(ILogger logger)
        {
            _logger = logger;

            var gateway = new PostGateway("https://jsonplaceholder.typicode.com/", logger);

            _post = gateway.GetPostBy(MissingPostId).Result;
        }

        public void then_should_return_null()
        {
            _post.Should().BeNull();
        }

        public void then_should_call_logger()
        {
            _logger.Received(1).Error(string.Format(PostGateway.PostDoesNotExistFormat, MissingPostId));
        }
    }
}
