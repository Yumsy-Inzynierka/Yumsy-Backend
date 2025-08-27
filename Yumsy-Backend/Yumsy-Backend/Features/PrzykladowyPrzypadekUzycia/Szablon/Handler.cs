using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.PrzykladowyPrzypadekUzycia.Szablon;

public class Handler
{
    private readonly SupabaseDbContext _dbContext;

    public Handler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        return new Response()
        {

        };
    }
}