using FirebaseAdmin;
using FirebaseAdmin.Auth;
using GraphQLDemo.API.Schema.Queries;

namespace GraphQLDemo.API.DataLoaders
{
    public class UserDataLoader : BatchDataLoader<string, UserType>
    {
        private const int MaxFirebaseBatchSize = 1000;
        private readonly FirebaseAuth _firebaseAuth;

        public UserDataLoader(FirebaseAuth firebaseAuth, IBatchScheduler scheduler)
            : base(scheduler, new DataLoaderOptions
            {
                MaxBatchSize = MaxFirebaseBatchSize
            })
        {
            _firebaseAuth = firebaseAuth;
        }

        protected override async Task<IReadOnlyDictionary<string, UserType>> LoadBatchAsync(
            IReadOnlyList<string> keys,
            CancellationToken cancellationToken)
        {
            var userIds = keys.Select(u => new UidIdentifier(u)).ToList();

            var users = await _firebaseAuth.GetUsersAsync(userIds, cancellationToken);

            return users.Users
                .Select(u => new UserType()
                {
                    Id = u.Uid,
                    Username = u.DisplayName ?? u.Email,
                    PhotoUrl = u.PhotoUrl
                })
                .ToDictionary(u => u.Id);
        }
    }
}
